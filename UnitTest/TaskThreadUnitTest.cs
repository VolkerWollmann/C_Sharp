using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp;
using C_Sharp.FileOperation;
using C_Sharp.Types;

namespace UnitTest
{
    [TestClass]
    public class TaskThreadUnitTest
    {
		[TestMethod]
		public void Process()
		{
			MyProcess.Test();
		}

		[TestMethod]
		public void Process_ParallelForeach()
		{
			MyProcess.Test2();
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
		public void ChildTask()
		{
			MyTask.TestChildTask();
		}

	}
}
