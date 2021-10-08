using C_Sharp.Language.DataTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class AbstractDataTests
    {
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
        public void TestXmlElement()
        {
            MyXml.TestXmlElement();
        }

        [TestMethod]
        public void TestXmlFile()
        {
            MyXml.TestXmlFile();
        }

        [TestMethod]
        public void ListCapacity()
        {
            MyListTest.Test();
        }

        [TestMethod]
        public void Stack()
        {
            MyStackTest.Test();
        }

    }
}
