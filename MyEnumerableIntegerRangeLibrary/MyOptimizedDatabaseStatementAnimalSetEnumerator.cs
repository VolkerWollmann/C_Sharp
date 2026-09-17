using System.Collections;

namespace MyEnumerableIntegerRangeLibrary
{
    /// <summary>
    /// Counterpart of <see cref="MyOptimizedDatabaseStatementIntegerSetEnumerator"/>
    /// </summary>
    public class MyOptimizedDatabaseStatementAnimalSetEnumerator(
        MyDatabaseStatementAnimalSetEnumerator enumerator,
        string whereClause)
        : IEnumerator<MyAnimal>
    {
        private readonly MyOptimizedDatabaseStatementAnimalSet _myDatabaseStatementAnimalSet = new(enumerator.MyDatabaseStatementAnimalSet);

        #region IEnumerator<MyAnimal>
        int _index = -1;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            _index = _myDatabaseStatementAnimalSet.GetNextIndex(_index, whereClause);
            return _index > 0;
        }

        public void Reset()
        {
            _index = -1;
        }

        public MyAnimal Current => _myDatabaseStatementAnimalSet.GetValueAtIndex(_index);

        object IEnumerator.Current => Current;

        #endregion
    }
}
