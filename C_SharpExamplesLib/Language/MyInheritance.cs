using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language
{
    // #interface #implementation #explicit
    internal interface IInheritanceInterface
    {
        int GetValue();
        int GetValue2();
    }

    internal class ClassPublicImpl : IInheritanceInterface
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

    internal class ClassPublicImplDerivedPartialImpl : ClassPublicImpl, IInheritanceInterface
    {
        // similar to #new 
        int IInheritanceInterface.GetValue()
        {
            return 2;
        }
    }

    internal class ClassPublicImplDerivedNewPublicImpl : ClassPublicImpl
    {
        // #new
        public new int GetValue()
        {
            return 3;
        }
    }

    internal class ClassExplicitImpl : IInheritanceInterface
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

    internal class ClassExplicitImplDerivedExplicitImpl : ClassExplicitImpl, IInheritanceInterface
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

    internal class ClassExplicitImplDerivedPartialExplicitImpl : ClassExplicitImpl, IInheritanceInterface
    {
        // similar to #new 
        int IInheritanceInterface.GetValue()
        {
            return 7;
        }
    }

    //internal class ClassExplicitImplDerivedNewExplicitImpl : ClassExplicitImpl, IInheritanceInterface
    //{
    //    // will not compile
    //    new int IInheritanceInterface.GetValue()
    //    {
    //        return 7;
    //    }
    //}

    public abstract class MyInheritanceInterfaceTest
    {
        [SuppressMessage("ReSharper", "IdentifierTypo")]
        [SuppressMessage("ReSharper", "CommentTypo")]
        public static void Test()
        {
            ClassPublicImpl cpi = new ClassPublicImpl();
            int cpiv = cpi.GetValue();
            Assert.AreEqual(1, cpiv);

            IInheritanceInterface iCpi = cpi;
            Assert.AreEqual(42, iCpi.GetValue2());

            ClassPublicImplDerivedPartialImpl cpidpi = new ClassPublicImplDerivedPartialImpl();
            int cpidpiv = cpidpi.GetValue();
            Assert.AreEqual(1, cpidpiv);

            int derivedClassValueCasted = ((IInheritanceInterface)cpidpi).GetValue();
            Assert.AreEqual(2, derivedClassValueCasted);

            ClassPublicImplDerivedNewPublicImpl cpidnpi = new ClassPublicImplDerivedNewPublicImpl();
            int cpidnpiv = cpidnpi.GetValue();
            Assert.AreEqual(3, cpidnpiv);

            ClassExplicitImpl cei = new ClassExplicitImpl();
            // will not compile
            //int ceiv = cei.GetValue();
            int ceiv = ((IInheritanceInterface)cei).GetValue();
            Assert.AreEqual(4, ceiv);

            ClassExplicitImplDerivedExplicitImpl ceidei = new ClassExplicitImplDerivedExplicitImpl();
            // will not compile
            //int ceideiv = ceidei.GetValue();
            int ceideiv = ((IInheritanceInterface)ceidei).GetValue();
            Assert.AreEqual(5, ceideiv);

            ClassExplicitImplDerivedPartialExplicitImpl ceidpei =
                new ClassExplicitImplDerivedPartialExplicitImpl();
            int ceidpeiv = ((IInheritanceInterface)ceidpei).GetValue();
            Assert.AreEqual(7, ceidpeiv);

        }
    }
}