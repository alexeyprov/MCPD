using System;
using System.Collections.Generic;
using Tasks.WebApi.Models;

namespace Tasks.WebApi.Server.Mappers
{
    internal sealed class UserMapper : IUserMapper
    {
        #region IUserMapper Members

        User IUserMapper.Create(string username, string firstname, string lastname, string email, Guid userId)
        {
            return new User
            {
                Email = email,
                FirstName = firstname,
                LastName = lastname,
                UserId = userId,
                UserName = username,
                Links = new List<Link>
                {
                    new Link
                    {
                        Rel = "self",
                        Title = "self",
                        Href = "/api/users/" + userId
                    },
                    new Link
                    {
                        Rel = "all",
                        Title = "All Users",
                        Href = "/api/users"
                    }
                }
            };
        }

        #endregion

        #region ITypeMapper<User,User> Members

        User ITypeMapper<Data.Models.User, User>.Create(Data.Models.User from)
        {
            return ((IUserMapper)this).Create(
                from.UserName,
                from.FirstName,
                from.LastName,
                from.Email,
                from.UserId);
        }

        #endregion
    }
}