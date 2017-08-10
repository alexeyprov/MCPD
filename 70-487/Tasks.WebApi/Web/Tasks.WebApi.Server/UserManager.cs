using Tasks.Data.Interface;
using Tasks.WebApi.Common.Interface;
using Tasks.WebApi.Common.Security;
using Tasks.WebApi.Models;
using Tasks.WebApi.Server.Mappers;

namespace Tasks.WebApi.Server
{
    internal sealed class UserManager : IUserManager
    {
        private readonly IUserRepository _repository;
        private readonly IMembershipInfoProvider _membership;
        private readonly IUserMapper _mapper;

        public UserManager(
            IUserRepository repository,
            IMembershipInfoProvider membership,
            IUserMapper mapper)
        {
            _repository = repository;
            _membership = membership;
            _mapper = mapper;
        }

        #region IUserManager Members

        User IUserManager.CreateUser(string username, string password, string firstname, string lastname, string email)
        {
            // first create core (ASP.NET membership) user
            MembershipUserWrapper membershipUser = _membership.CreateUser(username, password, email);

            // next create custom user info based on core user
            _repository.SaveUser(membershipUser.UserId, firstname, lastname);

            // finally, map user data to a web model
            return _mapper.Create(username, firstname, lastname, email, membershipUser.UserId);
        }

        #endregion
    }
}