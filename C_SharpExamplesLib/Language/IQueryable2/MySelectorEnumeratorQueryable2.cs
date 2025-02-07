using System.Collections;
using System.Linq.Expressions;
using C_Sharp.Language.MyEnumerableIntegerRangeLibrary;
using C_SharpExamplesLib.Language.IQueryable2;

namespace C_Sharp.Language.IQueryable2
{
    public class MySelectorEnumeratorQueryable2<TResultType, TBaseType> : IMyDisposeQueryable<TResultType>
    {
        private MySelectorEnumerator<TResultType, TBaseType> mySelectorEnumerator;

        public MySelectorEnumeratorQueryable2(MySelectorEnumerator<TResultType, TBaseType> selectorEnumerator )
        {
            mySelectorEnumerator = selectorEnumerator;
            Expression = Expression.Constant(this);

            Provider = new MySelectorEnumeratorQueryProvider2<TResultType, TBaseType>(this);
        }
        public Type ElementType => typeof(TResultType);

        public Expression Expression { get; }

        public IQueryProvider Provider { get; }

		public void Dispose()
		{
			mySelectorEnumerator.Dispose();
		}

		public IEnumerator<TResultType> GetEnumerator()
        {
            return mySelectorEnumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return mySelectorEnumerator;
        }
    }
}
