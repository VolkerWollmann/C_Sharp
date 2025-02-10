using System.Collections;

namespace MyEnumerableIntegerRangeLibrary
{
	/// <summary>
	/// Simulate a source, which is worth to be encapsulated for lazy linq queries.
	/// </summary>

	public class MyOptimizedDatabaseStatementIntegerSetEnumerator : IEnumerator<int>
	{
		private readonly MyOptimizedDatabaseStatementIntegerSet _myDatabaseStatementIntegerSet;
		private readonly string _whereClause;

		#region IEnumerator<int>
		int _index = -1;

		public void Dispose()
		{
		}

		public bool MoveNext()
		{
			_index = _myDatabaseStatementIntegerSet.GetNextIndex(_index,_whereClause);
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

		// ReSharper disable once ConvertToPrimaryConstructor
		public MyOptimizedDatabaseStatementIntegerSetEnumerator(MyDatabaseStatementIntegerSetEnumerator enumerator, string whereClause)
		{
			_myDatabaseStatementIntegerSet = new MyOptimizedDatabaseStatementIntegerSet(enumerator._myDatabaseStatementIntegerSet);
			_whereClause = whereClause;
		}
		
		#endregion


	}
}