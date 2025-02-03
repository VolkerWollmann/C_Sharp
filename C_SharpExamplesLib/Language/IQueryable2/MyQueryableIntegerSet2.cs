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

	public class MyQueryableIntegerSet2<TBaseType> : IQueryable<TBaseType>
	{
		private IEnumerable<TBaseType> _myIEnumerable;

		public MyQueryableIntegerSet2(IMyIntegerSet myIntegerSet)
		{
			_myIEnumerable = (IEnumerable<TBaseType>)myIntegerSet;
			Expression = Expression.Constant(this);
			
			Provider = new MyQueryableIntegerSetQueryProvider2<TBaseType>(this);
		}

        public MyQueryableIntegerSet2(IEnumerable<TBaseType> iEnumerable)
        {
            _myIEnumerable = iEnumerable;
            Expression = Expression.Constant(this);

            Provider = new MyQueryableIntegerSetQueryProvider2<TBaseType>(this);
        }

        public IEnumerator<TBaseType> GetEnumerator()
		{
			return (IEnumerator <TBaseType>)_myIEnumerable.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _myIEnumerable.GetEnumerator();
		}

		public Type ElementType => typeof(TBaseType);
		public Expression Expression { get; }
		public IQueryProvider Provider { get; }
	}
}
