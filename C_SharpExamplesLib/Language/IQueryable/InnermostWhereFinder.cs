using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace C_Sharp.Language.IQueryable
{
	/// <summary>
	/// used to find an expression that could be evaluated by MyQueryableIntegerSet and MyQueryableIntegerSetQueryProvider
	/// </summary>
	internal class InnermostExpressionFinder : ExpressionVisitor
	{
		private string _innerMostExpressionName = "Where";
		private MethodCallExpression? _innermostExpression = null;

		public MethodCallExpression? GetInnermostExpression(Expression expression)
		{
			Visit(expression);
			return _innermostExpression;
		}

		protected override Expression VisitMethodCall(MethodCallExpression expression)
		{
			if (expression.Method.Name == "Where" &&
			    expression.Arguments[0].Type.Name.StartsWith("MyQueryableIntegerSet"))
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
