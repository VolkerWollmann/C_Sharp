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
    /// Maps from IMyIntegerSet myIntegerSet
    ///      to   IQueryable<int>(IEnumerator<int>) 
    ///           with the elements of the integer set
    /// </summary>
    /// <typeparam name="TType">Type of the elments</typeparam>
    public class MyIntegerSetQueryable2 : IQueryable<int>
	{
		private IEnumerable<int> _myIEnumerable;

		public MyIntegerSetQueryable2(IMyIntegerSet myIntegerSet)
		{
			_myIEnumerable = myIntegerSet;
			Expression = Expression.Constant(this);
			
			Provider = new MyIntegerSetQueryProvider2(this);
		}

        public IEnumerator<int> GetEnumerator()
		{
			return _myIEnumerable.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _myIEnumerable.GetEnumerator();
		}

		public Type ElementType => typeof(int);
		public Expression Expression { get; }
		public IQueryProvider Provider { get; }
	}
}
