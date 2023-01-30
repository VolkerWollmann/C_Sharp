using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
    internal interface IInheritanceInterface
    {
        int GetValue();
    }

    internal class BaseClass : IInheritanceInterface
    {
        public int GetValue()
        {
            return 1;
        }
    }

    internal class DerivedClass : BaseClass, IInheritanceInterface
    {
        // similar to #new 
        int IInheritanceInterface.GetValue()
        {
            return 2;
        }
    }

    internal class DerivedClassWithExplictImplementation : BaseClass, IInheritanceInterface
    {
        // #new
        public new int GetValue()
        {
            return 3;
        }
    }

    public class MyInheritanceInterfaceTest
    {
        public static void Test()
        {
            BaseClass baseClass = new BaseClass();
            int baseClassValue = baseClass.GetValue();
            Assert.AreEqual(1, baseClassValue);

            DerivedClass derivedClass = new DerivedClass();
            int derivedClassValue = derivedClass.GetValue();
            Assert.AreEqual(1, derivedClassValue);

            int derivedClassValueCasted = ((IInheritanceInterface) derivedClass).GetValue();
            Assert.AreEqual(2, derivedClassValueCasted);

            DerivedClassWithExplictImplementation derivedClassWithExplictImpl = new DerivedClassWithExplictImplementation();
            int derivedClassWithExplictImplValue = derivedClassWithExplictImpl.GetValue();
            Assert.AreEqual(3, derivedClassWithExplictImplValue);
        }
    }
}