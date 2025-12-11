using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_SharpExamplesLib.FileOperation;
using AccessibilityProjectCore;
using C_SharpExamplesLib.Language;
using MyClassAccessibilityTest = AccessibilityOtherProjectCore.MyClassAccessibilityTest;


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
        public void LoopInvariant()
        {
            MyLoopInvariant.LoopInvariant1();
            MyLoopInvariant.LoopInvariant2();
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
        public void DictionaryDoubleUsage()
        {
            CSharp6.DictionaryDoubleUsage();
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
            AccessibilityProjectCore.MyClassAccessibilityTest.Test();
            MyClassAccessibilityTest.Test();
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
        public void CallByReference()
        {
            MySimpleCSharp.TestCallByReference();
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
        public void MockFile()
        {
            MyFileOperation.MockFile();
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
        public void TestClassInitializer()
        {
            CSharp.TestClassInitializer();
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
            MyYield.Yield();
        }

        [TestMethod]
        public void EnumerableAssignment()
        {
            MyYield.TestIEnumerableAssignment();
        }

        [TestMethod]
        public void ImplicitOperator()
        {
            CSharp.ImplicitExplicitOperator();
        }

        [TestMethod]
        public void ShowBitArray()
        {
            CSharp.ShowBitArray();
        }

        [TestMethod]
        public void DynamicType()
        {
            CSharp.DynamicType();
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

        [TestMethod]
        public void Params()
        {
            CSharp.ParamsTest();
        }

        [TestMethod]
        public void StepThrough()
        {
            DebugStepThrough.StepThroughExample();
        }

        [TestMethod]
        public void TestReturnAssignment()
        {
            CSharp.ReturnAssignment();
        }

        [TestMethod]
        public void StringJoin()
        {
            CSharp.StringJoin();
        }
    }
}
