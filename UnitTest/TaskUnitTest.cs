using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp;
using C_Sharp.Language.Task;


namespace UnitTest
{
    [TestClass]
    public class TaskUnitTest
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
		public void Task_ContinueWith()
		{
			MyTask.TestContinueWith();
		}

		[TestMethod]
		public void Task_ThreadSafetyViolation()
		{
			MyTask.Task_ThreadSafetyViolation();
		}

		[TestMethod]
		public void Task_ObjectLock()
		{
			MyTask.TestTaskObjectLock();
		}

		[TestMethod]
		public void Task_Monitor()
		{
			MyTask.TestTaskMonitor();
		}

		[TestMethod]
		public void Task_Interlocked()
		{
			MyTask.TestTaskInterlocked();
		}

		[TestMethod]
		public void Task_DeadLock()
        {
			MyTask.TaskDeadLock();
        }

		[TestMethod]
		public void Task_ChildTask()
		{
			MyTask.TestChildTask();
		}

		[TestMethod]
		public void Task_AsyncAwait()
        {
			MyTask.Test_AsyncAwait(); 
        }

		[TestMethod]
		public void Task_AsyncAwaitException()
		{
			MyTask.Test_AsyncAwaitException();
		}

		[TestMethod]
		public void Task_AsyncAwaitWhenAll()
		{
			MyTask.Test_AsyncAwaitWhenAll();
		}

		[TestMethod]
		public void Task_BlockingCollection()
        {
			MyTask.Test_BlockingCollection();
        }

		[TestMethod]
		public void Task_ConcurrentStack()
		{
			MyTask.Test_ConcurrentStack();
		}

		[TestMethod]
		public void Task_ConcurrentQueue()
		{
			MyTask.Test_ConcurrentQueue();
		}

		[TestMethod]
		public void Task_ConcurrentDictionary()
        {
			MyTask.Test_ConcurrentDictionary();
        }

		[TestMethod]
		public void Task_Cancellation()
        {
			MyTask.Task_Cancellation();
        }

		[TestMethod]
		public void Task_OperationCanceledException()
		{
			MyTask.Task_OperationCanceledException();
		}

		[TestMethod]
		public void Task_Event()
        {
			MyTaskEvent.TaskEvent();
		}
	}
}
