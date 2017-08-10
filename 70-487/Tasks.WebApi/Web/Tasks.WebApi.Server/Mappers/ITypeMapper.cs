using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.WebApi.Server.Mappers
{
    public interface ITypeMapper<in TFrom, out TTo>
    {
        TTo Create(TFrom from);
    }
}
