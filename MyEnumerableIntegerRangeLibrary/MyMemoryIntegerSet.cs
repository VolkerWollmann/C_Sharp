using System.Collections;

namespace MyEnumerableIntegerRangeLibrary
{
    /// <summary>
    /// Simulate a source, which is worth to be encapsulated for lazy linq queries.
    /// </summary>
    public class MyMemoryIntegerSet : IMyIntegerSet
    {
        #region IntegerRangeData
        
        private readonly List<int> _set;
        private int _i;

        #endregion

        #region IEnumerator<int>
        public void Dispose()
        {
            
        }

        /// <summary>
        /// Simulate time-consuming generation of next element
        /// </summary>
        /// <returns>next value</returns>
        public bool MoveNext()
        {
            _i = _i + 1;
            Thread.Sleep(100);

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

		#region Constructor

		public MyMemoryIntegerSet(List<int> set)
        {
            _set = set;
            Reset();

        }
        #endregion

    }
}
