using System.Collections;
using System.Linq.Expressions;

namespace C_SharpExamplesLib.Language.IQueryable
{
    public class MySelectorEnumeratorQueryable<TResultType, TBaseType> : IMyDisposeQueryable<TResultType>
    {
        private MySelectorEnumerator<TResultType, TBaseType> mySelectorEnumerator;

        public MySelectorEnumeratorQueryable(MySelectorEnumerator<TResultType, TBaseType> selectorEnumerator )
        {
            mySelectorEnumerator = selectorEnumerator;
            Expression = Expression.Constant(this);

            Provider = new MySelectorEnumeratorQueryProvider<TResultType, TBaseType>(this);
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
