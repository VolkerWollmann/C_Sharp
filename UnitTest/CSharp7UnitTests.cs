using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp;
using C_Sharp.Langauge;

namespace UnitTest
{
    [TestClass]
    public class CSharp7UnitTests
    {
		private TestContext testContextInstance;

		/// <summary>
		///  Gets or sets the test context which provides
		///  information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get => testContextInstance;
            set => testContextInstance = value;
        }

		[TestMethod]
		public void Deconstruct()
		{
			CSharp7.TestDeconstruct();
		}
	}
}
