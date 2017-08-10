using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

using LinqToAdo.Provider.Projections;
using LinqToAdo.Provider.Visitors.Projections;

namespace LinqToAdo.Provider.Visitors
{
    internal sealed class ColumnProjector : ExpressionVisitor
    {
        private static readonly MethodInfo _getDataMethod;
        private static readonly ParameterExpression _newParam;
        private readonly ICollection<string> _columns;
        private readonly LambdaExpression _expression;
        private readonly ParameterExpression _oldParam;

        static ColumnProjector()
        {
            _getDataMethod = typeof(IProjectionRow).GetMethod("GetValue",
                BindingFlags.Public | BindingFlags.Instance);
            _newParam = Expression.Parameter(typeof(IProjectionRow), "r");
        }

        public ColumnProjector(LambdaExpression expression)
        {
            _expression = expression;
            _columns = new List<string>();
            _oldParam = expression.Parameters[0];
        }

        public ProjectionResult Project()
        {
            _columns.Clear();

            Type delegateType = typeof(Func<,>).MakeGenericType(
                typeof(IProjectionRow),
                _expression.ReturnType);
            Expression body = Visit(_expression.Body);

            return new ProjectionResult
            {
                Projector = Expression.Lambda(delegateType, body, _newParam),
                ColumnList = string.Join(", ", _columns)
            };
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression != _oldParam || node.Member.MemberType != MemberTypes.Property)
            {
                return base.VisitMember(node);
            }

            _columns.Add(node.Member.Name);

            Expression call = Expression.Call(
                _newParam,
                _getDataMethod,
                Expression.Constant(_columns.Count - 1));

            return Expression.Convert(call, ((PropertyInfo)node.Member).PropertyType);
        }
    }
}
