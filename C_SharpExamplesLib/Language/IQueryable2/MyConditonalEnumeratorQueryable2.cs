using System.Collections;
using System.Linq.Expressions;
using C_Sharp.Language.MyEnumerableIntegerRangeLibrary;

namespace C_Sharp.Language.IQueryable2
{
    /// <summary>
    /// Maps from IQueryable TType (IEnumerator TType )
	///      to   IQueryable TType (IEnumerator TType) 
	///           with only those elements, which match the expression
    /// </summary>
    /// <typeparam name="TType">Type of the elements</typeparam>
    public class MyConditionalEnumeratorQueryable2<TType> : IQueryable<TType>
	{
		private MyConditionalEnumerator<TType> _myIntegerEnumerator;


		/// <summary>
		/// empty condition : acts like identical list
		/// </summary>
		/// <param name="integerEnumerator">the base enumerator</param>
		public MyConditionalEnumeratorQueryable2(IEnumerator<TType> integerEnumerator)
		{
			
			_myIntegerEnumerator = new MyConditionalEnumerator<TType>(integerEnumerator,null);
			Expression = Expression.Constant(this);

			Provider = new MyConditionalEnumeratorQueryProvider2<TType>(this);
		}

		/// <summary>
		/// Filters out objects, which do not match the condition 
		/// </summary>
		/// <param name="integerEnumerator">the base enumerator</param>
		/// <param name="expression">condition</param>
		public MyConditionalEnumeratorQueryable2(IEnumerator<TType> integerEnumerator, MethodCallExpression? expression)
			
		{
			_myIntegerEnumerator = new MyConditionalEnumerator<TType>(integerEnumerator, expression);
			Expression = Expression.Constant(this);

			Provider = new MyConditionalEnumeratorQueryProvider2<TType>(this);
		}

		/// <summary>
		/// used if type is known at compile time
		/// </summary>
		/// <returns></returns>
		public IEnumerator<TType> GetEnumerator()
		{
			return _myIntegerEnumerator;
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