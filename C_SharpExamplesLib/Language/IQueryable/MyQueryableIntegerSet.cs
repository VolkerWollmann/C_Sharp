﻿using System;
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
        public IEnumerator<int> GetEnumerator()
        {
            return (Provider.Execute<IEnumerable<int>>(Expression)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (Provider.Execute<System.Collections.IEnumerable>(Expression)).GetEnumerator();
        }

        public Expression Expression { get; }
        public Type ElementType { get; }
        public IQueryProvider Provider { get; }

        #region Constructors
        public MyQueryableIntegerSet(MyIntegerSet integerSet)
        {
            Provider = new MyQueryableIntegerSetQueryProvider(integerSet);
            Expression = Expression.Constant(this);
        }

        /// <summary> 
        /// This constructor is called by Provider.CreateQuery(). 
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="expression"></param>
        public MyQueryableIntegerSet(MyQueryableIntegerSetQueryProvider provider, Expression expression)
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
