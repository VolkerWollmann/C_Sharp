﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp;
using C_Sharp.FileOperation;
using C_Sharp.Types;

namespace UnitTest
{
    [TestClass]
    public class TaskThreadUnitTest
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
		public void ThreadMethod()
		{
			MyThread.TestThreadMethodWidData();
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

		[TestMethod]
		public void TaskContinueWith()
		{
			MyTask.TestContinueWith();
		}

		[TestMethod]
		public void TaskObjectLock()
		{
			MyTask.TestTaskObjectLock();
		}

		[TestMethod]
		public void TaskChildTask()
		{
			MyTask.TestChildTask();
		}

	}
}
