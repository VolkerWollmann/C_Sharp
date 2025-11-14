using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Reflection;
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
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestTheContext()
        {
            Console.WriteLine(TestContext.TestName);
            var me = MethodBase.GetCurrentMethod()?.Name;
            Assert.AreEqual(TestContext.TestName, me);
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

        [TestMethod]
        public void ReturnValueTuple()
        {
            CSharp7.ReturnValueTuple();
        }

        // #ExpectedException
        //[TestMethod] 
        //[ExpectedException(typeof(OutOfMemoryException), "Expected OutOfMemoryException" )]
        //public void ExpectedException()
        //{
        //    throw new OutOfMemoryException();
        //}

        // #DataTestMethod #DataRow #UintTest #Parameter
        [TestMethod]
        [DataRow("a", "b")]
        [DataRow(" ", "a")]
        public void DataTestMethod(string value1, string value2)
        {
            Assert.AreEqual(value1 + value2, string.Concat(value1, value2));
        }


        private static IEnumerable<object[]> GetDynamicData()
        {
            yield return ["a", "b"];
            yield return [" ", "b"];
        }

        // #DynamicData #UintTest #Parameter
        [DynamicData(nameof(GetDynamicData))]
        [TestMethod()]
        public void DynamicDataTestMethod(string value1, string value2)
        {
            Assert.AreEqual(value1 + value2, string.Concat(value1, value2));
        }

    }
}
