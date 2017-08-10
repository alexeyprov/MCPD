//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Workers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;

    // http://acs.codeplex.com/SourceControl/changeset/view/66024#949591
    public class TokenValidator
    {
        private const string IssuerLabel = "Issuer";
        private const string ExpiresLabel = "ExpiresOn";
        private const string AudienceLabel = "Audience";
        private const string HmacSHA256Label = "HMACSHA256";

        private readonly string trustedSigningKey;
        private readonly string trustedTokenIssuer;
        private readonly string trustedAudienceValue;

        public TokenValidator(string acsHostName, string serviceNamespace, string trustedAudienceValue, string trustedSigningKey)
        {
            this.trustedSigningKey = trustedSigningKey;
            this.trustedTokenIssuer = string.Format("https://{0}.{1}/", serviceNamespace.ToLowerInvariant(), acsHostName.ToLowerInvariant());
            this.trustedAudienceValue = trustedAudienceValue;
        }

        public bool Validate(string token)
        {
            if (!IsHmacValid(token, Convert.FromBase64String(this.trustedSigningKey)))
            {
                return false;
            }

            if (this.IsExpired(token))
            {
                return false;
            }

            if (!this.IsIssuerTrusted(token))
            {
                return false;
            }

            if (!this.IsAudienceTrusted(token))
            {
                return false;
            }

            return true;
        }

        public Dictionary<string, string> GetNameValues(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException();
            }

            return
                token
                .Split('&')
                .Aggregate(
                new Dictionary<string, string>(),
                (dict, rawNameValue) =>
                {
                    if (rawNameValue == string.Empty)
                    {
                        return dict;
                    }

                    string[] nameValue = rawNameValue.Split('=');

                    if (nameValue.Length != 2)
                    {
                        throw new ArgumentException("Invalid formEncodedstring - contains a name/value pair missing an = character");
                    }

                    if (dict.ContainsKey(nameValue[0]))
                    {
                        throw new ArgumentException("Repeated name/value pair in form");
                    }

                    dict.Add(HttpUtility.UrlDecode(nameValue[0]), HttpUtility.UrlDecode(nameValue[1]));
                    return dict;
                });
        }

        private static ulong GenerateTimeStamp()
        {
            // Default implementation of epoch time
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToUInt64(ts.TotalSeconds);
        }

        private static bool IsHmacValid(string swt, byte[] sha256HmacKey)
        {
            string[] swtWithSignature = swt.Split(new[] { "&" + HmacSHA256Label + "=" }, StringSplitOptions.None);

            if (swtWithSignature.Length != 2)
            {
                return false;
            }

            using (var hmac = new HMACSHA256(sha256HmacKey))
            {
                byte[] locallyGeneratedSignatureInBytes = hmac.ComputeHash(Encoding.ASCII.GetBytes(swtWithSignature[0]));

                string locallyGeneratedSignature = HttpUtility.UrlEncode(Convert.ToBase64String(locallyGeneratedSignatureInBytes));

                return locallyGeneratedSignature == swtWithSignature[1];
            }
        }

        private bool IsAudienceTrusted(string token)
        {
            Dictionary<string, string> tokenValues = this.GetNameValues(token);

            string audienceValue;

            tokenValues.TryGetValue(AudienceLabel, out audienceValue);

            if (!string.IsNullOrEmpty(audienceValue))
            {
                if (audienceValue.Equals(this.trustedAudienceValue, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsIssuerTrusted(string token)
        {
            Dictionary<string, string> tokenValues = this.GetNameValues(token);

            string issuerName;

            tokenValues.TryGetValue(IssuerLabel, out issuerName);

            if (!string.IsNullOrEmpty(issuerName))
            {
                if (issuerName.Equals(this.trustedTokenIssuer))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsExpired(string swt)
        {
            try
            {
                Dictionary<string, string> nameValues = this.GetNameValues(swt);
                string expiresOnValue = nameValues[ExpiresLabel];
                ulong expiresOn = Convert.ToUInt64(expiresOnValue);
                ulong currentTime = Convert.ToUInt64(GenerateTimeStamp());

                if (currentTime > expiresOn)
                {
                    return true;
                }

                return false;
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException();
            }
        }
    }
}
