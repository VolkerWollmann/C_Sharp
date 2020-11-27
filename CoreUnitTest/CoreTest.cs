using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpCore;

namespace CoreUnitTest
{
    [TestClass]
    public class CoreTest
    {
		private TestContext testContextInstance;

		/// <summary>
		///  Gets or sets the test context which provides
		///  information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get { return testContextInstance; }
			set { testContextInstance = value; }
		}

		[TestMethod]
		public void CSarp8_Operators()
		{
			CSharp8.Operators();
		}
	}
}
