using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.WebApi.Models;

namespace Tasks.WebApi.Server
{
    public interface IUserManager
    {
        User CreateUser(string username, string password, string firstname, string lastname, string email);
    }
}
