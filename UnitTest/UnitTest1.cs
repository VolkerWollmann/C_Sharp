using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp;
using C_Sharp.FileOperation;
using C_Sharp.Types;

namespace UnitTest
{
	[TestClass]
	public class UnitTest1
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
		public void MyEnumTest()
		{
			MyEnum.Test();
		}

		[TestMethod]
		public void MyOperatorsTest()
		{
			MyOperators.Test();
		}

		[TestMethod]
		public void MyAnonymousTypesTest()
		{
			MyAnonymousType.Test();
		}

		[TestMethod]
		public void MyDelegateTest()
		{
			MyDelegate.Test();
			MyDelegate.Test2();
			MyDelegate.Test3();
			MyDelegate.Test4();
		}

		[TestMethod]
		public void MyExceptionTest()
		{
			MyException.Test();
		}

		[TestMethod]
		public void CShart6Test()
		{
			CSharp6.Test();


		}

		[TestMethod]
		public void Linq1()
		{
			MyLinq.Test();
		}

		[TestMethod]
		public void Process()
		{
			MyProcess.Test();
		}

		[TestMethod]
		public void TestAssert()
		{
			CSharp6.TestAssert();
		}

		[TestMethod]
		public void TestReject()
		{
			CSharp6.TupleTest();
		}


		[TestMethod]
		public void TestExtensionMethod()
		{
			MyExtensionMethod.Test();
		}

		[TestMethod]
		public void TestScartchMethod()
		{
			Scratch.Test();
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
		public void MyClassAccessiblity()
		{
			C_Sharp.AccessiblityBase.MyClassAccessiblityTestA.Test();
			C_Sharp.AccessiblityBase.MyClassAccessiblityTestB.Test();
			C_Sharp.AccessiblityNeigbor.MyClassAccessiblityTest.Test();
			C_Sharp.AccessiblityOtherLibrary.MyClassAccessiblityTest.Test();
		}

		[TestMethod]
		public void Types()
		{
			MyType.Test();
		}

		[TestMethod]
		public void AbstractClass()
		{
			AbstractClassTest.Test();
		}

		[TestMethod]
		public void Arrays()
		{
			MyArray.Test();
		}

		[TestMethod]
		public void Generics()
		{
			MyGeneric.Test();
			
		}

		[TestMethod]
		public void Comparer()
		{
			MyComparer.Test();
		}

		[TestMethod]
		public void SimpleCSharp()
		{
			MySimpleCSharp.Test();
		}

		[TestMethod]
		public void IEnumerableTest()
		{
			MyIntegerRange.Test();
		}

		[TestMethod]
		public void ReadFileTest()
		{
			MyFileOperation.ReadLinesFromFile();
		}

		[TestMethod]
		public void SetOperations()
		{
			MyLinq.TestSetOperation();
		}

		[TestMethod]
		public void Cast()
		{
			MyCast.Test();
		}
	}
}
