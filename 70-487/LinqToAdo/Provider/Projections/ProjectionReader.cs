using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace LinqToAdo.Provider.Projections
{
    internal sealed class ProjectionReader<T> :
        IProjectionRow,
        IEnumerable<T>, 
        IDisposable
    {
        private readonly IDataReader _reader;
        private readonly Func<IProjectionRow, T> _projector;

        public ProjectionReader(
            IDataReader reader,
            Func<IProjectionRow, T> projector)
        {
            _reader = reader;
            _projector = projector;
        }

        #region IProjectionRow Members

        object IProjectionRow.GetValue(int index)
        {
            return _reader.IsDBNull(index) ?
                null :
                _reader[index];
        }

        #endregion

        #region IEnumerable<T> Members

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            while (_reader.Read())
            {
                yield return _projector(this);
            }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (!_reader.IsClosed)
            {
                _reader.Close();
            }
        }

        #endregion
    }
}
