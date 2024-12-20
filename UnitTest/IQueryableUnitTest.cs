﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ThisAccessibilityProject = AccessibilityProject;
using OtherAccessibilityProject = AccessibilityOtherProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp.Language.IQueryable;
using C_Sharp.Language.MyEnumerableIntegerRangeLibrary;
using System.Web;
using MyEnumerableIntegerRangeLibrary;

namespace UnitTest
{
    [TestClass]
    // ReSharper disable once InconsistentNaming
    public class IQueryableUnitTest
    {
        private MyIntegerSetFactory myIntegerSetFactory;
        List<IMyIntegerSet> myIntegerSets; 

        [TestInitialize]
        public void Initialize()
        {
            myIntegerSetFactory = new MyIntegerSetFactory();
            ;
            myIntegerSets = myIntegerSetFactory.GetIntegerSets();
        }

        [TestCleanup]
        public void Cleanup()
        {
            myIntegerSetFactory.Dispose();
        }

        [TestMethod]
        public void Test_IEnumerable()
        {
            MyEnumerableIntegerRangeTest.Test_IEnumerable();
        }

		[TestMethod]
		public void Test_IEnumerable_FromMemoryIntegerSet()
		{
			MyEnumerableIntegerRangeTest.Test_IEnumerable_FromMemoryIntegerSet();
		}

		[TestMethod]
        public void Test_IQueryable()
        {
            foreach (IMyIntegerSet myIntegerSet in myIntegerSets)
            {

                MyQueryableIntegerSet<int> myQueryableIntegerSet = new MyQueryableIntegerSet<int>(myIntegerSet);

                Assert.AreEqual(typeof(MyQueryableIntegerSet<int>), myQueryableIntegerSet.Expression.Type);
                Assert.AreEqual(typeof(ConstantExpression), myQueryableIntegerSet.Expression.GetType());
            }

        }

        #region Where

        [TestMethod]
        public void Test_IQueryable_Where()
        {
            foreach (IMyIntegerSet myIntegerSet in myIntegerSets)
            {
                MyQueryableIntegerSet<int> myQueryableIntegerSet = new MyQueryableIntegerSet<int>(myIntegerSet);
                var expression = myQueryableIntegerSet.Where(i => (i == i - i + 2));
                var result = expression.ToList();
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(2, result[0]);
            }
        }

		[TestMethod]
		public void Test_IQueryable_Where_ForEach()
		{
			foreach (IMyIntegerSet myIntegerSet in myIntegerSets)
			{
				MyQueryableIntegerSet<int> myQueryableIntegerSet = new MyQueryableIntegerSet<int>(myIntegerSet);
				var expression = myQueryableIntegerSet.Where(i => i < 2);
				foreach (var i in expression)
				{
					Assert.IsTrue(i < 2);
				}
			}
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

        /// <summary>
        /// Shows, how the MyQueryableIntegerSet together with the innermost where clause is
        /// evaluated first, before the result of this evaluation is provided to further linq query execution
        /// </summary>
        [TestMethod]
        public void Test_IQueryable_WhereComplex()
        {
            IMyIntegerSet myIntegerSet = myIntegerSetFactory.GetMemoryIntegerSet();

            MyQueryableIntegerSet<int> myQueryableIntegerSet = new MyQueryableIntegerSet<int>(myIntegerSet);

            var expression = myQueryableIntegerSet.Where(i => TestForTwo(i));
            var result = expression.ToList();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(2, result[0]);

        }

        /// <summary>
        /// Will fail for optimized database integer set
        /// </summary>
        [TestMethod]
        public void Test_IQueryable_WhereComplex_DatabaseIntegerSet()
        {
            if ( !myIntegerSetFactory.DatabaseIntegerSetsAvailable() )
                Assert.Inconclusive("No database connection");

            IMyIntegerSet myIntegerSet = myIntegerSetFactory.GetOptimizedDatabaseIntegerSet();


            MyQueryableIntegerSet<int> myQueryableIntegerSet = new MyQueryableIntegerSet<int>(myIntegerSet);
            var expression = myQueryableIntegerSet.Where(i => TestForTwo(i));

            Assert.ThrowsException <NotImplementedException>(() => expression.ToList());
            //Assert.AreEqual(1, result.Count);
            //Assert.AreEqual(2, result[0]);

        }

        /// <summary>
        /// Shows, how the MyQueryableIntegerSet together with the innermost where clause is
        /// evaluated first, before the result of this evaluation is provided to further linq query execution
        /// </summary>
        [TestMethod]
        public void Test_IQueryable_Where_Where()
        {
            IMyIntegerSet myIntegerSet = myIntegerSetFactory.GetMemoryIntegerSet();

            MyQueryableIntegerSet<int> myQueryableIntegerSet = new MyQueryableIntegerSet<int>(myIntegerSet);

            var expression = myQueryableIntegerSet.Where(i => TestForTwo(i)).Where(i => TestForTwo(i));
            var result = expression.ToList();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(2, result[0]);
        }
        #endregion

        #region Any
        [TestMethod]
        public void Test_IQueryable_AnyDelegated()
        {
            IMyIntegerSet myIntegerSet = myIntegerSetFactory.GetMemoryIntegerSet();

            MyQueryableIntegerSet<int> myQueryableIntegerSet = new MyQueryableIntegerSet<int>(myIntegerSet);

            var expression = myQueryableIntegerSet.Any();
            var result = expression;

            Assert.AreEqual(result, true);

        }

        [TestMethod]
        public void Test_IQueryable_AnyWithFunctionCallConditionAsExtension()
        {
            foreach (IMyIntegerSet myIntegerSet in
                     myIntegerSetFactory.GetIntegerSets(
	                     MyIntegerSetFactory.DesiredDatabases.Memory | 
	                     MyIntegerSetFactory.DesiredDatabases.DatabaseCursor |
	                     MyIntegerSetFactory.DesiredDatabases.DatabaseStatement))
            {
                MyQueryableIntegerSet<int> myQueryableIntegerSet = new MyQueryableIntegerSet<int>(myIntegerSet);

                var expression = myQueryableIntegerSet.Any(i => TestForTwo(i));
                var result = expression;

                Assert.AreEqual(result, true);
            }
        }

        [TestMethod]
        public void Test_IQueryable_AnyWithSimpleExpressionConditionAsExtension()
        {
            foreach (IMyIntegerSet myIntegerSet in myIntegerSets)
            {
                MyQueryableIntegerSet<int> myQueryableIntegerSet = new MyQueryableIntegerSet<int>(myIntegerSet);

                var expression = myQueryableIntegerSet.Any(i => i < 2);
                var result = expression;

                Assert.AreEqual(result, true);
            }
        }
        #endregion

        [TestMethod]
        public void Test_IQueryable_SumAsExtension()
        {
            foreach (IMyIntegerSet myIntegerSet in myIntegerSets)
            {

                MyQueryableIntegerSet<int> myQueryableIntegerSet = new MyQueryableIntegerSet<int>(myIntegerSet);

                var sum = myQueryableIntegerSet.Sum();

                Assert.IsTrue(sum == 6);
            }
        }

        [TestMethod]
        public void Test_IQueryable_MaxDelegated()
        {
            foreach (IMyIntegerSet myIntegerSet in myIntegerSets)
            {

                MyQueryableIntegerSet<int> myQueryableIntegerSet = new MyQueryableIntegerSet<int>(myIntegerSet);

                var max = myQueryableIntegerSet.Max();

                Assert.IsTrue(max == 3);
            }
        }

        #region Select
        [TestMethod]
        //[Ignore]
        public void Test_IQueryable_Select_Simple()
        {
            foreach (IMyIntegerSet myIntegerSet in myIntegerSets)
            {

                MyQueryableIntegerSet<int> myQueryableIntegerSet = new MyQueryableIntegerSet<int>(myIntegerSet);

                var result = myQueryableIntegerSet.Select(e => e);

                Assert.IsTrue(result.All(e => (e.GetType()) == typeof(int)));
            }
        }

        [TestMethod]
        public void Test_IQueryable_Select_IntegerFunction()
        {
            foreach (IMyIntegerSet myIntegerSet in myIntegerSets)
            {

                MyQueryableIntegerSet<int> myQueryableIntegerSet = new MyQueryableIntegerSet<int>(myIntegerSet);

                var result = myQueryableIntegerSet.Select(e => e * 2);

                // #Assert #list #equal
                CollectionAssert.AreEqual(result.ToList(), new List<int> {2, 4, 6});
            }
        }

        [TestMethod]
        public void Test_IQueryable_SelectSelect_IntegerFunction()
        {
            foreach (IMyIntegerSet myIntegerSet in myIntegerSets)
            {

                MyQueryableIntegerSet<int> myQueryableIntegerSet = new MyQueryableIntegerSet<int>(myIntegerSet);

                var result = myQueryableIntegerSet.Select(e => e * 2).Select(e => e * 2);

                // #Assert #list #equal
                CollectionAssert.AreEqual(result.ToList(), new List<int> {4, 8, 12});
            }
        }

        [TestMethod]
        //[Ignore]
        public void Test_IQueryable_Select_String()
        {
            foreach (IMyIntegerSet myIntegerSet in myIntegerSets)
            {
                MyQueryableIntegerSet<int> myQueryableIntegerSet = new MyQueryableIntegerSet<int>(myIntegerSet);

                var result = myQueryableIntegerSet.Select(e => "Z" + e);

                // #Assert #list #equal
                CollectionAssert.AreEqual(result.ToList(), new List<string> {"Z1", "Z2", "Z3"});
            }
        }

        [TestMethod]
        //[DataRow(new MyIntegerSet(new List<int> { 1, 2, 3 }))]
        //[Ignore]
        public void Test_IQueryable_Select_Tuple()
        {
            foreach (IMyIntegerSet myIntegerSet in myIntegerSets)
            {
                MyQueryableIntegerSet<int> myQueryableIntegerSet = new MyQueryableIntegerSet<int>(myIntegerSet);

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
        #endregion
    }
} 
