using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LinqToAdo.Provider.Visitors
{
    /// <summary>
    /// Performs bottom-up analysis to determine which nodes can possibly
    /// be part of an evaluated sub-tree.
    /// </summary>
    internal sealed class Nominator : ExpressionVisitor
    {
        private readonly Predicate<Expression> _evaluator;
        private ISet<Expression> _candidates;
        private bool _cannotBeEvaluated;

        public Nominator(Predicate<Expression> evaluator)
        {
            _evaluator = evaluator;
        }

        public ISet<Expression> Nominate(Expression expression)
        {
            _candidates = new HashSet<Expression>();

            Visit(expression);

            return _candidates;
        }

        public override Expression Visit(Expression expression)
        {
            if (expression != null)
            {
                bool parentCannotBeEvaluated = _cannotBeEvaluated;
                _cannotBeEvaluated = false;

                base.Visit(expression);

                if (!_cannotBeEvaluated)
                {
                    if (_evaluator(expression))
                    {
                        _candidates.Add(expression);
                    }
                    else
                    {
                        _cannotBeEvaluated = true;
                    }
                }

                _cannotBeEvaluated |= parentCannotBeEvaluated;
            }

            return expression;
        }
    }
}
