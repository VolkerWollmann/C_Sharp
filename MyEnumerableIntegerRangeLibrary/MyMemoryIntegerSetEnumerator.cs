using System.Collections;

namespace MyEnumerableIntegerRangeLibrary
{
	public class MyMemoryIntegerSetEnumerator: IEnumerator<int>
	{
		private readonly MyMemoryIntegerSet _set;
		int _index = -1;
		
		#region IEnumerator<int>
		public void Dispose()
		{
		}

		public bool MoveNext()
		{
			_index =  _set.GetNextIndex(_index);
			return _index >= 0;
		}

		public void Reset()
		{
			_index = -1;
		}

		public int Current => _set.GetValueAtIndex(_index);

		object IEnumerator.Current => Current;

		#endregion

		public MyMemoryIntegerSetEnumerator(MyMemoryIntegerSet set)
		{
			this._set = set;
			Reset();
		}
	}
}
