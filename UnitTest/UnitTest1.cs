using AccessibilityProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp.FileOperation;
using C_Sharp.Language;


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
        public void NullableReferences()
        {
            CSharp6.NullableReferences();
        }

		[TestMethod]
		public void ExtensionMethod()
		{
			MyExtensionMethod.Test();
		}

		[TestMethod]
		public void TestScratchMethod()
		{
			Scratch.Test();
		}

		[TestMethod]
		public void ClassAccessibility_Test()
		{
			MyClassAccessibilityTestA.Test();
			MyClassAccessibilityTestB.Test();
			MyClassAccessibilityTest.Test();
            AccessibilityOtherProject.MyClassAccessibilityTest.Test();
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
		public void ArrayExtension()
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
		public void TestXml()
        {
			MyXml.Test();
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

        [TestMethod]
        public void MultipleInheritance()
        {
            MyMultipleInheritanceTest.Test();
        }

		[TestMethod]
        public void ListCapacity()
        {
            MyListTest.Test();
        }

        [TestMethod]
        public void VirtualMethod()
        {
            VirtualTest.Test();
        }
	}
}
