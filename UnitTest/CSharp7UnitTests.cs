using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp.Language;

namespace UnitTest
{
    [TestClass]
    public class CSharp7UnitTests
    {
        /// <summary>
		///  Gets or sets the test context which provides
		///  information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestTheContext()
        {
			Console.WriteLine(TestContext.TestName);
        }
		[TestMethod]
		public void Deconstruct()
		{
			CSharp7.TestDeconstruct();
		}

        [TestMethod, ExpectedException(typeof(OutOfMemoryException), "Expected OutOfMemoryException" )]
        public void ExpectedException()
        {
            throw new OutOfMemoryException();
        }

        [DataTestMethod]
        [DataRow("a", "b")]
        [DataRow(" ", "a")]
        public void DataTestMethod(string value1, string value2)
        {
            Assert.AreEqual(value1 + value2, string.Concat(value1, value2));
        }
    }
}
