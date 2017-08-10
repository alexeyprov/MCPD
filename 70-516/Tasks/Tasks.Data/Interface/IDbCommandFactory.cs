using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Data.Interface
{
    public interface IDbCommandFactory
    {
        IDbCommand GetCommand();
    }
}
