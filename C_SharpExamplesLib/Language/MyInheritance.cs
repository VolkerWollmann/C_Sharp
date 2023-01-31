using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
    // #interface #implenetation #explicit
    internal interface IInheritanceInterface
    {
        int GetValue();
        int GetValue2();
    }

    internal class BaseClass : IInheritanceInterface
    {
        // as public members
        public int GetValue()
        {
            return 1;
        }

        public int GetValue2()
        {
            return 42;
        }
    }

    internal class DerivedClassWithPartialExplicit : BaseClass, IInheritanceInterface
    {
        // similar to #new 
        int IInheritanceInterface.GetValue()
        {
            return 2;
        }
    }

    internal class DerivedClassWithPartialNewImplementation : BaseClass
    {
        // #new
        public new int GetValue()
        {
            return 3;
        }
    }

    internal class BaseClassWithExplicitImplementation : IInheritanceInterface
    {
        int IInheritanceInterface.GetValue()
        {
            return 4;
        }

        int IInheritanceInterface.GetValue2()
        {
            return 42;
        }
    }

    internal class DerivedClassFromBaseClassWithExplicitImplementation : BaseClassWithExplicitImplementation, IInheritanceInterface
    {
        // similar to #new 
        int IInheritanceInterface.GetValue()
        {
            return 5;
        }

        int IInheritanceInterface.GetValue2()
        {
            return 6;
        }
    }

    internal class DerivedClassFromBaseClassWithPartialImplementation : BaseClassWithExplicitImplementation, IInheritanceInterface
    {
        // similar to #new 
        int IInheritanceInterface.GetValue()
        {
            return 7;
        }
    }

    //internal class DerivedClassFromBaseClassWithNewImplementation : BaseClassWithExplicitImplementation, IInheritanceInterface
    //{
    //    // will not compile
    //    new int IInheritanceInterface.GetValue()
    //    {
    //        return 7;
    //    }
    //}

    public class MyInheritanceInterfaceTest
    {
        public static void Test()
        {
            BaseClass baseClass = new BaseClass();
            int baseClassValue = baseClass.GetValue();
            Assert.AreEqual(1, baseClassValue);

            DerivedClassWithPartialExplicit derivedClass = new DerivedClassWithPartialExplicit();
            int derivedClassValue = derivedClass.GetValue();
            Assert.AreEqual(1, derivedClassValue);

            int derivedClassValueCasted = ((IInheritanceInterface) derivedClass).GetValue();
            Assert.AreEqual(2, derivedClassValueCasted);

            DerivedClassWithPartialNewImplementation derivedClassWithNewImpl = 
                new DerivedClassWithPartialNewImplementation();
            int derivedClassWithExplictImplValue = derivedClassWithNewImpl.GetValue();
            Assert.AreEqual(3, derivedClassWithExplictImplValue);

            BaseClassWithExplicitImplementation baseClassWithExplicitImplementation =
                new BaseClassWithExplicitImplementation();
            // will not compile
            //int baseClassWithExplicitImplementationValue = baseClassWithExplicitImplementation.GetValue();
            int baseClassWithExplicitImplementationValue = 
                ((IInheritanceInterface)baseClassWithExplicitImplementation).GetValue();
            Assert.AreEqual(4, baseClassWithExplicitImplementationValue);

            DerivedClassFromBaseClassWithExplicitImplementation derivedClassFromBaseClassWithExplicitImplementation 
                = new DerivedClassFromBaseClassWithExplicitImplementation();
            // will not compile
            //int derivedClassFromBaseClassWithExplicitImplementationValue =
            //    derivedClassFromBaseClassWithExplicitImplementation.GetValue();
            int derivedClassFromBaseClassWithExplicitImplementationValue =
                ((IInheritanceInterface)derivedClassFromBaseClassWithExplicitImplementation).GetValue();
            Assert.AreEqual(5, derivedClassFromBaseClassWithExplicitImplementationValue);

            DerivedClassFromBaseClassWithPartialImplementation derivedClassFromBaseClassWithPartialImplementation =
                new DerivedClassFromBaseClassWithPartialImplementation();
            int derivedClassFromBaseClassWithPartialImplementationValue =
                ((IInheritanceInterface)derivedClassFromBaseClassWithPartialImplementation).GetValue();
            Assert.AreEqual(7, derivedClassFromBaseClassWithPartialImplementationValue);

        }
    }
}