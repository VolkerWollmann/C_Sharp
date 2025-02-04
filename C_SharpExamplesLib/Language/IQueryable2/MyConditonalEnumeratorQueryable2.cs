using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using C_Sharp.Language.IQueryable;
using C_Sharp.Language.MyEnumerableIntegerRangeLibrary;

namespace C_Sharp.Language.IQueryable2
{

	public class MyConditonalEnumeratorQueryable2<TOutputType> : IQueryable<TOutputType>
	{
		private MyConditionalEnumerator  _myIntegerEnumerator;

		public MyConditonalEnumeratorQueryable2(IEnumerator<int> integerEnumerator)
		{
			
			_myIntegerEnumerator = new MyConditionalEnumerator(integerEnumerator,null);
			Expression = Expression.Constant(this);

			Provider = new MyConditonalEnumeratorQueryProvider2<TOutputType>(this);
		}

		public MyConditonalEnumeratorQueryable2(IEnumerator<int> integerEnumerator, MethodCallExpression? expression)
			
		{
			_myIntegerEnumerator = new MyConditionalEnumerator(integerEnumerator, expression);
			Expression = Expression.Constant(this);

			Provider = new MyConditonalEnumeratorQueryProvider2<TOutputType>(this);
		}

		public IEnumerator<TOutputType> GetEnumerator()
		{
			if (typeof(TOutputType) != typeof(int))
				throw new NotImplementedException();

			return (IEnumerator<TOutputType>)_myIntegerEnumerator;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _myIntegerEnumerator;
		}

		public Type ElementType => typeof(TOutputType);
		public Expression Expression { get; }
		public IQueryProvider Provider { get; }
	}
}