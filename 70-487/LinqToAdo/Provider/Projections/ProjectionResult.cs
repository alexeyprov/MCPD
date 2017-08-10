using System.Linq.Expressions;

namespace LinqToAdo.Provider.Visitors.Projections
{
    public sealed class ProjectionResult
    {
        public string ColumnList
        {
            get;
            set;
        }

        public LambdaExpression Projector
        {
            get;
            set;
        }
    }
}
