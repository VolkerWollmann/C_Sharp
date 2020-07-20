using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace C_Sharp
{
	/// <summary>
	/// #IEnumrable #IEnumerator<int>
	/// returns the number 1,2,3, ... not 0(zero)
	/// </summary>
	public class MyInteger : IEnumerable, IEnumerator<int>
	{
		int i;
		#region IEnumerator<int>
		public int Current
		{
			get
			{
				return i;
			}

			private set
			{
				i = value;
			}
		}

		object IEnumerator.Current => Current;

		protected virtual void Dispose(bool all)
		{
			Current = 0;
		}
		public void Dispose()
		{
			Dispose(true);
		}

		public bool MoveNext()
		{
			i = i + 1;
			return true;
		}

		public void Reset()
		{
			i = 0;
		}

		#endregion

		#region IEnumerable
		public IEnumerator GetEnumerator()
		{
			return this;
		}
		#endregion

		public static void Test()
		{
			MyInteger myInteger = new MyInteger();
			foreach( int i in myInteger )
			{
				if (i > 4)
					break;
			}
		}
	}
}
