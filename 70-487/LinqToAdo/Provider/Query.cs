using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqToAdo.Provider
{
    public class Query<T> : IQueryable<T>
    {
        private readonly QueryProvider _provider;
        private readonly Expression _expression;

        public Query(QueryProvider provider, Expression expression = null)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            expression = expression ?? Expression.Constant(this);

            if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type))
            {
                throw new ArgumentOutOfRangeException("expression");
            }

            _provider = provider;
            _expression = expression;
        }

        #region IEnumerable<T> Members

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (_provider.Execute<IEnumerable<T>>(_expression)).GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (_provider.Execute<IEnumerable>(_expression)).GetEnumerator();
        }

        #endregion

        #region IQueryable Members

        Type IQueryable.ElementType
        {
            get
            {
                return typeof(T);
            }
        }

        Expression IQueryable.Expression
        {
            get
            {
                return _expression;
            }
        }

        IQueryProvider IQueryable.Provider
        {
            get
            {
                return _provider;
            }
        }

        #endregion

        public override string ToString()
        {
            return _provider.GetQueryText(_expression);
        }
    }
}
