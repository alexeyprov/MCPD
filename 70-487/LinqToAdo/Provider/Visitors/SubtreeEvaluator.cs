using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LinqToAdo.Provider.Visitors
{
    /// <summary>
    /// Evaluates & replaces sub-trees when first candidate is reached (top-down)
    /// </summary>
    internal sealed class SubtreeEvaluator : ExpressionVisitor
    {
        private readonly ISet<Expression> _candidates;

        public SubtreeEvaluator(ISet<Expression> candidates)
        {
            _candidates = candidates;
        }

        public override Expression Visit(Expression node)
        {
            if (node == null)
            {
                return null;
            }

            if (_candidates.Contains(node))
            {
                return Evaluate(node);
            }

            return base.Visit(node);
        }

        private static Expression Evaluate(Expression node)
        {
            if (node.NodeType == ExpressionType.Constant)
            {
                return node;
            }

            LambdaExpression lambda = Expression.Lambda(node);

            Delegate d = lambda.Compile();

            return Expression.Constant(
                d.DynamicInvoke(null),
                node.Type);
        }
    }
}
