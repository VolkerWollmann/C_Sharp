using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp.Language.IQueryable
{
    internal class MyQueryableIntegerSetQueryContext
    {
        // Executes the expression tree that is passed to it. 
        internal static object Execute(Expression expression, bool isEnumerable)
        {
            throw new NotImplementedException();
        }
    }

    public class MyQueryableIntegerSetQueryProvider : IQueryProvider
    {
        private MyIntegerSet _integerSet;

        #region private Methods
        // Executes the expression tree that is passed to it. 
        internal static object Execute(Expression expression, bool isEnumerable)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IQueryProvider
            public System.Linq.IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            // TElement must be int
            if (typeof(TElement) != typeof(int)) 
                throw new NotImplementedException();
            return (IQueryable<TElement>)new MyQueryableIntegerSet(this, expression);
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            bool isEnumerable = (typeof(TResult).Name == "IEnumerable`1");

            return (TResult)MyQueryableIntegerSetQueryContext.Execute(expression, isEnumerable);
        }
        #endregion

        #region Construcutor

        public MyQueryableIntegerSetQueryProvider(MyIntegerSet integerSet)
        {
            _integerSet = integerSet;
        }
        #endregion
    }
}
