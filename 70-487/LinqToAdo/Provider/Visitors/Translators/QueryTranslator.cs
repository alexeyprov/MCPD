using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace LinqToAdo.Provider.Visitors.Translators
{
    public sealed class QueryTranslator : ExpressionVisitor
    {
        private StringBuilder _builder;
        private LambdaExpression _projector;
        private readonly bool _shouldPluralize;
        private readonly Predicate<Expression> _inlineFilter;

        public QueryTranslator(bool shouldPluralize, Predicate<Expression> inlineFilter = null)
        {
            _shouldPluralize = shouldPluralize;
            _inlineFilter = inlineFilter ??
                new Predicate<Expression>(e => e.NodeType != ExpressionType.Parameter);
        }

        public TranslationResult Translate(Expression expression)
        {
            _builder = new StringBuilder();

            expression = InlineConstants(expression);

            Visit(expression);

            return new TranslationResult
            {
                CommandText = _builder.ToString(),
                Projector = _projector
            };
        }

        protected override Expression VisitMethodCall(MethodCallExpression expression)
        {
            BaseCallTranslator callTranslator = CreateCallTranslator(expression);

            callTranslator.Translate();

            _projector = callTranslator.Projector;

            return expression;
        }

        private BaseCallTranslator CreateCallTranslator(MethodCallExpression expression)
        {
            MethodInfo method = expression.Method;

            if (method.DeclaringType == typeof(Queryable))
            {
                switch (method.Name)
                {
                    case "Where":
                        return new WhereCallTranslator(this, _builder, expression);
                    case "Select":
                        return new SelectCallTranslator(this, _builder, expression);
                    default:
                        break;
                }
            }

            throw new NotSupportedException(
                string.Format(
                    "Method {0}.{1} is not supported",
                    method.DeclaringType,
                    method.Name));
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            IQueryable queryable = node.Value as IQueryable;
            if (node.Value == null)
            {
                _builder.Append("NULL");
            }
            else if (queryable != null)
            {
                _builder.Append("SELECT * FROM ");
                _builder.Append(PluralizeIfNeeded(queryable.ElementType.Name));
            }
            else
            {
                switch (Type.GetTypeCode(node.Type))
                {
                    case TypeCode.Boolean:
                        _builder.Append(
                            ((bool)node.Value) ?
                            1 :
                            0);
                        break;
                    
                    case TypeCode.String:
                        _builder.AppendFormat("'{0}'", node.Value);
                        break;

                    case TypeCode.Object:
                        throw new NotSupportedException(
                            "Object-typed constants are not supported");

                    default:
                        _builder.Append(node.Value);
                        break;
                }
            }

            return node;
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (node.NodeType == ExpressionType.Not)
            {
                _builder.Append("NOT");
                Visit(node.Operand);

                return node;
            }

            throw new NotSupportedException(
                string.Format(
                    "{0} unary expression is not supported",
                    node.NodeType));
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            _builder.Append('(');
            Visit(node.Left);

            switch (node.NodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    _builder.Append(" AND ");
                    break;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    _builder.Append(" OR ");
                    break;
                case ExpressionType.Equal:
                    _builder.Append(" = ");
                    break;
                case ExpressionType.NotEqual:
                    _builder.Append(" <> ");
                    break;
                case ExpressionType.LessThan:
                    _builder.Append(" < ");
                    break;
                case ExpressionType.LessThanOrEqual:
                    _builder.Append(" <= ");
                    break;
                case ExpressionType.GreaterThan:
                    _builder.Append(" > ");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    _builder.Append(" >= ");
                    break;
                default:
                    throw new NotSupportedException(
                        string.Format(
                            "{0} binary expression type is not supported",
                            node.NodeType));
            }

            Visit(node.Right);
            _builder.Append(')');

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression != null && node.Expression.NodeType == ExpressionType.Parameter)
            {
                _builder.Append(node.Member.Name);
                return node;
            }

            throw new NotSupportedException(
                string.Format(
                    "Member {0} is not supported",
                    node.Member.Name));
        }

        private string PluralizeIfNeeded(string entityName)
        {
            if (!_shouldPluralize)
            {
                return entityName;
            }

            return entityName.EndsWith("y") ?
                entityName.Substring(0, entityName.Length - 1) + "ies" :
                entityName + 's';
        }

        private Expression InlineConstants(Expression expression)
        {
            Nominator nominator = new Nominator(_inlineFilter);
            ISet<Expression> nominations = nominator.Nominate(expression);

            SubtreeEvaluator evaluator = new SubtreeEvaluator(nominations);
            expression = evaluator.Visit(expression);
            return expression;
        }
    }
}
