using System.Linq;
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
    }
}
