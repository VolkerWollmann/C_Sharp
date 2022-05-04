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
    }
}
