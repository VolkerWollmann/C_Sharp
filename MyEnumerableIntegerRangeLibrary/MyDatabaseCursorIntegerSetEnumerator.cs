using System.Collections;

namespace C_Sharp.Language.MyEnumerableIntegerRangeLibrary
{
	/// <summary>
	/// Simulate a source, which is worth to be encapsulated for lazy linq queries.
	/// </summary>

	public class MyDatabaseCursorIntegerSetEnumerator : IEnumerator<int>
	{
		private readonly MyDatabaseCursorIntegerSet _myDatabaseCursorIntegerSet;
<<<<<<< Updated upstream
=======
		private SqlDataReader? _reader = null;
>>>>>>> Stashed changes

		#region IEnumerator<int>
		public void Dispose()
		{
<<<<<<< Updated upstream
=======
			_reader?.Close();

>>>>>>> Stashed changes
		}

		public bool MoveNext()
		{
<<<<<<< Updated upstream
			return _myDatabaseCursorIntegerSet.MoveNext();
=======
			if (_reader != null)
			{
				var wo = _reader.Read();
				return wo;
			}

			return false;
>>>>>>> Stashed changes
		}

		public void Reset()
		{
<<<<<<< Updated upstream
			_myDatabaseCursorIntegerSet.Reset();
=======
			if (_reader != null)
				_reader.Close();

			_reader = _myDatabaseCursorIntegerSet.GetReader();

		}


		public int Current => (int)Current;

		object IEnumerator.Current
		{
			get
			{
				if (_reader != null) return (object)_reader.GetInt32(0);
				throw new InvalidOperationException("Reader is null or not open.");
			}
>>>>>>> Stashed changes
		}

		public int Current => _myDatabaseCursorIntegerSet.Current;

		object IEnumerator.Current => Current;

		#endregion

		#region Constructor

		public MyDatabaseCursorIntegerSetEnumerator(MyDatabaseCursorIntegerSet set)
		{
			_myDatabaseCursorIntegerSet = set;
<<<<<<< Updated upstream
=======
			Reset();
>>>>>>> Stashed changes
		}
		#endregion


	}
}