using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using C_Sharp.Language.IQueryable2;
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

		private List<Type> _innerMostTypes = new List<Type>()
		{
			typeof(MyQueryableIntegerSet<int>),
			typeof(MyIntegerSetQueryable2),
			typeof(MyConditonalEnumeratorQueryable2<int>)
		};
		
		public MethodCallExpression? GetInnermostExpression(Expression expression)
		{
			Visit(expression);
			return _innermostExpression;
		}

		protected override Expression VisitMethodCall(MethodCallExpression expression)
		{
			if (expression.Method.Name == _innerMostExpressionName &&
			    _innerMostTypes.Contains(expression.Arguments[0].Type))
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
