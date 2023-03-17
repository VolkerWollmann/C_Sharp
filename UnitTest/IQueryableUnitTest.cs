using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ThisAccessibilityProject = AccessibilityProject;
using OtherAccessibilityProject = AccessibilityOtherProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp.Language.IQueryable;
using C_Sharp.Language.MyEnumerableIntegerRangeLibrary;
using System.Web;

namespace UnitTest
{
    [TestClass]
    // ReSharper disable once InconsistentNaming
    public class IQueryableUnitTest
    {
        private MyIntegerSetFactory myIntegerSetFactory;
        IMyIntegerSet[] myIntegerSets; 

        [TestInitialize]
        public void Initialize()
        {
            myIntegerSetFactory = new MyIntegerSetFactory();
            ;
            myIntegerSets = myIntegerSetFactory.GetTestData();
        }

        [TestMethod]
        public void Test_IEnumerable()
        {
            MyEnumerableIntegerRangeTest.Test_IEnumerable();
        }

        [TestMethod]
        public void Test_IQueryable()
        {
            foreach (IMyIntegerSet myIntegerSet in myIntegerSets)
            {

                MyQueryableIntegerSet<int> myQueryableIntegerSet = new MyQueryableIntegerSet<int>(myIntegerSet);

                Assert.AreEqual(myQueryableIntegerSet.Expression.Type, typeof(MyQueryableIntegerSet<int>));
                Assert.AreEqual(myQueryableIntegerSet.Expression.GetType(), typeof(ConstantExpression));
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
        /// Shows, how the MyQueryableIntegerSet together with the inner most where clause is
        /// evaluated first, before the result of this evaluation is provided to further linq query execution
        /// </summary>
        [TestMethod]
        public void Test_IQueryable_Where()
        {
            foreach (IMyIntegerSet myIntegerSet in myIntegerSets)
            {

                MyQueryableIntegerSet<int> myQueryableIntegerSet = new MyQueryableIntegerSet<int>(myIntegerSet);

                var expression = myQueryableIntegerSet.Where(i => TestForTwo(i));
                var result = expression.ToList();
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(result[0], 2);
            }
        }

        /// <summary>
        /// Shows, how the MyQueryableIntegerSet together with the inner most where clause is
        /// evaluated first, before the result of this evaluation is provided to further linq query execution
        /// </summary>
        [TestMethod]
        public void Test_IQueryable_WhereWhere()
        {
            foreach (IMyIntegerSet myIntegerSet in myIntegerSets)
            {

                MyQueryableIntegerSet<int> myQueryableIntegerSet = new MyQueryableIntegerSet<int>(myIntegerSet);

                var expression = myQueryableIntegerSet.Where(i => TestForTwo(i)).Where(i => TestForTwo(i));
                var result = expression.ToList();
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(result[0], 2);
            }
        }

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
    }
} 
