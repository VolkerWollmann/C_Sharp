using System.Linq.Expressions;
using C_Sharp.Language.IQueryable2;

namespace C_SharpExamplesLib.Language.IQueryable2
{
    /// <summary>
    /// used to find an expression that could be evaluated by MyQueryableIntegerSet and MyQueryableIntegerSetQueryProvider
    /// </summary>
    internal class InnermostExpressionFinder : ExpressionVisitor
    {
        private readonly string _innerMostExpressionName;
        private MethodCallExpression? _innermostExpression;

        private List<Type> _innerMostGenericTypes = new List<Type>()
        {
            typeof(MyConditionalEnumeratorQueryable2<>),
            typeof(MySelectorEnumeratorQueryable2<,>)
        };

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
            if (expression.Method.Name == _innerMostExpressionName &&
                BaseTypeFits(expression.Arguments[0].Type))
                _innermostExpression = expression;

            Visit(expression.Arguments[0]);

            return expression;
        }

        public InnermostExpressionFinder(string expressionName)
        {
            _innerMostExpressionName = expressionName;
        }
    }

}
