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
	public class MyQueryableIntegerSetQueryProvider2 : IQueryProvider
	{
		private MyQueryableIntegerSet2 _myQueryableIntegerSet;
		public MyQueryableIntegerSetQueryProvider2(MyQueryableIntegerSet2 QueryableIntegerSet)
		{
			_myQueryableIntegerSet = QueryableIntegerSet;
		}

		public System.Linq.IQueryable CreateQuery(Expression expression)
		{
			throw new NotImplementedException();
		}

		public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
		{
			//if (typeof(TElement) != typeof(int))
			//	throw new NotImplementedException();

			InnermostExpressionFinder whereFinder = new InnermostExpressionFinder("Where");
			MethodCallExpression? whereExpression = whereFinder.GetInnermostExpression(expression);

			if (whereExpression != null)
			{
				MyQueryableIntegerEnumerator2<int> x = new MyQueryableIntegerEnumerator2<int>(
					_myQueryableIntegerSet.GetEnumerator(), whereExpression);

				//return new MyQueryableIntegerSet2<TElement>(_myQueryableIntegerSet2);
				return (IQueryable<TElement>) x;
			}

			InnermostExpressionFinder selectFinder = new InnermostExpressionFinder("Select");
			MethodCallExpression? selectExpression = selectFinder.GetInnermostExpression(expression);
			if (selectExpression != null)
			{
				UnaryExpression unaryExpression = (UnaryExpression) (selectExpression.Arguments[1]);
				var x3 = unaryExpression.Type;
				var x31 = x3.GetGenericArguments();
				var x321 = x31[0].GenericTypeArguments[0].GetType();
				var x322 = x31[0].GenericTypeArguments[1];

				// Get the runtime type
				Type tt = typeof(int);
				// Create a generic List<T> dynamically
				Type listType = typeof(List<>).MakeGenericType(tt);
				object listInstance = Activator.CreateInstance(listType);
				;
			}

			throw new NotImplementedException("CreateQuery");
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

		#region Sum

		private int Sum()
		{
			using var enumerator = _myQueryableIntegerSet.GetEnumerator();
			enumerator.Reset();
			int sum = 0;
			while (enumerator.MoveNext())
			{
				sum += (int)(object)enumerator.Current!;
			}

			return sum;
		}

		#endregion

		#region Max

		private int Max()
		{
			using var enumerator = _myQueryableIntegerSet.GetEnumerator();
			enumerator.Reset();
			int max = Int32.MinValue;
			while (enumerator.MoveNext())
			{
				int value = (int)(object)enumerator.Current!;
				max = Math.Max(max, value);
			}

			return max;
		}

		#endregion

		public TResult Execute<TResult>(Expression expression)
		{
			// Check for any
			if (expression is MethodCallExpression {Method.Name: "Any"} anyCallExpression)
			{
				if (anyCallExpression.Arguments.Count == 1)
					return (TResult) (object) Any();
					
				return (TResult) (object) Any(anyCallExpression.Arguments[1]);
			}

			// Check for sum
			if (expression is MethodCallExpression { Method.Name: "Sum", Arguments.Count: 1}) 
				return (TResult)(object)Sum();

			// Check for max
			if (expression is MethodCallExpression { Method.Name: "Max", Arguments.Count: 1 })
				return (TResult)(object)Max();

			throw new NotImplementedException();
		}
	}
}
