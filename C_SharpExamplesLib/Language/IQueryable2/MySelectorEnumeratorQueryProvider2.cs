using System.Linq.Expressions;
using C_Sharp.Language.IQueryable2;

namespace C_SharpExamplesLib.Language.IQueryable2
{
    public class MySelectorEnumeratorQueryProvider2<TResultType, TBaseType> : IQueryProvider
    {
        private MySelectorEnumeratorQueryable2<TResultType, TBaseType> _mySelectorEnumerator;

        public MySelectorEnumeratorQueryProvider2(MySelectorEnumeratorQueryable2<TResultType, TBaseType> selectorEnumerator)
        {
            _mySelectorEnumerator = selectorEnumerator;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
            
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            throw new NotImplementedException();
        }

        public object? Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            throw new NotImplementedException();
        }
    }
}
