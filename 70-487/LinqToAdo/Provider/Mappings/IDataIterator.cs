using System.Collections.Generic;

namespace LinqToAdo.Provider.Mappings
{
    internal interface IDataIterator
    {
        IEnumerable<IDataAccessor> Accessors
        {
            get;
        }
    }
}
