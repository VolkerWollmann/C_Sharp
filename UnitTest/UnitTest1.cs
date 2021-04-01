using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp;
using C_Sharp.FileOperation;
using C_Sharp.Types;
using C_Sharp.InferfaceImplementation;

namespace UnitTest
{
    [TestClass]
	public class AllUnitTests
	{
		[TestMethod]
		public void Enum_Test()
		{
			MyEnum.Enum_Test();
		}

		[TestMethod]
		public void Operator_Test()
		{
			MyOperator.Operator_Test();
		}

		[TestMethod]
		public void AnonymousTypes_Test()
		{
			MyAnonymousType.AnonymousType_Test();
		}

		[TestMethod]
		public void Exception_Test()
		{
			MyException.Exception_Test();
		}

		[TestMethod]
		public void CSharp6Test()
		{
			CSharp6.Test();


		}

		[TestMethod]
		public void Assert_Test()
		{
			CSharp6.Assert_Test();
		}

		[TestMethod]
		public void TestTuple()
		{
			CSharp6.TupleTest();
		}


		[TestMethod]
		public void ExtensionMethod()
		{
			MyExtensionMethod.Test();
		}

		[TestMethod]
		public void TestScartchMethod()
		{
			Scratch.Test();
		}

		[TestMethod]
		public void ClassAccessiblity_Test()
		{
			C_Sharp.AccessiblityBase.MyClassAccessiblityTestA.Test();
			C_Sharp.AccessiblityBase.MyClassAccessiblityTestB.Test();
			C_Sharp.AccessiblityNeigbor.MyClassAccessiblityTest.Test();
			C_Sharp.AccessiblityOtherLibrary.MyClassAccessiblityTest.Test();
		}

		[TestMethod]
		public void ExplicitImplicitInterfaceImplementation()
        {
			MyInterfaceVisibility.ExplicitImplicitInterfaceImplementation();
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
		public void ArrayExentension()
		{
			MyArray.ArrayExtension();
		}

		[TestMethod]
		public void Generics()
		{
			MyGeneric.Test();

		}

		[TestMethod]
		public void SimpleCSharp()
		{
			MySimpleCSharp.Test();
		}

		[TestMethod]
		public void IEnumerable_Test()
		{
			MyIntegerRange.Test();
		}

		[TestMethod]
		public void ReadFileTest()
		{
			MyFileOperation.ReadLinesFromFile();
		}

		[TestMethod]
		public void Cast()
		{
			MyCast.Test();
		}

		[TestMethod]
		public void XML()
        {
			MyXML.Test();
        }

		[TestMethod]
		public void LocalFunction()
        {
			CSharp.LocalFunction();
		}

		[TestMethod]
		public void MultiLineStringConstant()
		{
			CSharp.MultiLineStringConstant();
		}
	}
}
