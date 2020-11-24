using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp;

namespace UnitTest
{
    [TestClass]
    public class ThreadUnitTest
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
		public void ThreadSimple()
		{
			MyThread.ThreadSimple();
		}


		[TestMethod]
		public void ThreadLambda()
		{
			MyThread.TestLambdaThreadWithData();
		}

		[TestMethod]
		public void ThreadParameterizedThreadStart()
		{
			MyThread.TestParameterizedThreadStart();
		}

		[TestMethod]
		public void ThreadStaticData()
		{
			TestContext.WriteLine("Hase");
			MyThread.TestThreadStaticData();
		}

		[TestMethod]
		public void ThreadLocalData()
		{
			MyThread.TestThreadLocalData();
		}

		[TestMethod]
		public void Thread_Dispatcher()
		{
			MyThread.Thread_Dispatcher();
		}

		[TestMethod]
		public void Thread_ParallelFor()
		{
			MyThread.ParallelFor();
		}
	}
}
