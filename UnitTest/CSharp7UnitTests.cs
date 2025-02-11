using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp.Language;
using System.Collections.Generic;
using C_SharpExamplesLib.Language;

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

        [TestMethod]
        public void BigInteger()
        {
            CSharp7.TestBigInteger();
        }

        // #ExpectedException
        [TestMethod, ExpectedException(typeof(OutOfMemoryException), "Expected OutOfMemoryException" )]
        public void ExpectedException()
        {
            throw new OutOfMemoryException();
        }

        // #DataTestMethod #DataRow #UintTest #Parameter
        [DataTestMethod]
        [DataRow("a", "b")]
        [DataRow(" ", "a")]
        public void DataTestMethod(string value1, string value2)
        {
            Assert.AreEqual(value1 + value2, string.Concat(value1, value2));
        }


        private static IEnumerable<object[]> GetDynamicData()
        {
            yield return new object[] { "a", "b" };
            yield return new object[] { " ", "b" };
        }

        // #DynamicData #UintTest #Parameter
        [DynamicData(nameof(GetDynamicData), DynamicDataSourceType.Method)] 
        [TestMethod()]
        public void DynamicDataTestMethod(string value1, string value2)
        {
            Assert.AreEqual(value1 + value2, string.Concat(value1, value2));
        }
        
    }
}
