using System.Collections;

namespace C_Sharp.Language.MyEnumerableIntegerRangeLibrary
{
	/// <summary>
	/// Simulate a source, which is worth to be encapsulated for lazy linq queries.
	/// </summary>
	
	public class MyDatabaseStatementIntegerSetEnumerator : IEnumerator<int>
	{
		private readonly MyDatabaseStatementIntegerSet _myDatabaseStatementIntegerSet;

		#region IEnumerator<int>
		public void Dispose()
		{
		}

		public bool MoveNext()
		{
			return _myDatabaseStatementIntegerSet.MoveNext();
		}

		public void Reset()
		{
			_myDatabaseStatementIntegerSet.Reset();
		}

		public int Current => _myDatabaseStatementIntegerSet.Current;

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