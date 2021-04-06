using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp;
using C_Sharp.Language;

namespace UnitTest
{
    [TestClass]
    public class CSharp7UnitTests
    {
		private TestContext _testContextInstance;

		/// <summary>
		///  Gets or sets the test context which provides
		///  information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get => _testContextInstance;
            set => _testContextInstance = value;
        }

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
	}
}
