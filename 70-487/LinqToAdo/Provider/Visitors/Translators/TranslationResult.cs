using System.Linq.Expressions;

namespace LinqToAdo.Provider.Visitors.Translators
{
    public sealed class TranslationResult
    {
        public string CommandText
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
