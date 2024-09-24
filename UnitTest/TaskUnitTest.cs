using System;
using C_Sharp.Language.ConcurrentDataTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp.Language.Task;
using System.Reflection;


namespace UnitTest
{
    [TestClass]
    public class TaskUnitTest
    {
        /// <summary>
		///  Gets or sets the test context which provides
		///  information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext { get; set; }

		[TestMethod]
		//[Ignore("Only for debug purpose")]
		public void TestTheContext()
		{

			Console.WriteLine(TestContext.TestName);
			var me = MethodBase.GetCurrentMethod()?.Name;
			Assert.AreEqual(TestContext.TestName, me);
		}

		[TestMethod]
		public void Task_ContinueWith()
		{
			MyTask.TestContinueWith();
		}

        [TestMethod]
        public void Task_ConstantTaskResult()
        {
            MyTask.ConstantTaskResult();
        }

        [TestMethod]
		public void Task_ThreadSafetyViolation()
		{
			MyTask.Task_ThreadSafetyViolation();
		}

		[TestMethod]
		public void Task_ObjectLock()
		{
            MyLock.SharedTotal.TestTaskObjectLock();
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
        public void Task_SchedulerTest_AsInfiniteLoop()
        {
            MyTask.Task_SchedulerTest_AsInfiniteLoop();
        }

        [TestMethod]
        public void Task_SchedulerTest_Timer()
        {
            MyTask.Task_SchedulerTest_Timer();
        }

        [TestMethod]
        public void Task_SchedulerTest_Timer_Task()
        {
            MyTask.Task_SchedulerTest_Timer_Task();
        }

        [TestMethod]
		public void Task_Event()
        {
			MyTaskEvent.TaskEvent();
		}

    }
}
