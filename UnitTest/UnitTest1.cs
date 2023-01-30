using ThisAccessibilityProject = AccessibilityProject ;
using OtherAccessibilityProject = AccessibilityOtherProject;
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
			ThisAccessibilityProject.MyClassAccessibilityTestA.Test();
			ThisAccessibilityProject.MyClassAccessibilityTestB.Test();
			ThisAccessibilityProject.MyClassAccessibilityTest.Test();
			OtherAccessibilityProject.MyClassAccessibilityTest.Test();
		}

		[TestMethod]
		public void ExplicitImplicitInterfaceImplementation()
        {
			ThisAccessibilityProject.MyInterfaceVisibility.ExplicitImplicitInterfaceImplementation();
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
		public void Reflection()
		{
			MyReflection.Test();
		}

		[TestMethod]
		public void SimpleCSharp()
		{
			MySimpleCSharp.Test();
		}

        [TestMethod]
        public void RecursiveClassTest()
        {
            CSharp.MyRecursiveCLass.Test();
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
        public void Attribute()
        {
			MyAttributedClassTest.Test();
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
		public void NamedParameters()
        {
			CSharp.NamedParameters();
        }

		[TestMethod]
		public void OptionalParameters()
		{
			CSharp.OptionalParameters();
		}

        [TestMethod]
		public void ShowCompilerServices()
        {
			CSharp.ShowCompilerServices();
		}

		[TestMethod]
		public void LazyClassTest()
		{
			CSharp.LazyClassTest();
		}
		

		[TestMethod]
        public void MultipleInheritance()
        {
            MyMultipleInheritanceTest.Test();
        }

        [TestMethod]
        public void InterfaceInheritance()
        {
            MyInheritanceInterfaceTest.Test();
        }

        [TestMethod]
        public void VirtualMethod()
        {
            VirtualTest.Test();
        }

        [TestMethod]
        public void Yield()
        {
            MyYield.Test();
        }

        [TestMethod]
        public void ImplicitOperator()
        {
            CSharp.ImplicitExplicitOperator();
        }

        [TestMethod]
        public void ObsoleteMethod()
        {
			MyObsolete.Test();
        }

        [TestMethod]
        public void ToStringTest()
        {
            CSharp.ToStringExamples();
        }

        [TestMethod]
        public void Goto()
        {
            CSharp.GotoTest();
        }
    }
}
