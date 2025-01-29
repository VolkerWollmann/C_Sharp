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

	public class MyQueryableIntegerSet2<TOutputType> : IQueryable<TOutputType>
	{
		private IMyIntegerSet _myIntegerSet;

		public MyQueryableIntegerSet2(IMyIntegerSet myIntegerSet)
		{
			_myIntegerSet = myIntegerSet;
			Expression = Expression.Constant(this);
			
			Provider = new MyQueryableIntegerSetQueryProvider2<TOutputType>(this);
		}

		public IEnumerator<TOutputType> GetEnumerator()
		{
			if (typeof(TOutputType) != typeof(int))
				throw new NotImplementedException();
			
			return (IEnumerator<TOutputType>) _myIntegerSet.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _myIntegerSet.GetEnumerator();
		}

		public Type ElementType => typeof(TOutputType);
		public Expression Expression { get; }
		public IQueryProvider Provider { get; }
	}
}
