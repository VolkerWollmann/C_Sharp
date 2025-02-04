using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using ThisAccessibilityProject = AccessibilityProjectCore;
using OtherAccessibilityProject = AccessibilityOtherProjectCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp.Language.IQueryable;
using C_Sharp.Language.MyEnumerableIntegerRangeLibrary;
using System.Web;
using C_Sharp.Language.IQueryable2;
using MyEnumerableIntegerRangeLibrary;

namespace UnitTest
{
	[TestClass]
	// ReSharper disable once InconsistentNaming
	public class IEnumerable
	{
		private MyIntegerSetFactory _myIntegerSetFactory;
		List<IMyIntegerSet> _myIntegerSets;

		[TestInitialize]
		public void Initialize()
		{
			_myIntegerSetFactory = new MyIntegerSetFactory();
			;
			_myIntegerSets = _myIntegerSetFactory.GetIntegerSets();
		}

		[TestCleanup]
		public void Cleanup()
		{
			_myIntegerSetFactory.Dispose();
		}

		[TestMethod]
		public void Test_IEnumerable()
		{
			MyEnumerableIntegerRangeTest.Test_IEnumerable();
		}

		[TestMethod]
		public void Test_IEnumerable_TwoEnumeratorsOnIEnumerable()
		{
			MyEnumerableIntegerRangeTest.Test_TwoEnumeratorsOnIEnumerable();
		}

		[TestMethod]
		public void Test_IEnumerable_Where()
		{
			MyEnumerableIntegerRangeTest.Test_IEnumerable_Where();
		}

		[TestMethod]
		public void Test_IEnumerable_FromMemoryIntegerSet()
		{
			MyEnumerableIntegerRangeTest.Test_IEnumerable_FromMemoryIntegerSet();
		}
	}
}