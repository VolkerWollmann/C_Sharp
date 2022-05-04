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
        private static int Counter=1;

        public  string Name { get; set; }
        public int Start { get; set; }
        public int Range { get; set; }
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
            do
            {
                _i = _i + 1;
            } while ((_i < _range.Count) && !EvalQueryExpression());

            return _i < _range.Count;
        }

        public void Reset()
        {
            _i = -1;
        }

        int IEnumerator<int>.Current => _range[_i];

        object IEnumerator.Current => ((IEnumerator<int>) this).Current;

        #endregion

        #region IEnumerable<int>

        public IEnumerator<int> GetEnumerator()
        {
            Reset();
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Reset();
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
                // evaluation should be done by MyIntegerRange
                // expose MyIntegerRange as constant outside
                var expression = Expression.Constant(this);
                Assert.IsNotNull(expression);

                return expression;
            }
        }

        // determines linq types
        public Type ElementType => typeof(int);

        public IQueryProvider Provider => this;

        #endregion

        #region Queryable Extensions Methods

        private bool Any()
        {
            return _range.Count > 0;
        }

        private bool Any(Func<int,bool> condition)
        {
            foreach (int i in this)
            {
                if (condition(i))
                    return true;
            }

            return false;
        }

        private int Sum()
        {
            int sum = 0;
            foreach (int i in this)
            {
                sum = sum + i;
            }
            return sum;
        }

        #endregion

        #region IQueryProvider

        private Expression QueryExpression { get; set; }

        private bool EvalQueryExpression()
        {
            if (QueryExpression == null)
                return true;

            // evaluate where condition for current element
            var result = ExecuteForCurrentElement(QueryExpression);

            return (bool)result;
        }
        public IQueryable CreateQuery(Expression expression)
        {
            MyIntegerRange copy = this.Copy();
            copy.QueryExpression = expression;  // TODO: needs concatenation
            return copy;
        }

        public IQueryable<T> CreateQuery<T>(Expression expression)
        {
            MyIntegerRange copy = this.Copy();
            copy.QueryExpression = expression;  // TODO: needs concatenation
            return (IQueryable <T>)copy;
        }

        public object ExecuteForCurrentElement(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Call)
            {
                MethodCallExpression methodCallExpression = (MethodCallExpression) expression;
                // private implementation of Queryable.Where
                if (methodCallExpression.Method.Name == "Where")
                {
                    if (methodCallExpression.Arguments.Count == 2)
                    {
                        UnaryExpression unaryExpression = (UnaryExpression)methodCallExpression.Arguments[1];
                        List<ParameterExpression> lp = new List<ParameterExpression> { Expression.Parameter(ElementType) };
                        InvocationExpression ie = Expression.Invoke(unaryExpression, lp);
                        var lambdaExpression = Expression.Lambda<Func<int, bool>>(ie, lp);
                        var whereFunction = lambdaExpression.Compile();

                        bool result = whereFunction(((IEnumerator<int>) this).Current);
                        return result;
                    }
                }
            }

            return null;
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
            Start = start;
            Range = range;
            int j = Start;
            while (j <= Start + Range)
            {
                _range.Add(j++);
            }
        }

        public MyIntegerRange(int start, int range) :
            this(start, range, "MIR" + Counter++ )
        {

        }

        #endregion

        #region Copy

        public MyIntegerRange Copy()
        {
            MyIntegerRange copy = new MyIntegerRange(this.Start, this.Range,
                this.Name + "_Copy_" + Counter++);

            return copy;
        }
        #endregion
    }

    public class MyIntegerRangeTest
    {

        public static void Test_IQueryable_as_IEnumerable()
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
        }

        public static void Test_IQueryable()
        {
            MyIntegerRange myIntegerRange = new MyIntegerRange(1, 10);

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

            //does work
            // uses public Expression Expression
            // uses public IQueryable<T> CreateQuery<T>(Expression expression)
            var e = myIntegerRange.Where(i => (i < 5)).ToList();
            Assert.IsNotNull(e);

            //does work lazy evaluation
            // uses public Expression Expression
            // uses public IQueryable<T> CreateQuery<T>(Expression expression)
            var g1 = myIntegerRange.Where(i => (i < 5));
            var g2 = g1.ToList();
            Assert.IsTrue(g2.Count == 4);
        }

        public static void Test_MultipleExpressions()
        {
            //does work lazy evaluation
            //CreateQuery creates a copy  
            //treat more like expressions
            MyIntegerRange myIntegerRange = new MyIntegerRange(1, 10);
            var g1 = myIntegerRange.Where(i => (i < 5));
            var g2 = myIntegerRange.Where(i => (i < 3));

            var g1r = g1.ToList();
            Assert.IsTrue(g1r.Count == 4);
            var g2r = g2.ToList();
            Assert.IsTrue(g2r.Count == 2);
        }


        public static void Test_CascadedExpressions()
        {
            //does work lazy evaluation
            //CreateQuery creates a copy  
            //treat more like expressions
            MyIntegerRange myIntegerRange = new MyIntegerRange(1, 10);
            var g1 = myIntegerRange.Where(i => (i < 5));
            var g2 = g1.Where(i => (i < 3));

            var g1r = g1.ToList();
            Assert.IsTrue(g1r.Count == 4);
            var g2r = g2.ToList();
            Assert.IsTrue(g2r.Count == 2);
        }
    }
}
