using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_SharpExamplesLib.Language;

namespace UnitTest
{
    [TestClass]
    public class DelegateUnitTest
    {
		[TestMethod]
		public void TestActionFuncPredicate()
		{
			MyDelegate.TestActionFuncPredicate();
		}

		[TestMethod]
		public void TestDelegate()
		{
			MyDelegate.TestDelegateAndFunc();
		}

        [TestMethod]
        public void TestMemberwiseClone()
        {
            MyMemberwiseClone.Test();
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

        [TestMethod]
        public void TestFuncConcatenation()
        {
            MyDelegate.TestFuncConcatenation();
        }
	}
}
