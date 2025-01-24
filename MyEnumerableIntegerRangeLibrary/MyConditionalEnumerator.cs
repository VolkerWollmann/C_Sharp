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

	public class MyConditionalEnumerator : IEnumerator<int>
	{
		private readonly IEnumerator<int> _myBaseEnumerator;
		private readonly Expression _whereExpression;

		#region IEnumerator<int>
		public void Dispose()
		{
		}

		public bool MoveNext()
		{
			return _myBaseEnumerator.MoveNext();
		}

		public void Reset()
		{
			_myBaseEnumerator.Reset();
		}

		public int Current => _myBaseEnumerator.Current;

		object IEnumerator.Current => Current;

		#endregion

		#region Constructor

		public MyConditionalEnumerator(IEnumerator<int> enumerator, Expression whereExpression)
		{
			_myBaseEnumerator = enumerator;
			_whereExpression = whereExpression;
		}
		#endregion


	}
}