using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp.Language.IQueryable
{
    public class MyIntegerSet : IEnumerator<int>
    {
        #region IntegerRangeData
        
        private readonly List<int> _set;
        private int _i;

        #endregion

        #region IEnumerator<int>
        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            _i = _i + 1;

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

        public MyIntegerSet GetFilteredSet(LambdaExpression lambdaExpression)
        {
            List<int> result = new List<int>();
            Func<int, bool> compiledExpression = (Func<int, bool>)lambdaExpression.Compile();
            
            foreach (var item in _set)
            {
                if (compiledExpression(item))
                    result.Add(item);
            }

            return new MyIntegerSet(result);
        }
        public List<int> ToList()
        {
            return _set.ToList();
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
