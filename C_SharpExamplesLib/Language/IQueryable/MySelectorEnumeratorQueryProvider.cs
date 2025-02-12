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

            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return (TResult) Execute(expression);
        }

    }
}
