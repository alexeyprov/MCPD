using System;
using System.Collections.Generic;

namespace CustomUI.Validation
{
    public sealed class FuncEqualityComparer<T> : EqualityComparer<T>
    {
        private Func<T, object> _keySelector;

        public FuncEqualityComparer(Func<T, object> keySelector)
        {
            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }

            _keySelector = keySelector;
        }

        public override bool Equals(T x, T y)
        {
            object xKey = (x != null) ? _keySelector(x) : null;
            object yKey = (y != null) ? _keySelector(y) : null;

            return (xKey != null) ?
                xKey.Equals(yKey) :
                (yKey == null);
        }

        public override int GetHashCode(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            object key = _keySelector(obj);

            return (key != null) ?
                key.GetHashCode() :
                0;
        }
    }
}
