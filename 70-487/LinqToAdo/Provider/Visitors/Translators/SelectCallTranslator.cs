using System.Linq.Expressions;
using System.Text;
using LinqToAdo.Provider.Visitors.Projections;

namespace LinqToAdo.Provider.Visitors.Translators
{
    internal sealed class SelectCallTranslator : BaseCallTranslator
    {
        private ProjectionResult _projectionResult;

        public SelectCallTranslator(
            ExpressionVisitor visitor,
            StringBuilder builder,
            MethodCallExpression call) :
            base(visitor, builder, call)
        {
        }

        public override LambdaExpression Projector
        {
            get
            {
                return _projectionResult.Projector;
            }
        }

        protected override string BuildColumnList(LambdaExpression lambda)
        {
            ColumnProjector projector = new ColumnProjector(lambda);

            _projectionResult = projector.Project();

            return _projectionResult.ColumnList;
        }
    }
}
