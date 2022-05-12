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
        MyIntegerRange : IEnumerator<int>, IQueryable<int> // IQueryable<int> includes IEnumerable<int>
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
            _i = _i + 1;
            
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
                Expression expression;
                expression = Expression.Constant(this);
                //expression = this.GetEnumerator();

                Assert.IsNotNull(expression);

                return expression;
            }
        }

        // determines linq types
        public Type ElementType => typeof(int);

        private IQueryProvider _provider;
        public IQueryProvider Provider => _provider;

        #endregion

        #region Queryable Extensions Methods

        internal bool Any()
        {
            return _range.Count > 0;
        }

        internal bool Any(Func<int,bool> condition)
        {
            foreach (int i in this)
            {
                if (condition(i))
                    return true;
            }

            return false;
        }

        internal int Sum()
        {
            int sum = 0;
            foreach (int i in this)
            {
                sum = sum + i;
            }
            return sum;
        }

        #endregion

        #region Constructor

        private MyIntegerRange(string name)
        {
            Name = name;
            _range = new List<int>();
            _i = 0;

            _provider = new MyIntegerRangeIQueryProvider(this);
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

        public MyIntegerRange(MyIntegerRange myIntegerRange)
        {
            Name = myIntegerRange.Name;
            Start = myIntegerRange.Start;
            Range = myIntegerRange.Range;

            _range = new List<int>();
            _i = 0;

            int j = Start;
            while (j <= Start + Range)
            {
                _range.Add(j++);
            }


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

}
