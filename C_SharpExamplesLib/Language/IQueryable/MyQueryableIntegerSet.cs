using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp.Language.IQueryable
{
    public class MyQueryableIntegerSet : IEnumerator<int>,  IQueryable<int>
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

        #region IQueryable<int>
        public Expression Expression { get; }
        public Type ElementType { get; }
        public IQueryProvider Provider { get; }

        private IEnumerator<int> GetResetedEnumerator()
        {
            Reset();
            return this;
        }
        public IEnumerator<int> GetEnumerator()
        {
            return GetResetedEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Constructor

        public MyQueryableIntegerSet(List<int> set)
        {
            _set = set;
            Reset();

            Provider = new MyQueryableIntegerSetQueryProvider();
            Expression = Expression.Constant(this);
        }
        #endregion

    }
}
