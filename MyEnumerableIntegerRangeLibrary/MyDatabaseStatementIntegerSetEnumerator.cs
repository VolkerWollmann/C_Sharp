﻿using System.Collections;

namespace C_Sharp.Language.MyEnumerableIntegerRangeLibrary
{
	/// <summary>
	/// Simulate a source, which is worth to be encapsulated for lazy linq queries.
	/// </summary>
	
	public class MyDatabaseStatementIntegerSetEnumerator : IEnumerator<int>
	{
		private readonly MyDatabaseStatementIntegerSet _myDatabaseStatementIntegerSet;

		#region IEnumerator<int>
		int _index = -1;

		public void Dispose()
		{
		}

		public bool MoveNext()
		{
			_index = _myDatabaseStatementIntegerSet.GetNextIndex(_index);
			return _index > 0;
		}

		public void Reset()
		{
			_index = -1;
		}

		public int Current => _myDatabaseStatementIntegerSet.GetValueAtIndex(_index);

		object IEnumerator.Current => Current;

		#endregion

		#region Constructor

		public MyDatabaseStatementIntegerSetEnumerator(MyDatabaseStatementIntegerSet set)
		{
			_myDatabaseStatementIntegerSet = set;
		}
		#endregion


	}
}