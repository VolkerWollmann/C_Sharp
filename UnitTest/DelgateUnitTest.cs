using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp;

namespace UnitTest
{
    [TestClass]
    public class DelgateUnitTest
    {
		[TestMethod]
		public void TestDelgateAndFunc()
		{
			MyDelegate.TestDelgateAndFunc();
		}

		[TestMethod]
		public void TestDelgateFuncInvocationList()
		{
			MyDelegate.TestDelgateFuncInvocationList();
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
