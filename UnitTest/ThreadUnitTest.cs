using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyThread = C_SharpExamplesLib.Language.Threads.MyThread;

namespace UnitTest
{
    [TestClass]
    public class ThreadUnitTest
    {
        /// <summary>
		///  Gets or sets the test context which provides
		///  information about and functionality for the current test run.
		///</summary>
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public TestContext TestContext { get; set; }

        [TestMethod]
		public void Thread_Simple()
		{
			MyThread.ThreadSimple();
		}


		[TestMethod]
		public void Thread_Lambda()
		{
			MyThread.TestLambdaThreadWithData();
		}

		[TestMethod]
		public void Thread_ParameterizedThreadStart()
		{
			MyThread.TestParameterizedThreadStart();
		}

		[TestMethod]
		public void Thread_StaticData()
		{
			TestContext.WriteLine("Rabbit");
			MyThread.TestThreadStaticData();
		}

		[TestMethod]
		public void Thread_LocalData()
		{
			MyThread.TestThreadLocalData();
		}

		[TestMethod]
		[Ignore("Only for debug purpose")]
		public void ThreadPool_GradeOfParallelism()
		{
			MyThread.TestTheadPoolWithPrimeSearch();
		}

		[TestMethod]
		[Ignore("Only for debug purpose")]
		public void ThreadAdministrativeData()
		{
			MyThread.Thread_AdministrativeData();
		}

		[TestMethod]
        //[Ignore("Only for debug purpose")]
        public void ThreadPool_Configuration()
        {
            MyThread.TestThreadPoolConfiguration();
        }

		[TestMethod]
		public void Thread_Dispatcher()
		{
			MyThread.Thread_Dispatcher();
		}

        [TestMethod]
		public void Thread_Cancelation()
		{
			MyThread.Thread_Canceling();
		}

		[TestMethod]
		public void Thread_Join()
		{
			MyThread.Thread_Join();
		}

		//[TestMethod]
  //      [Ignore("Only for debug purpose")]
  //      public void Thread_AdministrativeData()
		//{
		//	;
		//	//MyThread.Thread_AdministrativeData();
		//}

        [TestMethod]
		public void Semaphore()
        {
            MyThread.SemaphoreExample();

        }
    }
}
