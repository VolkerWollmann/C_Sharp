﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp.Language;

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
