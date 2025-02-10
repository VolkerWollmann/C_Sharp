using System.Collections;
using Microsoft.Data.SqlClient;

namespace C_Sharp.Language.MyEnumerableIntegerRangeLibrary
{
	/// <summary>
	/// Simulate a source, which is worth to be encapsulated for lazy linq queries.
	/// </summary>

	public class MyDatabaseCursorIntegerSetEnumerator : IEnumerator<int>
	{
		internal readonly MyDatabaseCursorIntegerSet _myDatabaseCursorIntegerSet;
		private SqlDataReader? _reader;

		#region IEnumerator<int>
		public void Dispose()
		{
			_reader?.Close();
		}

		private int _currentValue = -1;
		public bool MoveNext()
		{
			bool wo = false;
			if (_reader == null )
				_reader = _myDatabaseCursorIntegerSet.GetReader();
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


		public int Current
		{
			get
			{
				return _currentValue;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return _currentValue;
			}
		}

		#endregion

		#region Constructor

		public MyDatabaseCursorIntegerSetEnumerator(MyDatabaseCursorIntegerSet set)
		{
			_myDatabaseCursorIntegerSet = set;
			Reset();
		}
		#endregion


	}
}