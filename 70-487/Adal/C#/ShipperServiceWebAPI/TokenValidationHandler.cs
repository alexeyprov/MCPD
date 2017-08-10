using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Metadata;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace ShipperService
{
    internal class TokenValidationHandler : DelegatingHandler
    {
        #region Private Variables

        private static string _issuer;
        private static DateTime _stsMetadataRetrievalTime;
        private static IEnumerable<X509SecurityToken> _signingTokens;

        #endregion

        #region Overrides

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            string token = RetrieveToken(request);
            if (string.IsNullOrEmpty(token))
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            }

            try
            {
                UpdateTenantInformation();
            }
            catch (Exception)
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }

            try
            {
                // Use JwtSecurityTokenHandler to validate the JWT token
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler
                {
                    // turning off certificate validation for demo purposes. Please note that this shouldn't be done in production code.
                    CertificateValidator = X509CertificateValidator.None
                };

                // Set the expected properties of the JWT token in the TokenValidationParameters
                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    AllowedAudience = ConfigurationManager.AppSettings["AllowedAudience"],
                    ValidIssuer = _issuer,
                    SigningTokens = _signingTokens
                };

                Thread.CurrentPrincipal = tokenHandler.ValidateToken(token, validationParameters);

                if (HttpContext.Current != null)
                {
                    HttpContext.Current.User = Thread.CurrentPrincipal;
                }

                return base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenValidationException)
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            }
            catch (Exception)
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        #endregion

        #region Implementation

        // This function retrieves ACS token (in format of OAuth 2.0 Bearer Token type) from 
        // the Authorization header in the incoming HTTP request from the ShipperClient.
        private static string RetrieveToken(HttpRequestMessage request)
        {
            IEnumerable<string> authzHeaders;

            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                // Fail if no Authorization header or more than one Authorization headers 
                // are found in the HTTP request 
                return null;
            }

            // Remove the bearer token scheme prefix and return the rest as ACS token 
            string bearerToken = authzHeaders.ElementAt(0);
            return bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
        }

        /// <summary>
        /// Parses the federation metadata document and gets issuer Name and Signing Certificates
        /// </summary>
        private static void UpdateTenantInformation()
        {
            string metadataAddress = ConfigurationManager.AppSettings["Tenant"] +
                @"FederationMetadata\2007-06\FederationMetadata.xml";

            // The issuer and signingTokens are cached for 24 hours. They are updated if any of the conditions in the if condition is true.            
            if (DateTime.UtcNow.Subtract(_stsMetadataRetrievalTime).TotalHours > 24)
            {
                _issuer = null;
                _signingTokens = null;
            }

            if (string.IsNullOrEmpty(_issuer) || _signingTokens == null)
            {
                MetadataSerializer serializer = new MetadataSerializer
                {
                    // turning off certificate validation for demo. Don't use this in production code.
                    CertificateValidationMode = X509CertificateValidationMode.None
                };

                EntityDescriptor entityDescriptor;
                using (XmlReader reader = XmlReader.Create(metadataAddress))
                {
                    MetadataBase metadata = serializer.ReadMetadata(reader);
                    entityDescriptor = (EntityDescriptor)metadata;
                }

                // get the issuer name.
                if (!string.IsNullOrWhiteSpace(entityDescriptor.EntityId.Id))
                {
                    _issuer = entityDescriptor.EntityId.Id;
                }

                // get the signing certs.
                _signingTokens = ReadSigningCertsFromMetadata(entityDescriptor);

                _stsMetadataRetrievalTime = DateTime.UtcNow;
            }
        }

        private static IEnumerable<X509SecurityToken> ReadSigningCertsFromMetadata(EntityDescriptor entityDescriptor)
        {
            List<X509SecurityToken> stsSigningTokens = new List<X509SecurityToken>();

            SecurityTokenServiceDescriptor stsd = entityDescriptor.RoleDescriptors.OfType<SecurityTokenServiceDescriptor>().First();

            if (stsd == null)
            {
                throw new InvalidOperationException("There is no RoleDescriptor of type SecurityTokenServiceType in the metadata");
            }

            // read non-null X509Data keyInfo elements meant for Signing
            IEnumerable<X509SecurityToken> x509Tokens =
                from k in stsd.Keys
                where k.KeyInfo != null && (k.Use == KeyType.Signing || k.Use == KeyType.Unspecified)
                let c = k.KeyInfo.OfType<X509RawDataKeyIdentifierClause>().First()
                select new X509SecurityToken(new X509Certificate2(c.GetX509RawData()));

            return x509Tokens.ToList();
        }

        #endregion
    }
}