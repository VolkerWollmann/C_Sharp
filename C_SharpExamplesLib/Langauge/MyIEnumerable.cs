using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace C_Sharp
{
	/// <summary>
	/// #IEnumrable<int> #IEnumerator<int> #IQueryable<int>
	/// returns the number 1, ...., 10
	/// </summary>
	public class MyInteger : IEnumerable<int>, IEnumerator<int>, IQueryable<int>
	{
		private int i = 0;

		#region IEnumerator<int>
		public int Current => i;

		object IEnumerator.Current => this;

		// required by Dispose
		protected virtual void Dispose(bool b) { }

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

		#region IEnumerable<int>
		public IEnumerator<int> GetEnumerator()
		{
			return this;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}
		#endregion

		#region IQueryable<int>

		public Expression Expression => this.AsQueryable<int>().Expression;

		public Type ElementType => typeof(int);

		public IQueryProvider Provider =>  (IQueryProvider)this.AsQueryable<int>();

		#endregion

		public static void Test()
		{
			MyInteger myInteger = new MyInteger();
			foreach( int i in myInteger )
			{
				if (i > 5)
					break;
			}

			//does not work
			//myInteger.Where(i => (i < 5)).ToList();
		}
	}
}
