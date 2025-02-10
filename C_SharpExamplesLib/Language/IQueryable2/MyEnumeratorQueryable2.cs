using System.Collections;
using System.Linq.Expressions;
using C_Sharp.Language.MyEnumerableIntegerRangeLibrary;
using C_SharpExamplesLib.Language.IQueryable2;

namespace C_Sharp.Language.IQueryable2
{
	public class MyEnumeratorQueryable2<TType> : IMyDisposeQueryable<TType>
	{
		private IEnumerator<TType> _myIntegerEnumerator;


		/// <summary>
		/// empty condition : acts like identical list
		/// </summary>
		/// <param name="integerEnumerator">the base enumerator</param>
		public MyEnumeratorQueryable2(IEnumerator<TType> integerEnumerator)
		{

			_myIntegerEnumerator = integerEnumerator;
			Expression = Expression.Constant(this);

			Provider = new MyEnumeratorQueryProvider2<TType>(this);
		}

		/// <summary>
		/// Filters out objects, which do not match the condition 
		/// </summary>
		/// <param name="integerEnumerator">the base enumerator</param>
		/// <param name="expression">condition</param>
		public MyEnumeratorQueryable2(IEnumerator<TType> integerEnumerator, MethodCallExpression? expression)

		{
			_myIntegerEnumerator = new MyConditionalEnumerator<TType>(integerEnumerator, expression);
			Expression = Expression.Constant(this);

			Provider = new MyEnumeratorQueryProvider2<TType>(this);
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

		public void Dispose()
		{
			_myIntegerEnumerator.Dispose();
		}

		public Type ElementType => typeof(TType);
		public Expression Expression { get; }
		public IQueryProvider Provider { get; }
	}
}