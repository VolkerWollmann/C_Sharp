﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp;
using C_Sharp.Language;

namespace UnitTest
{
	[TestClass]
	public class CompareUnitTest
    {
		private TestContext testContextInstance;

		/// <summary>
		///  Gets or sets the test context which provides
		///  information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get => testContextInstance;
            set => testContextInstance = value;
        }

		[TestMethod]
		public void TestIComparable()
		{
			MyIComparable.TestIComparable();
		}

		[TestMethod]
		public void TestComparison()
		{
			MyIComparable.TestComparison();
		}

		[TestMethod]
		public void TestIEqualityComparer()
		{
			MyIEqualityComparer.Test();
		}

		[TestMethod]
		public void TestIEquatable()
        {
			MyIEquatable.Test();
        }
	}
}
