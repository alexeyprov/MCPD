using System.Linq.Expressions;
using System.Text;

namespace LinqToAdo.Provider.Visitors.Translators
{
    internal sealed class WhereCallTranslator : BaseCallTranslator
    {
        public WhereCallTranslator(
            ExpressionVisitor visitor,
            StringBuilder builder,
            MethodCallExpression call) :
            base(visitor, builder, call)
        {
        }

        protected override string BuildQueryClause(LambdaExpression lambda)
        {
            return "WHERE ";
        }
    }
}
