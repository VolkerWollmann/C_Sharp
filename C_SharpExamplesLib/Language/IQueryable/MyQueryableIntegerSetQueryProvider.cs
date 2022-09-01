using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp.Language.IQueryable
{
    public class MyQueryableIntegerSetQueryProvider : IQueryProvider
    {
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
            throw new NotImplementedException();
        }
    }
}
