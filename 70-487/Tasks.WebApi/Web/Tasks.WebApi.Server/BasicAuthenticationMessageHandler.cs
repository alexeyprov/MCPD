using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using NHibernate;
using Tasks.Data.Models;
using Tasks.WebApi.Common;
using Tasks.WebApi.Common.Interface;
using Tasks.WebApi.Common.Security;

namespace Tasks.WebApi.Server
{
    internal sealed class BasicAuthenticationMessageHandler : DelegatingHandler
    {
        private const string BASIC_SCHEME = "Basic";

        private readonly Lazy<IMembershipInfoProvider> _membershipProvider;
        private readonly Lazy<ISessionFactory> _sessionFactory;

        public BasicAuthenticationMessageHandler()
        {
            _membershipProvider = new Lazy<IMembershipInfoProvider>(
                WebDependencyResolver.Get<IMembershipInfoProvider>,
                true);
            _sessionFactory = new Lazy<ISessionFactory>(
                WebDependencyResolver.Get<ISessionFactory>,
                true);
        }

        #region Overrides

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            AuthenticationHeaderValue header = request.Headers.Authorization;

            if (header == null ||
                !string.Equals(header.Scheme, BASIC_SCHEME, StringComparison.InvariantCultureIgnoreCase) ||
                string.IsNullOrWhiteSpace(header.Parameter))
            {
                return CreateUnauthorizedResponse();
            }

            string parameter = Encoding.ASCII.GetString(
                Convert.FromBase64String(header.Parameter));

            string[] credentials = parameter.Split(':');
            if (credentials.Length != 2)
            {
                return CreateUnauthorizedResponse();
            }

            string username = credentials[0].Trim();
            string password = credentials[1].Trim();

            if (!_membershipProvider.Value.ValidateUser(username, password))
            {
                return CreateUnauthorizedResponse();
            }

            SetPrincipal(username);

            return base.SendAsync(request, cancellationToken);
        }

        #endregion

        private void SetPrincipal(string username)
        {
            MembershipUserWrapper userWrapper = _membershipProvider.Value.GetUser(username);

            User user;
            using (ISession session = _sessionFactory.Value.OpenSession())
            {
                user = session.Get<User>(userWrapper.UserId);
            }

            IPrincipal principal = new GenericPrincipal(
                CreateIdentity(user),
                _membershipProvider.Value.GetRolesForUser(username));

            Thread.CurrentPrincipal = principal;

            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal; 
            }
        }

        private static IIdentity CreateIdentity(User user)
        {
            ClaimsIdentity identity = new GenericIdentity(user.UserName, BASIC_SCHEME);

            identity.AddClaims(
                new[]
                {
                    new Claim(ClaimTypes.Sid, user.UserId.ToString()),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Email, user.Email)
                });

            return identity;
        }

        private static Task<HttpResponseMessage> CreateUnauthorizedResponse()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.Headers.Add("WWW-Authenticate", BASIC_SCHEME);

            TaskCompletionSource<HttpResponseMessage> taskSource = new TaskCompletionSource<HttpResponseMessage>();

            taskSource.SetResult(response);

            return taskSource.Task;
        }
    }
}
