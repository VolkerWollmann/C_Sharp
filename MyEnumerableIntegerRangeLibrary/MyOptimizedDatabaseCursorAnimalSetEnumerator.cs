using System.Collections;
using Microsoft.Data.SqlClient;

namespace MyEnumerableIntegerRangeLibrary
{
    /// <summary>
    /// Counterpart of <see cref="MyOptimizedDatabaseCursorIntegerSetEnumerator"/>
    /// </summary>
    public class MyOptimizedDatabaseCursorAnimalSetEnumerator : IEnumerator<MyAnimal>
    {
        private readonly MyOptimizedDatabaseCursorAnimalSet _myOptimizedDatabaseCursorAnimalSet;
        private SqlDataReader? _reader;
        private readonly string _whereClause;

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
            _reader ??= _myOptimizedDatabaseCursorAnimalSet.GetReader(_whereClause);
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

        public MyOptimizedDatabaseCursorAnimalSetEnumerator(MyDatabaseCursorAnimalSetEnumerator enumerator, string whereClause)
        {
            _myOptimizedDatabaseCursorAnimalSet = new MyOptimizedDatabaseCursorAnimalSet(enumerator.MyDatabaseCursorAnimalSet);
            _whereClause = whereClause;
            Reset();
        }
        #endregion
    }
}
