using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp.Language.MyEnumerableIntegerRangeLibrary
{
	public class MyMemoryIntegerSetEnumerator : IEnumerator<int>
	{
		private readonly MyMemoryIntegerSet _myMemoryIntegerSet;

		#region IEnumerator<int>
		public void Dispose()
		{
		}

		public bool MoveNext()
		{
			return _myMemoryIntegerSet.MoveNext();
		}

		public void Reset()
		{
			_myMemoryIntegerSet.Reset();
		}

		public int Current => _myMemoryIntegerSet.Current;

		object IEnumerator.Current => Current;

		#endregion

		#region Constructor

		public MyMemoryIntegerSetEnumerator(MyMemoryIntegerSet set)
		{
			_myMemoryIntegerSet = set;
		}
		#endregion


	}
}
