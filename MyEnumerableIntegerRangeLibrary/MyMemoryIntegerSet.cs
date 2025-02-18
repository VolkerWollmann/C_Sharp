using System.Collections;
using System.Threading;

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

		#region IMyIntegerSet
		public void Dispose()
        {
            
        }

		#endregion

		public int GetNextIndex(int i)
		{
			// simulate time-consuming operation
			Thread.Sleep(100);
            
			int result = i + 1;
			if (result < _set.Count)
				return result;
            
			return -1;
		}

		public int GetValueAtIndex(int i)
		{
			return _set[i];
		}

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
        }
        #endregion

    }
}
