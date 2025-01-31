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
		#endregion

		public TResult Execute<TResult>(Expression expression)
		{
			// Check for any
			if (expression is MethodCallExpression methodCallExpression)
			{
				if ( typeof(TResult) == typeof(bool))
					return (TResult)(object)Any();
			}
			throw new NotImplementedException();
		}
	}
}
