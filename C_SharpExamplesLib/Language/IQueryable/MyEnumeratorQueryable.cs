using System.Collections;
using System.Linq.Expressions;
using C_Sharp.Language.MyEnumerableIntegerRangeLibrary;
using C_SharpExamplesLib.Language.IQueryable;

namespace C_Sharp.Language.IQueryable
{
	public class MyEnumeratorQueryable<TType> : IMyDisposeQueryable<TType>
	{
		private IEnumerator<TType> _myIntegerEnumerator;


		/// <summary>
		/// empty condition : acts like identical list
		/// </summary>
		/// <param name="integerEnumerator">the base enumerator</param>
		public MyEnumeratorQueryable(IEnumerator<TType> integerEnumerator)
		{

			_myIntegerEnumerator = integerEnumerator;
			Expression = Expression.Constant(this);

			Provider = new MyEnumeratorQueryProvider<TType>(this);
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