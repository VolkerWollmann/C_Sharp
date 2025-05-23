﻿using System.Collections;
using System.Linq.Expressions;

namespace C_SharpExamplesLib.Language.IQueryable
{
	/// <summary>
	/// Simulate a source, which is worth to be encapsulated for lazy linq queries.
	/// </summary>

	public class MyConditionalEnumerator<TType> : IEnumerator<TType>
	{
		private readonly IEnumerator<TType> _myBaseEnumerator;
		private readonly LambdaExpression _lambdaExpression;

		#region IEnumerator<int>
		public void Dispose()
		{
			_myBaseEnumerator.Dispose();
		}

		private bool MoveNextConditional()
		{ 
			bool baseEnumeratorMoveResult = _myBaseEnumerator.MoveNext();
			
			Func<TType, bool> compiledExpression = (Func<TType, bool>)_lambdaExpression.Compile();

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

		public TType Current => _myBaseEnumerator.Current;

		object IEnumerator.Current => Current!;

		#endregion

		#region Constructor

		public MyConditionalEnumerator(IEnumerator<TType> enumerator, Expression? expression)
		{
			_myBaseEnumerator = enumerator;

			if (expression is MethodCallExpression methodCallExpression)
			{
				// apply lambda/where on the items and get a filtered MyIntegerSet
				// get lambda expression
				_lambdaExpression =
					(LambdaExpression)((UnaryExpression)(methodCallExpression.Arguments[1])).Operand;
			}
			else if (expression is UnaryExpression unaryExpression)
			{
				_lambdaExpression = (LambdaExpression)unaryExpression.Operand;
			}
			else
			{
				throw new ArgumentException(
					"whereExpression must be a method call expression with a lambda expression as the second argument.");
			}

		}
		#endregion
	}
}