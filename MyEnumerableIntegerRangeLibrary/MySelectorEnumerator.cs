using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEnumerableIntegerRangeLibrary
{
	public class MySelectEnumerator<TypeA, TypeB> : IEnumerator<TypeA>
	{
		private readonly IEnumerator<TypeB> _baseEnumerator;
		private readonly Func<TypeB, TypeA> _castFunction;

		public MySelectEnumerator(IEnumerator<TypeB> baseEnumerator, Expression? expression)
		{
			_baseEnumerator = baseEnumerator;

			if (expression != null)
			{
				if (expression is MethodCallExpression methodCallExpression)
				{
					_castFunction = null;
					//(Func<TypeB, TypeA>) ((object) (methodCallExpression.Arguments[1])).Operand;
				}

			}
		}

		private TypeA ApplyCast()
		{
			return _castFunction(_baseEnumerator.Current);
		}
		public TypeA Current => ApplyCast();
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
