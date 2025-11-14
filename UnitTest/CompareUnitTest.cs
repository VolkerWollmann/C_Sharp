using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_SharpExamplesLib.Language;

namespace UnitTest
{
    [TestClass]
    public class CompareUnitTest
    {
        [TestMethod]
        public void TestIComparer()
        {
            MyIComparable.TestIComparer();
        }

        [TestMethod]
        public void TestIComparable()
        {
            MyIComparable.TestIComparable();
        }

        [TestMethod]
        public void TestComparison()
        {
            MyIComparable.TestComparison();
        }

        [TestMethod]
        public void TestIEqualityComparer()
        {
            MyIEqualityComparer.Test();
        }

        [TestMethod]
        public void TestIEquatable()
        {
            MyIEquatable.Test();
        }

        [TestMethod]
        public void PartialOrder()
        {
            MyIEquatable.PartialOrderTest();

        }
    }
}
