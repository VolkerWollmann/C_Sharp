using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using C_Sharp.Language.MyEnumerableIntegerRangeLibrary;

namespace C_Sharp.Language.IQueryable
{
    public class MyQueryableIntegerSet : IQueryable<int>
    {
        private MyIntegerSet _myIntegerSet;

        public IEnumerator<int> GetEnumerator()
        {
            return (Provider.Execute<IEnumerable<int>>(Expression)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (Provider.Execute<IEnumerable>(Expression)).GetEnumerator();
        }

        public Expression Expression { get; }
        public Type ElementType { get; }
        public IQueryProvider Provider { get; }

        #region IList<int>
        public List<int> ToList()
        {
            List<int> result = new List<int>();

            _myIntegerSet.Reset();
            while (_myIntegerSet.MoveNext())
            {
                result.Add(_myIntegerSet.Current);
            }

            return result;
        }
        #endregion

        public MyIntegerSet GetFilteredSet(LambdaExpression lambdaExpression)
        {
            return _myIntegerSet.GetFilteredSet(lambdaExpression);
        }

        #region Constructors
            public MyQueryableIntegerSet(MyIntegerSet myIntegerSet)
        {
            _myIntegerSet = myIntegerSet;
            Provider = new MyQueryableIntegerSetQueryProvider(this);
            Expression = Expression.Constant(this);
        }

        /// <summary> 
        /// This constructor is called by Provider.CreateQuery(). 
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="expression"></param>
        /// <param name="integerSet"></param>
        private MyQueryableIntegerSet(MyIntegerSet integerSet, MyQueryableIntegerSetQueryProvider provider, Expression expression):
            this(integerSet)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            if (!typeof(IQueryable<int>).IsAssignableFrom(expression.Type))
            {
                throw new ArgumentOutOfRangeException(nameof(expression));
            }

            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            Expression = expression;
        }

        public MyQueryableIntegerSet CreateMyQueryableIntegerSet(MyQueryableIntegerSetQueryProvider provider,
            Expression expression)
        {
            return new MyQueryableIntegerSet(this._myIntegerSet, provider, expression);
        }
        #endregion
    }
}
