﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ThisAccessibilityProject = AccessibilityProject;
using OtherAccessibilityProject = AccessibilityOtherProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp.Language.IQueryable;
using C_Sharp.Language.MyEnumerableIntegerRangeLibrary;


namespace UnitTest
{
    [TestClass]
    // ReSharper disable once InconsistentNaming
    public class IQueryableUnitTest
    {
        [TestMethod]
        public void Test_IEnumerable()
        {
            MyEnumerableIntegerRangeTest.Test_IEnumerable();
        }

        [TestMethod]
        public void Test_IQueryable()
        {
            MyIntegerSet myIntegerSet =
                new MyIntegerSet(new List<int> {1, 2, 3});

            MyQueryableIntegerSet myQueryableIntegerSet = new MyQueryableIntegerSet(myIntegerSet);

            Assert.AreEqual(myQueryableIntegerSet.Expression.Type, typeof(MyQueryableIntegerSet));
            Assert.AreEqual(myQueryableIntegerSet.Expression.GetType(), typeof(ConstantExpression));

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
        /// Shows, how the MyQueryableIntegerSet together with the inner most where clause is
        /// evaluted first, before the result of this evaluation is provied to further linq query execution
        /// </summary>
        [TestMethod]
        public void Test_IQueryable_Where()
        {
            MyIntegerSet myIntegerSet =
                new MyIntegerSet(new List<int> {1, 2, 3});

            MyQueryableIntegerSet myQueryableIntegerSet = new MyQueryableIntegerSet(myIntegerSet);

            var expression = myQueryableIntegerSet.Where(i => TestForTwo(i));
            var result = expression.ToList();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(result[0], 2);
        }

        /// <summary>
        /// Shows, how the MyQueryableIntegerSet together with the inner most where clause is
        /// evaluted first, before the result of this evaluation is provied to further linq query execution
        /// </summary>
        [TestMethod]
        public void Test_IQueryable_WhereWhere()
        {
            MyIntegerSet myIntegerSet =
                new MyIntegerSet(new List<int> {1, 2, 3});

            MyQueryableIntegerSet myQueryableIntegerSet = new MyQueryableIntegerSet(myIntegerSet);

            var expression = myQueryableIntegerSet.Where(i => TestForTwo(i)).Where(i => TestForTwo(i));
            var result = expression.ToList();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(result[0], 2);
        }

        [TestMethod]
        public void Test_IQueryable_SumAsExtension()
        {
            MyIntegerSet myIntegerSet =
                new MyIntegerSet(new List<int> {1, 2, 3});

            MyQueryableIntegerSet myQueryableIntegerSet = new MyQueryableIntegerSet(myIntegerSet);

            var sum = myQueryableIntegerSet.Sum();

            Assert.IsTrue(sum == 6);
        }

        [TestMethod]
        public void Test_IQueryable_MaxDelegated()
        {
            MyIntegerSet myIntegerSet =
                new MyIntegerSet(new List<int> { 1, 2, 3 });

            MyQueryableIntegerSet myQueryableIntegerSet = new MyQueryableIntegerSet(myIntegerSet);

            var max = myQueryableIntegerSet.Max();

            Assert.IsTrue(max == 3);
        }

        [TestMethod]
        //[Ignore]
        public void Test_IQueryable_Select_Simple()

        {
            MyIntegerSet myIntegerSet =
                new MyIntegerSet(new List<int> {1, 2, 3});

            MyQueryableIntegerSet myQueryableIntegerSet = new MyQueryableIntegerSet(myIntegerSet);

            var result = myQueryableIntegerSet.Select(e => e);

            Assert.IsTrue(result.All(e => (e.GetType()) == typeof(int) ));
        }

        [TestMethod]
        public void Test_IQueryable_Select_IntegerFunction()

        {
            MyIntegerSet myIntegerSet =
                new MyIntegerSet(new List<int> { 1, 2, 3 });

            MyQueryableIntegerSet myQueryableIntegerSet = new MyQueryableIntegerSet(myIntegerSet);

            var result = myQueryableIntegerSet.Select(e => e*2);

            // #Assert #list #equal
            CollectionAssert.AreEqual(result.ToList(), new List<int> { 2, 4, 6 });
        }

        [TestMethod]
        public void Test_IQueryable_SelectSelect_IntegerFunction()

        {
            MyIntegerSet myIntegerSet =
                new MyIntegerSet(new List<int> { 1, 2, 3 });

            MyQueryableIntegerSet myQueryableIntegerSet = new MyQueryableIntegerSet(myIntegerSet);

            var result = myQueryableIntegerSet.Select(e => e * 2).Select( e=> e *2 );

            // #Assert #list #equal
            CollectionAssert.AreEqual(result.ToList(), new List<int> { 4, 8, 12 });
        }

        [TestMethod]
        [Ignore]
        public void Test_IQueryable_Select_StringFunction()
        {
            MyIntegerSet myIntegerSet =
                new MyIntegerSet(new List<int> { 1, 2, 3 });

            MyQueryableIntegerSet myQueryableIntegerSet = new MyQueryableIntegerSet(myIntegerSet);

            var result = myQueryableIntegerSet.Select(e => "Z"+e);

            // #Assert #list #equal
            CollectionAssert.AreEqual(result.ToList(), new List<string> { "Z1", "Z2", "Z3" });
        }
    }
} 
