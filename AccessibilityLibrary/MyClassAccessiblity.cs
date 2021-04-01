using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AccessibilityLibrary
{
    /// <summary>
    /// #internal	: current assembly
    /// #private	: private
    /// #public		: unrestricted 
    /// #protected  : containing class or types derived
    /// </summary>
    public class MyClassAccessibility
    {

        private int MyPrivateNumber = 1;
        internal int MyInternalNumber = 2;
        protected int MyProtectedNumber = 3;
        public int MyPublicNumber = 4;

        protected internal int MyInternalProtected = 5;

        public void Test1()
        {
            int i = MyPrivateNumber;
            MyPrivateNumber = i;
            MyInternalNumber = 42;
            MyProtectedNumber = 42;
            MyPublicNumber = 42;
            MyInternalProtected = 42;
        }
    }

    public static class MyClassAccessibilityTestA
    {
        public static void Test()
        {
            MyClassAccessibility mca = new MyClassAccessibility();
            //mca.MyPrivateNumber = 42; //Error CS0122  'MyClassAccessibility.MyNumber' is inaccessible due to its protection level 
            mca.MyInternalNumber = 42;
            //mca.MyProtectedNumber = 42;  //Error CS0122  'MyClassAccessibility.MyNumber' is inaccessible due to its protection level
            mca.MyPublicNumber = 42;
            mca.MyInternalProtected = 42;
        }
    }

    public class MyClassAccessibilityTestB : MyClassAccessibility
    {
        public static void Test()
        {
            MyClassAccessibilityTestB mca2 = new MyClassAccessibilityTestB();
            // mca2.MyPrivateNumber = 42; //Error CS0122  'MyClassAccessibility.MyNumber' is inaccessible due to its protection level
            mca2.MyInternalNumber = 42;
            mca2.MyProtectedNumber = 42;
            mca2.MyPublicNumber = 42;
            mca2.MyInternalProtected = 42;
        }
    }

    public static class MyClassAccessibilityTest
    {
        public static void Test()
        {
            MyClassAccessibilityTestB mcaB = new MyClassAccessibilityTestB();
            //mcaB.MyPrivateNumber = 42; //Error CS0122  'MyClassAccessibility.MyNumber' is inaccessible due to its protection level 
            mcaB.MyInternalNumber = 42;
            //mcaB.MyProtectedNumber = 42;  //Error CS0122  'MyClassAccessibility.MyNumber' is inaccessible due to its protection level
            mcaB.MyPublicNumber = 42;
            mcaB.MyInternalProtected = 42;
        }
    }

    interface IIncrement
    {
        int Increment(int i); 
    }

// #interface #explicit implementation
    class MyExplicitIncrementer : IIncrement
    {
        // explicit interface implementation
        int IIncrement.Increment(int i)
        {
            return ++i;
        }
    }

    class MyImplicitIncrementer : IIncrement
    {
        // implicit interface implementation
        public int Increment(int i)
        {
            return ++i;
        }
    }


    public class MyInterfaceVisibility
    {
        public static void ExplicitImplicitInterfaceImplementation()
        {
            MyExplicitIncrementer myExplicitIncrementer = new MyExplicitIncrementer();

            // cast necessary
            int threeA = ((IIncrement)myExplicitIncrementer).Increment(2);
            Assert.AreEqual(threeA,3);

            MyImplicitIncrementer myImplicitIncrementer = new MyImplicitIncrementer();

            // cast not necessary
            int threeB = myImplicitIncrementer.Increment(2);
            Assert.AreEqual(threeB, 3);
        }
    }
}