using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AccessibilityProject
{
    /// <summary>
    /// #internal	: current assembly
    /// #private	: private
    /// #public		: unrestricted 
    /// #protected  : containing class or types derived
    /// </summary>
    public class MyClassAccessibility
    {

        private int _MyPrivateNumber = 1;
        internal int MyInternalNumber = 2;
        protected int MyProtectedNumber = 3;
        public int MyPublicNumber = 4;

        protected internal int MyInternalProtected = 5;

        public void Test1()
        {
            int i = _MyPrivateNumber;
            _MyPrivateNumber = i;
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

            mca.Test1();

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
            MyClassAccessibilityTestB mca2 = new MyClassAccessibilityTestB
            {
                MyInternalNumber = 42, MyProtectedNumber = 42, MyPublicNumber = 42, MyInternalProtected = 42
                //, _myPrivateNumber = 42 //Error   CS0122  'MyClassAccessibility._myPrivateNumber' is inaccessible due to its protection level AccessibilityProject    E:\C_Sharp\C_Sharp\AccessibilityProject\MyClassAccessibility.cs  55  Active

            };
            
            Assert.IsNotNull(mca2);
        }
    }

    public static class MyClassAccessibilityTest
    {
        public static void Test()
        {
            MyClassAccessibilityTestB mcaB = new MyClassAccessibilityTestB
            {
                MyInternalNumber = 42, MyPublicNumber = 42, MyInternalProtected = 42

                //, MyPrivateNumber = 42,    //Error CS0122  'MyClassAccessibility.MyNumber' is inaccessible due to its protection level 
                //, MyProtectedNumber = 42,  //Error CS0122  'MyClassAccessibility.MyNumber' is inaccessible due to its protection level
            };

            Assert.IsNotNull(mcaB);
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