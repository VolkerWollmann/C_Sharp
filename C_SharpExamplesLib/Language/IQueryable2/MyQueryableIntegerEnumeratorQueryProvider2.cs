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
	public class MyQueryableIntegerEnumeratorQueryProvider2<TOutputType> : IQueryProvider
	{
		private MyQueryableIntegerEnumerator2<TOutputType> _myQueryableIntegerEnumerator;
		public MyQueryableIntegerEnumeratorQueryProvider2(MyQueryableIntegerEnumerator2<TOutputType> queryableIntegerEnumerator)
		{
			_myQueryableIntegerEnumerator = queryableIntegerEnumerator;
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
				(IEnumerator<int>)_myQueryableIntegerEnumerator.GetEnumerator(), whereExpression);

			return (IQueryable<TElement>)x;
		}

		public object Execute(Expression expression)
		{
			throw new NotImplementedException();
		}

		public TResult Execute<TResult>(Expression expression)
		{
			throw new NotImplementedException();
		}
	}
}