using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using C_Sharp.Language.IQueryable;
using C_Sharp.Language.MyEnumerableIntegerRangeLibrary;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace C_Sharp.Language.IQueryable2
{
	public class MyQueryableIntegerSetQueryProvider2<TOutputType> : IQueryProvider
	{
		private MyQueryableIntegerSet2<TOutputType> _myQueryableIntegerSet;
		public MyQueryableIntegerSetQueryProvider2(MyQueryableIntegerSet2<TOutputType> QueryableIntegerSet)
		{
			_myQueryableIntegerSet = QueryableIntegerSet;
		}

		public System.Linq.IQueryable CreateQuery(Expression expression)
		{
			throw new NotImplementedException();
		}

		public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
		{
			if (typeof(TElement) != typeof(int))
				throw new NotImplementedException();

			InnermostExpressionFinder whereFinder = new InnermostExpressionFinder("Where");
			MethodCallExpression? whereExpression = whereFinder.GetInnermostExpression(expression);

			MyQueryableIntegerEnumerator2<int> x = new MyQueryableIntegerEnumerator2<int>(
				(IEnumerator<int>)_myQueryableIntegerSet.GetEnumerator(), whereExpression);
			
			//return new MyQueryableIntegerSet2<TElement>(_myQueryableIntegerSet2);
			return (IQueryable<TElement>)x;
		}

		public object Execute(Expression expression)
		{
			throw new NotImplementedException();
		}

		#region Any

		private bool Any()
		{
			using var enumerator = _myQueryableIntegerSet.GetEnumerator();
			return enumerator.MoveNext();
		}

		private bool Any(Expression conditionExpression)
		{
			using var enumerator = _myQueryableIntegerSet.GetEnumerator();
			using var enumerator2 = new MyConditionalEnumerator((IEnumerator<int>)enumerator, conditionExpression);
			return enumerator2.MoveNext();
		}
		#endregion

		public TResult Execute<TResult>(Expression expression)
		{
			// Check for any
			if (expression is MethodCallExpression methodCallExpression)
			{
				if (typeof(TResult) == typeof(bool))
				{
					if ( methodCallExpression.Arguments.Count == 1 )
						return (TResult) (object) Any();
					else
					{
						return (TResult) (object)Any(methodCallExpression.Arguments[1]);
					}
				}
			}
			throw new NotImplementedException();
		}
	}
}
