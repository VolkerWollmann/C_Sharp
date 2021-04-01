using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp;
using C_Sharp.Langauge;

namespace UnitTest
{
    [TestClass]
    public class DelgateUnitTest
    {
		[TestMethod]
		public void TestActionFuncPredicate()
		{
			MyDelegate.TestActionFuncPredicate();
		}

		[TestMethod]
		public void TestDelgate()
		{
			MyDelegate.TestDelegateAndFunc();
		}

		[TestMethod]
		public void TestDelegateFuncInvocationList()
		{
			MyDelegate.TestDelegateFuncInvocationList();
		}

		[TestMethod]
		public void TestDelegateAssignmentByName()
		{
			MyDelegate.TestDelegateAssignmentByName();
		}

		[TestMethod]
		public void TestDelegateAssignmentByMethodInfo()
		{
			MyDelegate.TestDelegateAssignmentByMethodInfo();
		}
	}
}
