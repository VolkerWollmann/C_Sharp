using C_SharpExamplesLib.Language.DataTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class AbstractDataTests
    {
        [TestMethod]
        public void SimpleTypes()
        {
            MySimpleTypes.Test();
        }

        [TestMethod]
        public void Arrays()
        {
            MyArray.Test();
        }

        [TestMethod]
        public void ArrayExtension()
        {
            MyArray.ArrayExtension();
        }

        [TestMethod]
        public void GenericInterface()
        {
            MyGenericInterface.Test();

        }

        [TestMethod]
        public void GenericClass()
        {
            MyGenericClassTest.Test();

        }

        [TestMethod]
        public void ListCapacity()
        {
            MyListTest.Test();
        }

        [TestMethod]
        public void SortedList()
        {
            MySortedListTest.Test();
        }

        [TestMethod]
        public void Stack()
        {
            MyStackTest.Test();
        }

        [TestMethod]
        public void HashSet()
        {
            MyHashSet.Test();
        }

        [TestMethod]
        public void DateTimeComparison()
        {
            MyDate.DateTimeComparison();
        }
    }
}
