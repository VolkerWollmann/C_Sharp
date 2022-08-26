using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language.IQueryable
{

    /// <summary>
    /// returns the number 1, ...., 10
    ///
    /// see : https://putridparrot.com/blog/creating-a-custom-linq-provider/
    /// </summary>
    public class MyEnumerableIntegerRange : IEnumerator<int>, IEnumerable<int>
    {
        private static int _counter=1;

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

        #region IEnumerator<int>

        public IEnumerator<int> GetEnumerator()
        {
            Reset();
            return this;
        }
        #endregion

        #region IEnumerable<int>
        IEnumerator IEnumerable.GetEnumerator()
        {
            Reset();
            return this;
        }

        #endregion

        #region Constructor
        private MyEnumerableIntegerRange(string name)
        {
            Name = name;
            _range = new List<int>();
            _i = 0;

        }

        public MyEnumerableIntegerRange(int start, int range, string name) : this(name)
        {
            Start = start;
            Range = range;
            int j = Start;
            while (j <= Start + Range)
            {
                _range.Add(j++);
            }
        }

        public MyEnumerableIntegerRange(int start, int range) :
            this(start, range, "MyIntegerRange" + _counter++)
        {

        }

        #endregion

    }

}
