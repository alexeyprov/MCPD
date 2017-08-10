using System.Collections.Generic;
using System.Data;

namespace LinqToAdo.Provider.Mappings
{
    internal sealed class DataRowsIterator : IDataIterator
    {
        private IEnumerable<DataRow> _rows;

        public DataRowsIterator(IEnumerable<DataRow> rows)
        {
            _rows = rows;
        }

        #region IDataIterator Members

        IEnumerable<IDataAccessor> IDataIterator.Accessors
        {
            get
            {
                foreach (DataRow row in _rows)
                {
                    yield return new DataRowAccessor(row);
                }
            }
        }

        #endregion
    }
}
