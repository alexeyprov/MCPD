using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Data.Models;

namespace Tasks.Data.Interface
{
    public interface IUserRepository
    {
        void SaveUser(Guid userId, string firstname, string lastname);

        User GetUser(Guid userId);
    }
}
