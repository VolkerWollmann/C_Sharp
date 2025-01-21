using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp.Language.MyEnumerableIntegerRangeLibrary
{
    /// <summary>
    /// Simulate a source, which is worth to be encapsulated for lazy linq queries.
    /// </summary>
    public class MyMemoryIntegerSet : IMyIntegerSet
    {
        Guid guid = Guid.NewGuid();

        #region IntegerRangeData
        
        private readonly List<int> _set;
        private int _i;

        #endregion

        #region IEnumerator<int>
        public void Dispose()
        {
            
        }

        /// <summary>
        /// Simulate time consuming generation of next element
        /// </summary>
        /// <returns>next value</returns>
        public bool MoveNext()
        {
            _i = _i + 1;
            System.Threading.Thread.Sleep(100);

            return _i < _set.Count;
        }

        public void Reset()
        {
            _i = -1;
        }

        public int Current => _set[_i];


        #endregion

        #region IEnumerable<int>
        // bad implementation because only one iterator possible
        public IEnumerator<int> GetEnumerator()
        {
            return new MyMemoryIntegerSetEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MyMemoryIntegerSetEnumerator(this);
        }
        #endregion

        #region IMyIntegerSet

        public IMyIntegerSet GetFilteredSet(LambdaExpression lambdaExpression)
        {
            List<int> result = new List<int>();
            Func<int, bool> compiledExpression = (Func<int, bool>)lambdaExpression.Compile();
            
            Reset();
            while (MoveNext())
            {
                if (compiledExpression(Current))
                    result.Add(Current);
            }

            return new MyMemoryIntegerSet(result);
        }

        public int Sum()
        {
            int sum = 0;
            Reset();
            while (MoveNext())
            {
                sum = sum + Current;
            }

            return sum;
        }

        public virtual bool Any(LambdaExpression lambdaExpression)
        {
            Func<int, bool> compiledExpression = (Func<int, bool>)lambdaExpression.Compile();

            Reset();
            while (MoveNext())
            {
                if (compiledExpression(Current))
                    return true;
            }

            return false;
        }

        public virtual bool Any()
        {
            Reset();
            return MoveNext();
        }

        #endregion

        #region Constructor

            public MyMemoryIntegerSet(List<int> set)
        {
            _set = set;
            Reset();

        }
        #endregion

    }
}
