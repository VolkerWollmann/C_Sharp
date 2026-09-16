using System.Collections;

namespace MyEnumerableIntegerRangeLibrary
{
    /// <summary>
    /// Generic counterpart of <see cref="MyMemoryIntegerSetEnumerator"/>
    /// </summary>
    /// <typeparam name="TType">type of the elements</typeparam>
    public class MyMemorySetEnumerator<TType> : IEnumerator<TType>
    {
        private readonly MyMemorySet<TType> _set;
        int _index = -1;

        #region IEnumerator<TType>
        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            _index = _set.GetNextIndex(_index);
            return _index >= 0;
        }

        public void Reset()
        {
            _index = -1;
        }

        public TType Current => _set.GetValueAtIndex(_index);

        object IEnumerator.Current => Current!;

        #endregion

        public MyMemorySetEnumerator(MyMemorySet<TType> set)
        {
            _set = set;
            Reset();
        }
    }
}
