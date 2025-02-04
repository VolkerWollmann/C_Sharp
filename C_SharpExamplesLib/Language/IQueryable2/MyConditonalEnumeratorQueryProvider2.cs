using System.Linq.Expressions;
using C_Sharp.Language.IQueryable;
using C_Sharp.Language.MyEnumerableIntegerRangeLibrary;

namespace C_Sharp.Language.IQueryable2
{
	public class MyConditionalEnumeratorQueryProvider2<TType> : IQueryProvider
	{
		private MyConditionalEnumeratorQueryable2<TType> _myQueryableIntegerEnumerator;
		public MyConditionalEnumeratorQueryProvider2(MyConditionalEnumeratorQueryable2<TType> queryableIntegerEnumerator)
		{
			_myQueryableIntegerEnumerator = queryableIntegerEnumerator;
		}

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
				MyConditionalEnumeratorQueryable2<TType> x = new MyConditionalEnumeratorQueryable2<TType>(
					_myQueryableIntegerEnumerator.GetEnumerator(), whereExpression);

				return (IQueryable<TElement>) x;
			}

            InnermostExpressionFinder selectFinder = new InnermostExpressionFinder("Select");
            MethodCallExpression? selectExpression = selectFinder.GetInnermostExpression(expression);
            if (selectExpression != null)
            {
                UnaryExpression unaryExpression = (UnaryExpression)(selectExpression.Arguments[1]);
                var unaryExpressionType = unaryExpression.Type;
                var parametersTypes = unaryExpressionType.GetGenericArguments();
                var argumentType = parametersTypes[0].GenericTypeArguments[0];
                var resultType = parametersTypes[0].GenericTypeArguments[1];

                IEnumerator<TType> enumerator = (IEnumerator<TType>)_myQueryableIntegerEnumerator.GetEnumerator();

                MySelectorEnumerator<TElement, TType> e = new MySelectorEnumerator<TElement, TType>(enumerator, selectExpression);
                //Type genericType = typeof(MySelectorEnumerator<,>).MakeGenericType(resultType,argumentType);
                //object instance = Activator.CreateInstance(genericType, enumerator, (Expression)unaryExpression);

                var x = new MySelectorEnumeratorQueryable2<TElement, TType>(e);
                //MyQueryableIntegerEnumerator2<int> x = new MyQueryableIntegerEnumerator2<int>(
                //    _myQueryableIntegerSet.GetEnumerator(), whereExpression);

                return (IQueryable<TElement>)x;
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
			using var enumerator = _myQueryableIntegerEnumerator.GetEnumerator();
			return enumerator.MoveNext();
		}

		private bool Any(Expression conditionExpression)
		{
			using var enumerator = _myQueryableIntegerEnumerator.GetEnumerator();
			using var enumerator2 = new MyConditionalEnumerator<TType>(enumerator, conditionExpression);
			return enumerator2.MoveNext();
		}
		#endregion

		#region Sum
		private int Sum()
		{
			using var enumerator = _myQueryableIntegerEnumerator.GetEnumerator();
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
			using var enumerator = _myQueryableIntegerEnumerator.GetEnumerator();
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
			if (expression is MethodCallExpression {Method.Name: "Any"} methodCallExpression)
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