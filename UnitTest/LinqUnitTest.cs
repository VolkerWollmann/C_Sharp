using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp;
using C_Sharp.FileOperation;
using C_Sharp.Types;

namespace UnitTest
{
    [TestClass]
    public class LinqUnitTest
    {
		[TestMethod]
		public void Linq1()
		{
			MyLinq.Test();
		}

		[TestMethod]
		public void ParallelLinq()
		{
			MyLinq.TestParallelLinq();
		}

		[TestMethod]
		public void ParallelLinq_Exception()
        {
			MyLinq.PLinqExceptions();

		}
	}
}
