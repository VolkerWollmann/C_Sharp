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
		private readonly Expression? _whereExpression = null;
		private readonly LambdaExpression? _lambdaExpression =null;

		#region IEnumerator<int>
		public void Dispose()
		{
		}

		internal bool MoveNextConditional()
		{ 
			bool baseEnumeratorMoveResult = _myBaseEnumerator.MoveNext();
			if (_lambdaExpression == null)
				return baseEnumeratorMoveResult;
			Func<int, bool> compiledExpression = (Func<int, bool>)_lambdaExpression.Compile();

			while (baseEnumeratorMoveResult && !compiledExpression(Current))
			{
				baseEnumeratorMoveResult = _myBaseEnumerator.MoveNext();
			}

			return baseEnumeratorMoveResult;
		}
		public bool MoveNext()
		{
			return MoveNextConditional();
		}

		public void Reset()
		{
			_myBaseEnumerator.Reset();
		}

		public int Current => _myBaseEnumerator.Current;

		object IEnumerator.Current => Current;

		#endregion

		#region Constructor

		public MyConditionalEnumerator(IEnumerator<int> enumerator, MethodCallExpression? whereExpression)
		{
			_myBaseEnumerator = enumerator;
			_whereExpression = whereExpression;
			if (whereExpression != null)
			{
				// apply lambda/where on the items and get a filtered MyIntegerSet
				// get lambda expression
				_lambdaExpression = (LambdaExpression) ((UnaryExpression) (whereExpression.Arguments[1])).Operand;
			}
		}
		#endregion
	}
}