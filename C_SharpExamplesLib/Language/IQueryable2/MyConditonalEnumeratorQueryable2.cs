using System.Collections;
using System.Linq.Expressions;
using C_Sharp.Language.MyEnumerableIntegerRangeLibrary;

namespace C_Sharp.Language.IQueryable2
{
    /// <summary>
    /// Maps from IQueryable<TType>(IEnumerator<TType>)
	///      to   IQueryable<TType>(IEnumerator<TType>) 
	///           with only those elements, which match the expression
    /// </summary>
    /// <typeparam name="TType">Type of the elments</typeparam>
    public class MyConditonalEnumeratorQueryable2<TType> : IQueryable<TType>
	{
		private MyConditionalEnumerator<TType> _myIntegerEnumerator;

		public MyConditonalEnumeratorQueryable2(IEnumerator<TType> integerEnumerator)
		{
			
			_myIntegerEnumerator = new MyConditionalEnumerator<TType>(integerEnumerator,null);
			Expression = Expression.Constant(this);

			Provider = new MyConditonalEnumeratorQueryProvider2<TType>(this);
		}

		public MyConditonalEnumeratorQueryable2(IEnumerator<TType> integerEnumerator, MethodCallExpression? expression)
			
		{
			_myIntegerEnumerator = new MyConditionalEnumerator<TType>(integerEnumerator, expression);
			Expression = Expression.Constant(this);

			Provider = new MyConditonalEnumeratorQueryProvider2<TType>(this);
		}

		public IEnumerator<TType> GetEnumerator()
		{
			return (IEnumerator<TType>)_myIntegerEnumerator;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _myIntegerEnumerator;
		}

		public Type ElementType => typeof(TType);
		public Expression Expression { get; }
		public IQueryProvider Provider { get; }
	}
}