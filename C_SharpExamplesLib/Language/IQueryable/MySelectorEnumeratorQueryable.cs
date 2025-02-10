using System.Collections;
using System.Linq.Expressions;

namespace C_SharpExamplesLib.Language.IQueryable
{
    public class MySelectorEnumeratorQueryable<TResultType, TBaseType> : IMyDisposeQueryable<TResultType>
    {
        private MySelectorEnumerator<TResultType, TBaseType> _mySelectorEnumerator;

        public MySelectorEnumeratorQueryable(MySelectorEnumerator<TResultType, TBaseType> selectorEnumerator )
        {
            _mySelectorEnumerator = selectorEnumerator;
            Expression = Expression.Constant(this);

            Provider = new MySelectorEnumeratorQueryProvider<TResultType, TBaseType>(this);
        }
        public Type ElementType => typeof(TResultType);

        public Expression Expression { get; }

        public IQueryProvider Provider { get; }

		public void Dispose()
		{
			_mySelectorEnumerator.Dispose();
		}

		public IEnumerator<TResultType> GetEnumerator()
        {
            return _mySelectorEnumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _mySelectorEnumerator;
        }
    }
}
