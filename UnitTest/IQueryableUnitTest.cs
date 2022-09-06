using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ThisAccessibilityProject = AccessibilityProject;
using OtherAccessibilityProject = AccessibilityOtherProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp.Language.IQueryable;


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

        [TestMethod]
        public void Test_IQueryable_Where()
        {
            MyIntegerSet myIntegerSet =
                new MyIntegerSet(new List<int> {1, 2, 3});

            MyQueryableIntegerSet myQueryableIntegerSet = new MyQueryableIntegerSet(myIntegerSet);

            var expression = myQueryableIntegerSet.Where(i => (i == 2));
            var result = expression.ToList();
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void Test_IQueryable_Sum()
        {
            MyIntegerSet myIntegerSet =
                new MyIntegerSet(new List<int> {1, 2, 3});

            MyQueryableIntegerSet myQueryableIntegerSet = new MyQueryableIntegerSet(myIntegerSet);

            var sum = myQueryableIntegerSet.Sum();

            Assert.IsTrue(sum == 6);
        }
    }
} 
