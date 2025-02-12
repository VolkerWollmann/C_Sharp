using System.Collections;

namespace MyEnumerableIntegerRangeLibrary
{
    /// <summary>
    /// MyEnumerableIntegerRangeEnumerator
    /// Needed separately for e.g. with two or more concurrent IEnumerators needed
    /// </summary>
    public class MyEnumerableIntegerRangeEnumerator(MyEnumerableIntegerRange range) : IEnumerator<int>
    {
	    private int _i = -1;
        #region IEnumerator<int>
        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            _i = _i + 1;
            return _i < range.Count;
        }

        public void Reset()
        {
            _i = -1;
        }

        public int Current => range.ValueAt(_i);

        object IEnumerator.Current => Current;

        #endregion
    }

}
