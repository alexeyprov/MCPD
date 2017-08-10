using System;
using Tasks.WebApi.Models;

namespace Tasks.WebApi.Server.Mappers
{
    public interface IUserMapper : ITypeMapper<Data.Models.User, User>
    {
        User Create(string username, string firstname, string lastname, string email, Guid userId);
    }
}
