using System.Linq.Expressions;
using System.Text;

namespace LinqToAdo.Provider.Visitors.Translators
{
    internal abstract class BaseCallTranslator
    {
        private readonly StringBuilder _builder;
        private readonly Expression _from;
        private readonly LambdaExpression _lambda;
        private readonly ExpressionVisitor _visitor;

        protected BaseCallTranslator(
            ExpressionVisitor visitor,
            StringBuilder builder,
            MethodCallExpression call)
        {
            _visitor = visitor;
            _builder = builder;

            _from = call.Arguments[0];
            _lambda = (LambdaExpression)StripQuotes(call.Arguments[1]);
        }

        public virtual LambdaExpression Projector
        {
            get
            {
                return null;
            }
        }

        public void Translate()
        {
            _builder.AppendFormat(
                "SELECT {0} FROM (",
                BuildColumnList(_lambda));

            _visitor.Visit(_from);

            string clause = BuildQueryClause(_lambda);
            _builder.AppendFormat(") t {0}", clause);

            if (!string.IsNullOrEmpty(clause))
            {
                _visitor.Visit(_lambda.Body);
            }
        }

        protected virtual string BuildColumnList(LambdaExpression lambda)
        {
            return "*";
        }

        protected virtual string BuildQueryClause(LambdaExpression lambda)
        {
            return string.Empty;
        }

        private static Expression StripQuotes(Expression expression)
        {
            while (expression.NodeType == ExpressionType.Quote)
            {
                expression = ((UnaryExpression)expression).Operand;
            }

            return expression;
        }
    }
}
