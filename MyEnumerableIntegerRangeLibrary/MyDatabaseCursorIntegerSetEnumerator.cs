using System.Collections;
using Microsoft.Data.SqlClient;

namespace MyEnumerableIntegerRangeLibrary
{
	/// <summary>
	/// Simulate a source, which is worth to be encapsulated for lazy linq queries.
	/// </summary>

	public class MyDatabaseCursorIntegerSetEnumerator : IEnumerator<int>
	{
		internal readonly MyDatabaseCursorIntegerSet MyDatabaseCursorIntegerSet;
		private SqlDataReader? _reader;

		#region IEnumerator<int>
		public void Dispose()
		{
			_reader?.Close();
		}

		private int _currentValue = -1;
		public bool MoveNext()
		{
			bool moveNextResult = false;
			//# the null-coalescing assignment 
			_reader ??= MyDatabaseCursorIntegerSet.GetReader();
			if (_reader != null)
			{
				moveNextResult = _reader.Read();
				if (moveNextResult)
					_currentValue = _reader.GetInt32(0);
			}

			return moveNextResult;
		}

		public void Reset()
		{
			if (_reader != null)
				_reader.Close();
		}


		public int Current => _currentValue;

		object IEnumerator.Current => _currentValue;

		#endregion

		#region Constructor

		public MyDatabaseCursorIntegerSetEnumerator(MyDatabaseCursorIntegerSet set)
		{
			MyDatabaseCursorIntegerSet = set;
			Reset();
		}
		#endregion


	}
}