﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_SharpExamplesLib.Language.IQueryable;
using MyEnumerableIntegerRangeLibrary;

namespace UnitTest
{
	[TestClass]
	public class IQueryableUnitTest
	{
		private MyIntegerSetFactory _myIntegerSetFactory;
		List<IMyIntegerSet> _myIntegerSets;

		[TestInitialize]
		public void Initialize()
		{
			_myIntegerSetFactory = new MyIntegerSetFactory();
			
			_myIntegerSets = _myIntegerSetFactory.GetIntegerSets();
		}

		[TestCleanup]
		public void Cleanup()
		{
			_myIntegerSetFactory.Dispose();
		}

        private IMyDisposeQueryable<int> GetMyQueryable(IMyIntegerSet myIntegerSet)
        {
            return new MyEnumeratorQueryable<int>(myIntegerSet.GetEnumerator());
        }

        [TestMethod]
		public void Test_ForEach()
		{
			foreach (IMyIntegerSet myIntegerSet in _myIntegerSets)
			{
				using var myQueryableIntegerSet = GetMyQueryable(myIntegerSet);
				var expression = myQueryableIntegerSet.Where(i => i < 2);
				foreach (var i in expression)
				{
					Assert.IsTrue(i < 2);
				}
			}

		}

	    [TestMethod]
		public void Test_Where_ForEach()
		{
			foreach (IMyIntegerSet myIntegerSet in _myIntegerSets)
			{
				using var myQueryableIntegerSet = GetMyQueryable(myIntegerSet);
				var expression = myQueryableIntegerSet.Where(i => i <= 2);
				foreach (var i in expression)
				{
					Assert.IsTrue(i <= 2);
				}
			}
		}

		[TestMethod]
		public void Test_ToList()
		{
			List<int> l = [1, 2, 3];
			MyMemoryIntegerSet myIntegerSet = new MyMemoryIntegerSet(l);

			using var myQueryableIntegerSet = GetMyQueryable(myIntegerSet);
			List<int> result = myQueryableIntegerSet.ToList();
			CollectionAssert.AreEqual(l, result);
		}

		[TestMethod]
		public void Test_Where_ToList()
		{
			List<int> l = [1, 2, 3];
			MyMemoryIntegerSet myIntegerSet = new MyMemoryIntegerSet(l);

			using var myQueryableIntegerSet = GetMyQueryable(myIntegerSet);
			var expression = myQueryableIntegerSet.Where(i => i <= 2);
			var result = expression.ToList();

			CollectionAssert.AreEqual(new List<int> {1, 2}, result);
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

		private bool SecondTestForTwo(int i)
		{
			return i == 2;
		}

		[TestMethod]
		public void Test_Where_Where()
		{
			IMyIntegerSet myIntegerSet = _myIntegerSetFactory.GetMemoryIntegerSet();

			using var myQueryableIntegerSet = GetMyQueryable(myIntegerSet);
			var expression = myQueryableIntegerSet.Where(i => TestForTwo(i)).Where(i => SecondTestForTwo(i));
			var result = expression.ToList();
			Assert.AreEqual(1, result.Count);
			Assert.AreEqual(2, result[0]);
		}

		[TestMethod]
		//[Ignore]
		public void Test_Select_Simple()
		{
			foreach (IMyIntegerSet myIntegerSet in _myIntegerSets)
			{
				using var myQueryableIntegerSet = GetMyQueryable(myIntegerSet);
				var result = myQueryableIntegerSet.Select(e => e);

				foreach (var e in result)
				{
					Assert.IsTrue(e <= 3);
				}
			}
		}

        [TestMethod]
        //[Ignore]
        public void Test_Select_Select()
        {
            foreach (IMyIntegerSet myIntegerSet in _myIntegerSets)
            {
	            using var myQueryableIntegerSet = GetMyQueryable(myIntegerSet);
	            var result = myQueryableIntegerSet.Select(e => e).Select(e => e);

	            foreach (var e in result)
	            {
		            Assert.IsTrue(e <= 3);
	            }
            }
        }

        [TestMethod]
        public void Test_Select_IntegerFunction()
        {
            foreach (IMyIntegerSet myIntegerSet in _myIntegerSets)
            {
                using var myQueryableIntegerSet = GetMyQueryable(myIntegerSet);
                var result = myQueryableIntegerSet.Select(e => e * 2);

                var controlSet = new List<int> {2, 4, 6};

                var resultList = new List<int>();
                foreach (var e in result)
                {
                    resultList.Add(e);
                }

                // #Assert #list #equal
                CollectionAssert.AreEqual(resultList, controlSet);
            }

        }

		[TestMethod]
        public void Test_Select_String()
        {
            List<int> l = [1, 2, 3];
            MyMemoryIntegerSet myIntegerSet = new MyMemoryIntegerSet(l);

            using var myQueryableIntegerSet = GetMyQueryable(myIntegerSet);
            var result = myQueryableIntegerSet.Select(e => "Donkey_" + e);

            var controlSet = new List<string> {"Donkey_1", "Donkey_2", "Donkey_3"};

            var resultList = new List<string>();
            foreach (var e in result)
            {
	            resultList.Add(e);

            }

            // #Assert #list #equal
            CollectionAssert.AreEqual(resultList, controlSet);
        }

        [TestMethod]
        public void Test_Where_Select_String()
        {
            List<int> l = [1, 2, 3];
            MyMemoryIntegerSet myIntegerSet = new MyMemoryIntegerSet(l);

            using var myQueryableIntegerSet = GetMyQueryable(myIntegerSet);
            var result = myQueryableIntegerSet.Where(x => (x == 1 || x == 3)).Select(e => "Donkey_" + e);

            var controlSet = new List<string> {"Donkey_1", "Donkey_3"};

            var resultList = new List<string>();
            foreach (var e in result)
            {
	            resultList.Add(e);

            }

            // #Assert #list #equal
            CollectionAssert.AreEqual(resultList, controlSet);
        }


        [TestMethod]
        public void Test_Select_Tuple()
        {
            foreach (IMyIntegerSet myIntegerSet in _myIntegerSets)
            {
	            using var myQueryableIntegerSet = GetMyQueryable(myIntegerSet);
	            var result = myQueryableIntegerSet.Select(e => Tuple.Create("A", e));

	            // #Assert #list #equal
	            CollectionAssert.AreEqual(result.ToList(),
		            new List<Tuple<string, int>>
		            {
			            Tuple.Create("A", 1),
			            Tuple.Create("A", 2),
			            Tuple.Create("A", 3)
		            });
            }
        }

        [TestMethod]
        public void Test_Select_Tuple_Any()
        {
            foreach (IMyIntegerSet myIntegerSet in _myIntegerSets)
            {
	            using var myQueryableIntegerSet = GetMyQueryable(myIntegerSet);
	            var result = myQueryableIntegerSet.Select(e => Tuple.Create("A", e)).Any();

	            Assert.IsTrue(result);
            }
        }

        [TestMethod]
		public void Test_Any()
		{
			foreach (IMyIntegerSet myIntegerSet in _myIntegerSets)
			{
				using var myQueryableIntegerSet = GetMyQueryable(myIntegerSet);
				var expression = myQueryableIntegerSet.Any();
				var result = expression;

				Assert.AreEqual(result, true);
			}
		}

		[TestMethod]
		public void Test_AnyExpression()
		{
			foreach (IMyIntegerSet myIntegerSet in _myIntegerSets)
			{
				using var myQueryableIntegerSet = GetMyQueryable(myIntegerSet);
				var expression = myQueryableIntegerSet.Any(i => i < 2);
				var result = expression;

				Assert.AreEqual(result, true);
			}
		}

		[TestMethod]
		public void Test_AnyWithFunctionCallExpression()
		{
			foreach (IMyIntegerSet myIntegerSet in _myIntegerSets)
			{
				using var myQueryableIntegerSet = GetMyQueryable(myIntegerSet);
				var expression = myQueryableIntegerSet.Any(i => TestForTwo(i));
				var result = expression;

				Assert.AreEqual(result, true);
			}
		}

		[TestMethod]
		public void Test_Square()
		{
			foreach (IMyIntegerSet myIntegerSet in _myIntegerSets)
			{
				using var myQueryableIntegerSet = GetMyQueryable(myIntegerSet);
				var squaredNumbers = myQueryableIntegerSet.Square().Where(i => i <= 4).ToList();
				CollectionAssert.AreEqual(new[] { 1, 4 }, squaredNumbers);

                var squaredNumbers1 = myQueryableIntegerSet.Where(i => i <= 2).Square().ToList();
                CollectionAssert.AreEqual(new[] { 1, 4 }, squaredNumbers1);

            }
        }

		[TestMethod]
		public void Test_Where_Any()
		{
			foreach (IMyIntegerSet myIntegerSet in _myIntegerSets)
			{
				using var myQueryableIntegerSet = GetMyQueryable(myIntegerSet);
				// ReSharper disable once ReplaceWithSingleCallToAny
				var expression = myQueryableIntegerSet.Where(i => i < 2).Any();
				var result = expression;

				Assert.AreEqual(result, true);
			}

		}

		[TestMethod]
		public void Test_Sum()
		{
			foreach (IMyIntegerSet myIntegerSet in _myIntegerSets)
			{
				using var myQueryableIntegerSet = GetMyQueryable(myIntegerSet);
				var sum = myQueryableIntegerSet.Sum();

				Assert.IsTrue(sum == 6);
			}
		}

		[TestMethod]
		public void Test_Max()
		{
			foreach (IMyIntegerSet myIntegerSet in _myIntegerSets)
			{
				using var myQueryableIntegerSet = GetMyQueryable(myIntegerSet);
				var max = myQueryableIntegerSet.Max();

				Assert.IsTrue(max == 3);
			}
		}
	}
}
