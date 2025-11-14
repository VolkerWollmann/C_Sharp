using System.Linq.Expressions;

namespace C_SharpExamplesLib.Language.IQueryable
{
    /// <summary>
    /// used to find an expression that could be evaluated by MyQueryableIntegerSet and MyQueryableIntegerSetQueryProvider
    /// </summary>
    internal class InnermostExpressionFinder(string expressionName) : ExpressionVisitor
    {
        private MethodCallExpression? _innermostExpression;

        private readonly List<Type> _innerMostGenericTypes =
        [
            typeof(MyConditionalEnumeratorQueryable<>),
            typeof(MySelectorEnumeratorQueryable<,>),
            typeof(MyEnumeratorQueryable<>)
        ];

        private bool BaseTypeFits(Type typeToCheck)
        {
            foreach (var tt in _innerMostGenericTypes)
            {
                if (typeToCheck.IsGenericType && typeToCheck.GetGenericTypeDefinition() == tt)
                    return true;
            }

            return false;
        }

        public MethodCallExpression? GetInnermostExpression(Expression expression)
        {
            Visit(expression);
            return _innermostExpression;
        }

        protected override Expression VisitMethodCall(MethodCallExpression expression)
        {
            if (expression.Method.Name == expressionName &&
                BaseTypeFits(expression.Arguments[0].Type))
                _innermostExpression = expression;

            Visit(expression.Arguments[0]);

            return expression;
        }
    }

}
