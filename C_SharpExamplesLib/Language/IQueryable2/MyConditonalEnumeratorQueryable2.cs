﻿using System;
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
	/// <summary>
	/// T
	/// </summary>
	/// <typeparam name="TOutputType"></typeparam>
	public class MyConditonalEnumeratorQueryable2<TType> : IQueryable<TType>
	{
		private MyConditionalEnumerator<TType> _myIntegerEnumerator;

		public MyConditonalEnumeratorQueryable2(IEnumerator<TType> integerEnumerator)
		{
			
			_myIntegerEnumerator = new MyConditionalEnumerator<TType>(integerEnumerator,null);
			Expression = Expression.Constant(this);

			Provider = new MyConditonalEnumeratorQueryProvider2<TType>(this);
		}

		public MyConditonalEnumeratorQueryable2(IEnumerator<TType> integerEnumerator, MethodCallExpression? expression)
			
		{
			_myIntegerEnumerator = new MyConditionalEnumerator<TType>(integerEnumerator, expression);
			Expression = Expression.Constant(this);

			Provider = new MyConditonalEnumeratorQueryProvider2<TType>(this);
		}

		public IEnumerator<TType> GetEnumerator()
		{
			return (IEnumerator<TType>)_myIntegerEnumerator;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _myIntegerEnumerator;
		}

		public Type ElementType => typeof(TType);
		public Expression Expression { get; }
		public IQueryProvider Provider { get; }
	}
}