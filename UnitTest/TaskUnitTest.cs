﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_SharpExamplesLib.Language.Tasks;
using System.Reflection;
using C_SharpExamplesLib.Language.ConcurrentDataTypes;


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
			MyTasks.TestContinueWith();
		}

        [TestMethod]
        public void Task_ConstantTaskResult()
        {
            MyTasks.ConstantTaskResult();
        }

        [TestMethod]
		public void Task_ThreadSafetyViolation()
		{
			MyTasks.Task_ThreadSafetyViolation();
		}

		[TestMethod]
		public void Task_ObjectLock()
		{
            MyLock.SharedTotal.TestTaskObjectLock();
		}

		[TestMethod]
		public void Task_Monitor()
		{
			MyTasks.TestTaskMonitor();
		}

		[TestMethod]
		public void Task_Interlocked()
		{
			MyTasks.TestTaskInterlocked();
		}

		[TestMethod]
		public void Task_DeadLock()
        {
			MyTasks.TaskDeadLock();
        }

		[TestMethod]
		public void Task_ChildTask()
		{
			MyTasks.TestChildTask();
		}

		[TestMethod]
		public void Task_AsyncAwait()
        {
			MyTasks.Test_AsyncAwait(); 
        }

		[TestMethod]
		public void Task_AsyncAwaitException()
		{
			MyTasks.Test_AsyncAwaitException();
		}

		[TestMethod]
		public void Task_AsyncAwaitWhenAll()
		{
			MyTasks.Test_AsyncAwaitWhenAll();
		}

		[TestMethod]
		public void Task_BlockingCollection()
        {
			MyTasks.Test_BlockingCollection();
        }

		[TestMethod]
		public void Task_Cancellation()
        {
			MyTasks.Task_Cancellation();
        }

		[TestMethod]
		public void Task_OperationCanceledException()
		{
			MyTasks.Task_OperationCanceledException();
		}

        [TestMethod]
        public void Task_SchedulerTest_AsInfiniteLoop()
        {
            MyTasks.Task_SchedulerTest_AsInfiniteLoop();
        }

        [TestMethod]
        public void Task_SchedulerTest_Timer()
        {
            MyTasks.Task_SchedulerTest_Timer();
        }

        [TestMethod]
        public void Task_SchedulerTest_Timer_Task()
        {
            MyTasks.Task_SchedulerTest_Timer_Task();
        }

        [TestMethod]
		public void Task_Event()
        {
			MyTaskEvent.TaskEvent();
		}

    }
}
