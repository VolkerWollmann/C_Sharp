using System.Collections;

namespace MyEnumerableIntegerRangeLibrary
{
	/// <summary>
	/// Simulate a source, which is worth to be encapsulated for lazy linq queries.
	/// </summary>
	
	public class MyDatabaseStatementIntegerSetEnumerator(MyDatabaseStatementIntegerSet set) : IEnumerator<int>
	{
		internal readonly MyDatabaseStatementIntegerSet MyDatabaseStatementIntegerSet = set;

		#region IEnumerator<int>
		int _index = -1;

		public void Dispose()
		{
		}

		public bool MoveNext()
		{
			_index = MyDatabaseStatementIntegerSet.GetNextIndex(_index);
			return _index > 0;
		}

		public void Reset()
		{
			_index = -1;
		}

		public int Current => MyDatabaseStatementIntegerSet.GetValueAtIndex(_index);

		object IEnumerator.Current => Current;

		#endregion

	}
}