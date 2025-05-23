﻿using System.Linq.Expressions;

namespace C_SharpExamplesLib.Language.IQueryable
{
	public class MyEnumeratorQueryProvider<TType>(MyEnumeratorQueryable<TType> queryableIntegerEnumerator)
		: IQueryProvider
	{
		public System.Linq.IQueryable CreateQuery(Expression expression)
		{
			throw new NotImplementedException();
		}

		public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
		{
			InnermostExpressionFinder whereFinder = new InnermostExpressionFinder("Where");
			MethodCallExpression? whereExpression = whereFinder.GetInnermostExpression(expression);

			if (whereExpression != null)
			{
				var result = MyQueryableFactory.GetMyConditionalEnumeratorQueryable(queryableIntegerEnumerator.GetEnumerator(), whereExpression);

				return (IQueryable<TElement>)result;
			}

			InnermostExpressionFinder selectFinder = new InnermostExpressionFinder("Select");
			MethodCallExpression? selectExpression = selectFinder.GetInnermostExpression(expression);
			if (selectExpression != null)
			{
				IEnumerator<TType> enumerator = queryableIntegerEnumerator.GetEnumerator();

				var selectorEnumerator = new MySelectorEnumerator<TElement, TType>(enumerator, selectExpression);

				var newQueryableEnumerator = new MySelectorEnumeratorQueryable<TElement, TType>(selectorEnumerator);

				return newQueryableEnumerator;
			}

			throw new NotImplementedException("CreateQuery");
		}

		public object Execute(Expression expression)
		{
			throw new NotImplementedException();
		}

		#region aggregate functions
		#region Any
		private bool Any()
		{
			using var enumerator = queryableIntegerEnumerator.GetEnumerator();
			return enumerator.MoveNext();
		}

		private bool Any(Expression conditionExpression)
		{
			using var enumerator = queryableIntegerEnumerator.GetEnumerator();
			using var enumerator2 = new MyConditionalEnumerator<TType>(enumerator, conditionExpression);
			return enumerator2.MoveNext();
		}
		#endregion

		#region Sum
		private int Sum()
		{
			using var enumerator = queryableIntegerEnumerator.GetEnumerator();
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
			using var enumerator = queryableIntegerEnumerator.GetEnumerator();
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
		#endregion

		public TResult Execute<TResult>(Expression expression)
		{
			// Check for any
			if (expression is MethodCallExpression { Method.Name: "Any" } methodCallExpression)
			{
				if (methodCallExpression.Arguments.Count == 1)
					return (TResult)(object)Any();

				return (TResult)(object)Any(methodCallExpression.Arguments[1]);
			}

			// Check for sum
			if (expression is MethodCallExpression { Method.Name: "Sum", Arguments.Count: 1 })
				return (TResult)(object)Sum();

			// Check for max
			if (expression is MethodCallExpression { Method.Name: "Max", Arguments.Count: 1 })
				return (TResult)(object)Max();

			throw new NotImplementedException();
		}
	}
}