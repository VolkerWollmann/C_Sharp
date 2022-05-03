using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
    /// <summary>
    /// <![CDATA[ #IEnumerable<int> #IEnumerator<int> #IQueryable<int> #IQueryProvider ]]>
    /// returns the number 1, ...., 10
    ///
    /// see : https://putridparrot.com/blog/creating-a-custom-linq-provider/
    /// </summary>
    public class
        MyIntegerRange : IEnumerator<int>, IQueryable<int>, IQueryProvider // IQueryable<int> includes IEnumerable<int>
    {
        public  string Name { get; set; }
        private readonly List<int> _range;
        private int _i;

        #region IEnumerator<int>

        // required by Dispose
        protected virtual void Dispose(bool b)
        {
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public bool MoveNext()
        {
            _i = _i + 1;
            return _i < _range.Count;
        }

        public void Reset()
        {
            _i = 0;
        }

        int IEnumerator<int>.Current => _range[_i];

        object IEnumerator.Current => ((IEnumerator<int>) this).Current;

        #endregion

        #region IEnumerable<int>

        public IEnumerator<int> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        #endregion


        #region IQueryable<int>

        // The expression as EnumerableQuery<int>
        // #Expression
        public Expression Expression
        {
            get
            {
                var z = Expression.Constant(this);
                Assert.IsNotNull(z);

                var x = _range.AsQueryable();
                var y = x.Expression;

                return z;

                //return this.Expression;
            }
        }

        // determines linq types
        public Type ElementType => typeof(int);

        public IQueryProvider Provider => this;

        #endregion

        #region Enumerable

        private bool Any()
        {
            return _range.Count > 0;
        }

        private bool Any(Func<int,bool> condition)
        {
            foreach (int i in _range)
            {
                if (condition(i))
                    return true;
            }

            return false;
        }

        private int Sum()
        {
            int sum = _range.Sum();
            return sum;
        }

        #endregion

        #region IQueryProvider

        public IQueryable CreateQuery(Expression expression)
        {
            return new EnumerableQuery<int>(expression);
        }

        public IQueryable<T> CreateQuery<T>(Expression expression)
        {
            return new EnumerableQuery<T>(expression);
        }

        public object Execute(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Call)
            {
                MethodCallExpression methodCallExpression = (MethodCallExpression) expression;
                // private implementation of Enumerable.Any #Any
                if (methodCallExpression.Method.Name == "Any")
                {
                    if (methodCallExpression.Arguments.Count == 1)
                        return Any();
                    
                    if (methodCallExpression.Arguments.Count == 2)
                    {
                        // complie lambda function as condition for any
                        UnaryExpression unaryExpression = (UnaryExpression)methodCallExpression.Arguments[1];
                        List<ParameterExpression> lp = new List<ParameterExpression> {Expression.Parameter(ElementType)};
                        InvocationExpression ie = Expression.Invoke(unaryExpression, lp);
                        var lambdaExpression = Expression.Lambda<Func<int,bool>>(ie, lp);
                        var anyFunction = lambdaExpression.Compile();

                        return Any(anyFunction);
                    }
                }

                if (methodCallExpression.Method.Name == "Sum")
                {
                    return Sum();
                }
            }

            var result = Expression.Lambda(expression).Compile().DynamicInvoke();
            return result;
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return (TResult) Execute(expression);
        }

        #endregion

        #region Constructor

        private MyIntegerRange(string name)
        {
            Name = name;
            _range = new List<int>();
            _i = 0;
        }

        public MyIntegerRange(int start, int range, string name) : this(name)
        {
            int j = start;
            while (j <= start + range)
            {
                _range.Add(j++);
            }
        }

        public MyIntegerRange(int start, int range) :
            this(start, range, "MIR" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second)
        {

        }

            #endregion
    }

    public class MyIntegerRangeTest
    {

        public static void Test()
        {
            // uses public IEnumerator<int> GetEnumerator()
            // uses public bool MoveNext()
            // uses int IEnumerator<int>.Current
            MyIntegerRange myIntegerRange = new MyIntegerRange(1, 10);
            foreach (int i in myIntegerRange)
            {
                if (i > 5)
                    break;
            }

            // myIntegerRange stands at 6
            // uses int IEnumerator<int>.Current
            var a = ((IEnumerator<int>) myIntegerRange).Current;
            Assert.AreEqual(a, 6);

            // uses object IEnumerator.Current
            var b = ((IEnumerator) myIntegerRange).Current;
            Assert.IsNotNull(b);

            // does work
            // uses public IEnumerator<int> GetEnumerator()
            var d = myIntegerRange.ToList();
            Assert.IsNotNull(d);

            // does work
            // uses private any implementation
            var d1 = myIntegerRange.Any();

            // does work
            // uses private conditional any implementation
            var d2 = myIntegerRange.Any(i => i > 5);

            // does work 
            // uses public Expression Expression
            // uses public TResult Execute<TResult>(Expression expression)
            var f = myIntegerRange.Sum();
            Assert.IsTrue(f > 0);

            //does not work
            // uses public Expression Expression
            // uses public IQueryable<T> CreateQuery<T>(Expression expression)
            var e = myIntegerRange.Where(i => (i < 5)).ToList();
            Assert.IsNotNull(e);
        }
    }
}
