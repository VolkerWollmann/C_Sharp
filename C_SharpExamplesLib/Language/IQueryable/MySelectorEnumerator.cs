﻿using System.Collections;
using System.Linq.Expressions;

namespace C_SharpExamplesLib.Language.IQueryable
{
	/// <summary>
	/// Enumerate base enumerator with a cast function
	/// </summary>
	/// <typeparam name="TResult"></typeparam>
	/// <typeparam name="TParameter"></typeparam>
	public class MySelectorEnumerator<TResult, TParameter> : IEnumerator<TResult>
	{
		private readonly IEnumerator<TParameter> _baseEnumerator;
		private readonly Func<TParameter, TResult>? _castFunction;

		public MySelectorEnumerator(IEnumerator<TParameter> baseEnumerator, Expression? expression)
		{
			_baseEnumerator = baseEnumerator;

			if (expression != null)
			{
				if (expression is MethodCallExpression methodCallExpression)
                {
                    LambdaExpression lambda =
                        (LambdaExpression) ((UnaryExpression) methodCallExpression.Arguments[1]).Operand;
                    _castFunction = (Func<TParameter, TResult>)lambda.Compile();
                }
                else
                {
                    throw new ArgumentException("Expression is no method call expression");
                }
			}
		}

		public MySelectorEnumerator(IEnumerator<TParameter> baseEnumerator, Func<TParameter, TResult> castFunction)
		{
			_baseEnumerator = baseEnumerator;
			_castFunction = castFunction;
		}


		private TResult ApplyCast()
		{
            return _castFunction!(_baseEnumerator.Current);
		}
		public TResult Current => ApplyCast();
		object IEnumerator.Current => Current!;
		public bool MoveNext()
		{
			return _baseEnumerator.MoveNext();
		}
		public void Reset()
		{
			_baseEnumerator.Reset();
		}
		public void Dispose()
		{
			_baseEnumerator.Dispose();
		}
	}
}
