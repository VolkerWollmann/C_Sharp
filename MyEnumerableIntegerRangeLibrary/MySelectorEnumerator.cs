using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Extensibility;

namespace C_Sharp.Language.MyEnumerableIntegerRangeLibrary
{
	/// <summary>
	/// Enumerate base enumerator with a cast function
	/// </summary>
	/// <typeparam name="TResult"></typeparam>
	/// <typeparam name="TParameter"></typeparam>
	public class MySelectorEnumerator<TResult, TParameter> : IEnumerator<TResult>
	{
		private readonly IEnumerator<TParameter> _baseEnumerator;
		private readonly Func<TParameter, TResult> _castFunction;

		public MySelectorEnumerator(IEnumerator<TParameter> baseEnumerator, Expression? expression)
		{
			_baseEnumerator = baseEnumerator;

			if (expression != null)
			{
				if (expression is UnaryExpression unaryExpression)
                {
                    _castFunction = (Func<TParameter, TResult>)((LambdaExpression)unaryExpression.Operand).Compile();
                    //Func<int, bool> compiledExpression = (Func<int, bool>)_lambdaExpression.Compile();
                    //_lambdaExpression =
                    //    (LambdaExpression)((UnaryExpression)(methodCallExpression.Arguments[1])).Operand;
                }

			}
		}

		private TResult ApplyCast()
		{
            return _castFunction(_baseEnumerator.Current);
		}
		public TResult Current => ApplyCast();
		object IEnumerator.Current => Current;
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
