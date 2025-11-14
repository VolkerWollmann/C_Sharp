using System.Collections;

namespace MyEnumerableIntegerRangeLibrary
{
    /// <summary>
    /// Simulate a source, which is worth to be encapsulated for lazy linq queries.
    /// </summary>

    public class MyOptimizedDatabaseStatementIntegerSetEnumerator(
        MyDatabaseStatementIntegerSetEnumerator enumerator,
        string whereClause)
        : IEnumerator<int>
    {
        private readonly MyOptimizedDatabaseStatementIntegerSet _myDatabaseStatementIntegerSet = new(enumerator.MyDatabaseStatementIntegerSet);

        #region IEnumerator<int>
        int _index = -1;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            _index = _myDatabaseStatementIntegerSet.GetNextIndex(_index, whereClause);
            return _index > 0;
        }

        public void Reset()
        {
            _index = -1;
        }

        public int Current => _myDatabaseStatementIntegerSet.GetValueAtIndex(_index);

        object IEnumerator.Current => Current;

        #endregion
    }
}