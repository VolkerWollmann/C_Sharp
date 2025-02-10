using System.Collections;
using Microsoft.Data.SqlClient;

namespace MyEnumerableIntegerRangeLibrary
{
	/// <summary>
	/// Simulate a source, which is worth to be encapsulated for lazy linq queries.
	/// </summary>

	public class MyOptimizedDatabaseCursorIntegerSetEnumerator : IEnumerator<int>
	{
		private readonly MyOptimizedDatabaseCursorIntegerSet _myOptimizedDatabaseCursorIntegerSet;
		private SqlDataReader? _reader;
		private readonly string _whereClause;

		#region IEnumerator<int>
		public void Dispose()
		{
			_reader?.Close();
		}

		private int _currentValue = -1;
		public bool MoveNext()
		{
			bool wo = false;
			_reader ??= _myOptimizedDatabaseCursorIntegerSet.GetReader(_whereClause);
			if (_reader != null)
			{
				wo = _reader.Read();
				if (wo)
					_currentValue = _reader.GetInt32(0);
			}

			return wo;
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

		public MyOptimizedDatabaseCursorIntegerSetEnumerator(MyDatabaseCursorIntegerSetEnumerator enumerator, string whereClause)
		{
			_myOptimizedDatabaseCursorIntegerSet = new MyOptimizedDatabaseCursorIntegerSet(enumerator.MyDatabaseCursorIntegerSet);
			_whereClause = whereClause;
			Reset();
		}
		#endregion


	}
}