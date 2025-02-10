using System.Collections;

namespace MyEnumerableIntegerRangeLibrary
{
	public class MyMemoryIntegerSetEnumerator(MyMemoryIntegerSet set) : IEnumerator<int>
	{
		#region IEnumerator<int>
		public void Dispose()
		{
		}

		public bool MoveNext()
		{
			return set.MoveNext();
		}

		public void Reset()
		{
			set.Reset();
		}

		public int Current => set.Current;

		object IEnumerator.Current => Current;

		#endregion

		#region Constructor

		#endregion


	}
}
