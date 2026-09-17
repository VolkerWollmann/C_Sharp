using System.Collections;
using Microsoft.Data.SqlClient;

namespace MyEnumerableIntegerRangeLibrary
{
    /// <summary>
    /// Counterpart of <see cref="MyDatabaseCursorIntegerSetEnumerator"/>
    /// </summary>
    public class MyDatabaseCursorAnimalSetEnumerator : IEnumerator<MyAnimal>
    {
        internal readonly MyDatabaseCursorAnimalSet MyDatabaseCursorAnimalSet;
        private SqlDataReader? _reader;

        #region IEnumerator<MyAnimal>
        /// <summary>
        /// closes the reader; the next MoveNext opens a new one,
        /// because the queryables hand out the same enumerator instance again
        /// </summary>
        public void Dispose()
        {
            Reset();
        }

        private MyAnimal? _currentValue;
        public bool MoveNext()
        {
            bool moveNextResult = false;
            //# the null-coalescing assignment
            _reader ??= MyDatabaseCursorAnimalSet.GetReader();
            if (_reader != null)
            {
                moveNextResult = _reader.Read();
                if (moveNextResult)
                    _currentValue = MyDatabaseStatementAnimalSet.ReadAnimal(_reader);
            }

            return moveNextResult;
        }

        public void Reset()
        {
            _reader?.Close();
            _reader = null;
            _currentValue = null;
        }

        public MyAnimal Current => _currentValue ?? throw new InvalidOperationException("Enumeration has not started.");

        object IEnumerator.Current => Current;

        #endregion

        #region Constructor

        public MyDatabaseCursorAnimalSetEnumerator(MyDatabaseCursorAnimalSet set)
        {
            MyDatabaseCursorAnimalSet = set;
            Reset();
        }
        #endregion
    }
}
