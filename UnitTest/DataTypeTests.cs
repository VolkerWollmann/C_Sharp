using System;
using C_Sharp.Language;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp.Language.Task;



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
        public void TestXml()
        {
            MyXml.Test();
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
