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
        public void IEnumerable_Test()
        {
            MyEnumerableIntegerRangeTest.Test_IEnumerable();
        }

        [TestMethod]
        public void IQueryable_Test_MyQueryableIntegerSet()
        {
            MyQueryableIntegerSet myQueryableIntegerSet = 
                new MyQueryableIntegerSet(new List<int>{1,2,3});
            
            Assert.AreEqual(myQueryableIntegerSet.Expression.Type, typeof(MyQueryableIntegerSet));
            Assert.AreEqual(myQueryableIntegerSet.Expression.GetType(), typeof(ConstantExpression));
        }
    }
}
