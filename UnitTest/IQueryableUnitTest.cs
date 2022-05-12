using System.Linq;
using ThisAccessibilityProject = AccessibilityProject;
using OtherAccessibilityProject = AccessibilityOtherProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp.FileOperation;
using C_Sharp.Language;


namespace UnitTest
{
    [TestClass]
    public class IQueryableUnitTest
    {
        [TestMethod]
        public void IEnumerable_Test()
        {
            MyIntegerRangeTest.Test_IQueryable_as_IEnumerable();
        }

        [TestMethod]
        public void IQueryable_Test()
        {
            MyIntegerRange myIntegerRange = new MyIntegerRange(1, 10);

            // does work
            // uses private any implementation
            var d1 = ((IQueryable<int>) myIntegerRange).Any();

            MyIntegerRangeTest.Test_IQueryable();
        }

        [TestMethod]
        public void MultipleExpressions()
        {
            MyIntegerRangeTest.Test_MultipleExpressions();
        }

        [TestMethod]
        public void CascadedExpressions()
        {
            MyIntegerRangeTest.Test_CascadedExpressions();
        }

        [TestMethod]
        public void ProjectionExpression()
        {
            MyIntegerRangeTest.Test_ProjectionExpression();
        }

        [TestMethod]
        public void Test()
        {
            MyIntegerRangeTest.Test_Test();
        }


    }
}
