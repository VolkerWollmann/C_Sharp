using System.Linq.Expressions;

namespace C_SharpExamplesLib.Language.IQueryable
{
    public class MySelectorEnumeratorQueryProvider<TResultType, TBaseType>(
        MySelectorEnumeratorQueryable<TResultType, TBaseType> mySelectorEnumerator)
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
                var result = MyQueryableFactory.GetMyConditionalEnumeratorQueryable(
                    mySelectorEnumerator.GetEnumerator(), whereExpression);

                return (IQueryable<TElement>)result;
            }

            InnermostExpressionFinder selectFinder = new InnermostExpressionFinder("Select");
            MethodCallExpression? selectExpression = selectFinder.GetInnermostExpression(expression);
            if (selectExpression != null)
            {
                IEnumerator<TResultType> enumerator = mySelectorEnumerator.GetEnumerator();

                var selectorEnumerator = new MySelectorEnumerator<TElement, TResultType>(enumerator, selectExpression);

                var newQueryableEnumerator = new MySelectorEnumeratorQueryable<TElement, TResultType>(selectorEnumerator);

                return newQueryableEnumerator;
            }

            throw new NotImplementedException("CreateQuery");
        }

        #region aggregate functions
        #region Any
        private bool Any()
        {
            using var enumerator = mySelectorEnumerator.GetEnumerator();
            return enumerator.MoveNext();
        }

        private bool Any(Expression conditionExpression)
        {
            using var enumerator = mySelectorEnumerator.GetEnumerator();
            using var enumerator2 = new MyConditionalEnumerator<TResultType>(enumerator, conditionExpression);
            return enumerator2.MoveNext();
        }
        #endregion

        #region Sum
        private int Sum()
        {
            using var enumerator = mySelectorEnumerator.GetEnumerator();
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
            using var enumerator = mySelectorEnumerator.GetEnumerator();
            enumerator.Reset();
            int max = Int32.MinValue;
            while (enumerator.MoveNext())
            {
                int value = (int)(object)enumerator.Current!;
                max = Math.Max(max, value);
            }

            return max;
        }

        private TResultType First()
        {
            using var enumerator = mySelectorEnumerator.GetEnumerator();
            enumerator.Reset();
            if (enumerator.MoveNext())
            {
                return enumerator.Current!;
            }

            throw new InvalidOperationException("Sequence contains no elements.");
        }

        #endregion

        #region Count
        private int Count()
        {
            using var enumerator = mySelectorEnumerator.GetEnumerator();
            enumerator.Reset();
            int count = 0;
            while (enumerator.MoveNext())
            {
                count++;
            }

            return count;
        }
        #endregion
        #endregion

        #region AtIndex
        private TResultType AtIndex(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            using var enumerator = mySelectorEnumerator.GetEnumerator();
            enumerator.Reset();
            for (int i = 0; i <= index; i++)
            {
                if (!enumerator.MoveNext())
                    throw new ArgumentOutOfRangeException(nameof(index));
            }

            return enumerator.Current!;
        }

        private static int GetIndexArgument(MethodCallExpression methodCall)
        {
            Expression indexExpression = methodCall.Arguments[1];
            if (indexExpression is UnaryExpression { NodeType: ExpressionType.Convert } convert &&
                convert.Operand is ConstantExpression inner &&
                inner.Value is int converted)
                return converted;

            if (indexExpression is ConstantExpression { Value: int idx })
                return idx;

            throw new NotSupportedException("ElementAt index must be a constant int.");
        }
        #endregion

        // actual interface is  public object? Execute(Expression expression)
        public object Execute(Expression expression)
        {
            // Check for any
            if (expression is MethodCallExpression { Method.Name: "Any" } methodCallExpression)
            {
                if (methodCallExpression.Arguments.Count == 1)
                    return Any();

                return Any(methodCallExpression.Arguments[1]);
            }

            // Check for sum
            if (expression is MethodCallExpression { Method.Name: "Sum", Arguments.Count: 1 })
                return Sum();

            // Check for max
            if (expression is MethodCallExpression { Method.Name: "Max", Arguments.Count: 1 })
                return Max();

            // Check for count
            if (expression is MethodCallExpression { Method.Name: "Count", Arguments.Count: 1 })
                return Count();

            // Check for first
            if (expression is MethodCallExpression { Method.Name: "First" })
                return First()!;

            // Check for ElementAt
            if (expression is MethodCallExpression { Method.Name: "ElementAt", Arguments.Count: 2 } elementAtCall)
                return AtIndex(GetIndexArgument(elementAtCall))!;

            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return (TResult)Execute(expression);
        }

    }
}
