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
    public class MyIntegerSet : IMyIntegerSet
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

        object IEnumerator.Current => ((IEnumerator<int>)this).Current;
        #endregion

        #region Helper methods

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

            return new MyIntegerSet(result);
        }
        
        #endregion

        #region Constructor

        public MyIntegerSet(List<int> set)
        {
            _set = set;
            Reset();

        }
        #endregion

    }
}
