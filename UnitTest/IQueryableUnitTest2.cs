using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using ThisAccessibilityProject = AccessibilityProject;
using OtherAccessibilityProject = AccessibilityOtherProject;
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
	public class IQueryableUnitTest2
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
		public void Test_IQueryable_ForEach()
		{
			IMyIntegerSet myIntegerSet = _myIntegerSetFactory.GetMemoryIntegerSet();

			MyQueryableIntegerSet2<int> myQueryableIntegerSet = new MyQueryableIntegerSet2<int>(myIntegerSet);
			foreach (var i in myQueryableIntegerSet)
			{
				Assert.IsTrue(i < 4);
			}

		}

		[TestMethod]
		public void Test_IQueryable_Where_ForEach()
		{
			IMyIntegerSet myIntegerSet = _myIntegerSetFactory.GetMemoryIntegerSet();

			MyQueryableIntegerSet2<int> myQueryableIntegerSet = new MyQueryableIntegerSet2<int>(myIntegerSet);
			var expression = myQueryableIntegerSet.Where(i => i < 2);
			foreach (var i in expression)
			{
				Assert.IsTrue(i < 4);
			}
		}

		[TestMethod]
		public void Test_IQueryable_ToList()
		{
			List<int> l = [1, 2, 3];
			MyMemoryIntegerSet myIntegerSet = new MyMemoryIntegerSet(l);

			MyQueryableIntegerSet2<int> myQueryableIntegerSet = new MyQueryableIntegerSet2<int>(myIntegerSet);

			List<int> result = myQueryableIntegerSet.ToList();
			CollectionAssert.AreEqual(l, result);

		}

		[TestMethod]
		public void Test_IQueryable_Where_ToList()
		{
			List<int> l = [1, 2, 3];
			MyMemoryIntegerSet myIntegerSet = new MyMemoryIntegerSet(l);

			MyQueryableIntegerSet2<int> myQueryableIntegerSet = new MyQueryableIntegerSet2<int>(myIntegerSet);
			var expression = myQueryableIntegerSet.Where(i => i <= 2);
			var result = expression.ToList();

			CollectionAssert.AreEqual( new List<int>{1,2}, result);
		}
	}
}
