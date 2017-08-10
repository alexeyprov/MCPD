using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Tasks.WebApi.Common.Interface;

namespace Tasks.WebApi.Common.Security
{
    public sealed class MembershipAdapter : IMembershipInfoProvider
    {
        #region IMembershipInfoProvider Members

        MembershipUserWrapper IMembershipInfoProvider.GetUser(string username)
        {
            MembershipUser user = Membership.GetUser(username);

            return CreateUserWrapper(user);
        }


        MembershipUserWrapper IMembershipInfoProvider.GetUser(Guid userId)
        {
            MembershipUser user = Membership.GetUser(userId);

            return CreateUserWrapper(user);
        }

        MembershipUserWrapper IMembershipInfoProvider.CreateUser(string username, string password, string email)
        {
            MembershipUser user = Membership.CreateUser(username, password, email);

            return CreateUserWrapper(user);
        }

        bool IMembershipInfoProvider.ValidateUser(string username, string password)
        {
            return Membership.ValidateUser(username, password);
        }

        string[] IMembershipInfoProvider.GetRolesForUser(string username)
        {
            return Roles.GetRolesForUser(username);
        }

        #endregion

        private static MembershipUserWrapper CreateUserWrapper(MembershipUser user)
        {
            return user == null ?
                null :
                new MembershipUserWrapper
                {
                    Email = user.Email,
                    UserId = Guid.Parse(user.ProviderUserKey.ToString()),
                    Username = user.UserName
                };
        }
    }
}
