using System.Collections;

namespace C_Sharp.Language.MyEnumerableIntegerRangeLibrary
{
    /// <summary>
    /// MyEnumerableIntegerRangeEnumerator
    /// Needed separately for e.g. with two or more concurrent IEnumerators needed
    /// </summary>
    public class MyEnumerableIntegerRangeEnumerator : IEnumerator<int>
    {
        private readonly MyEnumerableIntegerRange _myEnumerableIntegerRange;
        private int _i;
        #region IEnumerator<int>
        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            _i = _i + 1;
            return _i < _myEnumerableIntegerRange.Count;
        }

        public void Reset()
        {
            _i = -1;
        }

        public int Current => _myEnumerableIntegerRange.ValueAt(_i);

        object IEnumerator.Current => Current;

        #endregion

        #region Constructor

        // ReSharper disable once ConvertToPrimaryConstructor
        public MyEnumerableIntegerRangeEnumerator(MyEnumerableIntegerRange range)
        {
            _myEnumerableIntegerRange = range;
            _i = -1;
        }
        #endregion


    }

}
