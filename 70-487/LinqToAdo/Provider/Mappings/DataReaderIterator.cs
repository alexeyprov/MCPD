using System;
using System.Collections.Generic;
using System.Data;

namespace LinqToAdo.Provider.Mappings
{
    internal sealed class DataReaderIterator : IDataIterator, IDisposable
    {
        private readonly IDataReader _reader;

        public DataReaderIterator(IDataReader reader)
        {
            _reader = reader;
        }

        #region IDataIterator Members

        IEnumerable<IDataAccessor> IDataIterator.Accessors
        {
            get
            {
                IDataAccessor accessor = new DataRecordAccessor(_reader, _reader.GetSchemaTable());

                while (_reader.Read())
                {
                    yield return accessor;
                }
            }
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            _reader.Close();
        }

        #endregion
    }
}
