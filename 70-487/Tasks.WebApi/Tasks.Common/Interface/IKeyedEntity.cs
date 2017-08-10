using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Common.Interface
{
    public interface IKeyedEntity<out T>
    {
        T Id
        {
            get;
        }
    }
}
