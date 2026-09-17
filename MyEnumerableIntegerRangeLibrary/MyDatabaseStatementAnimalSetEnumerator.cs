using System.Collections;

namespace MyEnumerableIntegerRangeLibrary
{
    /// <summary>
    /// Counterpart of <see cref="MyDatabaseStatementIntegerSetEnumerator"/>
    /// </summary>
    public class MyDatabaseStatementAnimalSetEnumerator(MyDatabaseStatementAnimalSet set) : IEnumerator<MyAnimal>
    {
        public readonly MyDatabaseStatementAnimalSet MyDatabaseStatementAnimalSet = set;

        #region IEnumerator<MyAnimal>
        int _index = -1;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            _index = MyDatabaseStatementAnimalSet.GetNextIndex(_index);
            return _index > 0;
        }

        public void Reset()
        {
            _index = -1;
        }

        public MyAnimal Current => MyDatabaseStatementAnimalSet.GetValueAtIndex(_index);

        object IEnumerator.Current => Current;

        #endregion
    }
}
