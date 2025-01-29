using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using C_Sharp.Language.IQueryable;
using C_Sharp.Language.MyEnumerableIntegerRangeLibrary;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace C_Sharp.Language.IQueryable2
{
	public class MyQueryableIntegerSetQueryProvider2<TOutputType> : IQueryProvider
	{
		private MyQueryableIntegerSet2<TOutputType> _myQueryableIntegerSet2;
		public MyQueryableIntegerSetQueryProvider2(MyQueryableIntegerSet2<TOutputType> myQueryableIntegerSet2IntegerSet)
		{
			_myQueryableIntegerSet2 = myQueryableIntegerSet2IntegerSet;
		}

		public System.Linq.IQueryable CreateQuery(Expression expression)
		{
			throw new NotImplementedException();
		}

		public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
		{
			if (typeof(TElement) != typeof(int))
				throw new NotImplementedException();
			
			//return new MyQueryableIntegerSet2<TElement>(_myQueryableIntegerSet2);
			throw new NotImplementedException();
		}

		public object Execute(Expression expression)
		{
			throw new NotImplementedException();
		}

		public TResult Execute<TResult>(Expression expression)
		{
			throw new NotImplementedException();
		}
	}
}
