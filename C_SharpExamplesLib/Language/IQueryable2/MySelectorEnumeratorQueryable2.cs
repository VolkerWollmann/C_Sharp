using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using C_Sharp.Language.IQueryable;
using C_Sharp.Language.MyEnumerableIntegerRangeLibrary;
using C_SharpExamplesLib.Language.IQueryable2;

namespace C_Sharp.Language.IQueryable2
{
    public class MySelectorEnumeratorQueryable2<TResultType, TBaseType> : IQueryable<TResultType>
    {
        private MySelectorEnumerator<TResultType, TBaseType> mySelectorEnumerator;

        public MySelectorEnumeratorQueryable2(MySelectorEnumerator<TResultType, TBaseType> selectorEnumerator, MethodCallExpression? expression)

        {
            mySelectorEnumerator = selectorEnumerator;
            Expression = Expression.Constant(this);

            Provider = new MySelectorEnumeratorQueryProvider2<TResultType, TBaseType>(this);
        }
        public Type ElementType => typeof(TResultType);

        public Expression Expression { get; }

        public IQueryProvider Provider { get; }

        public IEnumerator<TResultType> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
