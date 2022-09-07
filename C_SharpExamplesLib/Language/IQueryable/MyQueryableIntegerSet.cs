using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace C_Sharp.Language.IQueryable
{
    public class MyQueryableIntegerSet : IQueryable<int>
    {
        public MyIntegerSet MyIntegerSet;

        public IEnumerator<int> GetEnumerator()
        {
            // if expression is only a MyQueryableIntegerSet,
            // the expression is simple enough to return only the enumerator of MyIntegerSet
            if (Expression is ConstantExpression constantExpression)
            {
                if (constantExpression.Value is MyQueryableIntegerSet myQueryableIntegerSet)
                    return MyIntegerSet;
            }

            return (Provider.Execute<IEnumerable<int>>(Expression)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            // if expression is only a MyQueryableIntegerSet,
            // the expression is simple enough to return only the enumerator of MyIntegerSet
            if (Expression is ConstantExpression constantExpression)
            {
                if (constantExpression.Value is MyQueryableIntegerSet myQueryableIntegerSet)
                    return MyIntegerSet;
            }
            return (Provider.Execute<IEnumerable>(Expression)).GetEnumerator();
        }

        public Expression Expression { get; }
        public Type ElementType { get; }
        public IQueryProvider Provider { get; }

        #region Constructors
        public MyQueryableIntegerSet(MyIntegerSet myIntegerSet)
        {
            MyIntegerSet = myIntegerSet;
            Provider = new MyQueryableIntegerSetQueryProvider(this);
            Expression = Expression.Constant(this);
        }

        /// <summary> 
        /// This constructor is called by Provider.CreateQuery(). 
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="expression"></param>
        /// <param name="integerSet"></param>
        public MyQueryableIntegerSet(MyIntegerSet integerSet, MyQueryableIntegerSetQueryProvider provider, Expression expression):
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
        #endregion
    }
}
