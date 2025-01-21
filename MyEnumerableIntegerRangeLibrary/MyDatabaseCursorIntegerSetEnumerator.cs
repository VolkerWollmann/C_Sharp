using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace C_Sharp.Language.MyEnumerableIntegerRangeLibrary
{
	/// <summary>
	/// Simulate a source, which is worth to be encapsulated for lazy linq queries.
	/// </summary>
	
	public class MyDatabaseCursorIntegerSetEnumerator : IEnumerator<int>
	{
		private readonly MyDatabaseCursorIntegerSet _myDatabaseCursorIntegerSet;

		#region IEnumerator<int>
		public void Dispose()
		{
		}

		public bool MoveNext()
		{
			return _myDatabaseCursorIntegerSet.MoveNext();
		}

		public void Reset()
		{
			_myDatabaseCursorIntegerSet.Reset();
		}

		public int Current => _myDatabaseCursorIntegerSet.Current;

		object IEnumerator.Current => Current;

		#endregion

		#region Constructor

		public MyDatabaseCursorIntegerSetEnumerator(MyDatabaseCursorIntegerSet set)
		{
			_myDatabaseCursorIntegerSet = set;
		}
		#endregion


	}
}