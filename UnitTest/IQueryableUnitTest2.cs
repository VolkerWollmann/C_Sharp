﻿using System;
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
			foreach (IMyIntegerSet myIntegerSet in _myIntegerSets)
			{
				MyQueryableIntegerSet<int> myQueryableIntegerSet = new MyQueryableIntegerSet<int>(myIntegerSet);
				var expression = myQueryableIntegerSet.Where(i => i < 2);
				foreach (var i in expression)
				{
					Assert.IsTrue(i < 2);
				}
			}

		}

	    [TestMethod]
		public void Test_IQueryable_Where_ForEach()
		{
			foreach (IMyIntegerSet myIntegerSet in _myIntegerSets)
			{
				MyQueryableIntegerSet2<int> myQueryableIntegerSet = new MyQueryableIntegerSet2<int>(myIntegerSet);
				var expression = myQueryableIntegerSet.Where(i => i < 2);
				foreach (var i in expression)
				{
					Assert.IsTrue(i < 4);
				}
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


		/// <summary>
		/// Test function for debugging purpose 
		/// </summary>
		/// <param name="i">number to test</param>
		/// <returns>true, if i == 2</returns>
		private bool TestForTwo(int i)
		{
			return i == 2;
		}

		private bool TestFuerZwei(int i)
		{
			return i == 2;
		}

		[TestMethod]
		public void Test_IQueryable_Where_Where()
		{
			IMyIntegerSet myIntegerSet = _myIntegerSetFactory.GetMemoryIntegerSet();

			MyQueryableIntegerSet2<int> myQueryableIntegerSet = new MyQueryableIntegerSet2<int>(myIntegerSet);

			var expression = myQueryableIntegerSet.Where(i => TestForTwo(i)).Where(i => TestFuerZwei(i));
			var result = expression.ToList();
			Assert.AreEqual(1, result.Count);
			Assert.AreEqual(2, result[0]);
		}

		[TestMethod]
		//[Ignore]
		public void Test_IQueryable_Select_Simple()
		{
			foreach (IMyIntegerSet myIntegerSet in _myIntegerSets)
			{

				MyQueryableIntegerSet2<int> myQueryableIntegerSet = new MyQueryableIntegerSet2<int>(myIntegerSet);

				var result = myQueryableIntegerSet.Select(e => e);

				foreach (var e in result)
				{
					Assert.IsTrue(e is int);
				}
			}
		}

		[TestMethod]
		public void Test_IQueryable_Any()
		{
			foreach (IMyIntegerSet myIntegerSet in _myIntegerSets)
			{

				MyQueryableIntegerSet2<int> myQueryableIntegerSet = new MyQueryableIntegerSet2<int>(myIntegerSet);

				var expression = myQueryableIntegerSet.Any();
				var result = expression;

				Assert.AreEqual(result, true);
			}

		}

		[TestMethod]
		public void Test_IQueryable_AnyWithSimpleExpressionConditionAsExtension()
		{
			foreach (IMyIntegerSet myIntegerSet in _myIntegerSets)
			{
				MyQueryableIntegerSet2<int> myQueryableIntegerSet = new MyQueryableIntegerSet2<int>(myIntegerSet);

				var expression = myQueryableIntegerSet.Any(i => i < 2);
				var result = expression;

				Assert.AreEqual(result, true);
			}
		}

		[TestMethod]
		public void Test_IQueryable_Where_Any()
		{
			foreach (IMyIntegerSet myIntegerSet in _myIntegerSets)
			{

				MyQueryableIntegerSet2<int> myQueryableIntegerSet = new MyQueryableIntegerSet2<int>(myIntegerSet);

				// ReSharper disable once ReplaceWithSingleCallToAny
				var expression = myQueryableIntegerSet.Where( i => i<2).Any();
				var result = expression;

				Assert.AreEqual(result, true);
			}

		}

		[TestMethod]
		public void Test_IQueryable_Sum()
		{
			foreach (IMyIntegerSet myIntegerSet in _myIntegerSets)
			{

				MyQueryableIntegerSet<int> myQueryableIntegerSet = new MyQueryableIntegerSet<int>(myIntegerSet);

				var sum = myQueryableIntegerSet.Sum();

				Assert.IsTrue(sum == 6);
			}
		}

		[TestMethod]
		public void Test_IQueryable_Max()
		{
			foreach (IMyIntegerSet myIntegerSet in _myIntegerSets)
			{

				MyQueryableIntegerSet<int> myQueryableIntegerSet = new MyQueryableIntegerSet<int>(myIntegerSet);

				var max = myQueryableIntegerSet.Max();

				Assert.IsTrue(max == 3);
			}
		}
	}
}
