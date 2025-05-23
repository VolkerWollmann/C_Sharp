﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AccessibilityProjectCore
{
    /// <summary>
    /// #internal	: current assembly
    /// #private	: private
    /// #public		: unrestricted 
    /// #protected  : containing class or types derived
    /// </summary>
    public class MyClassAccessibility
    {

        private int _myPrivateNumber = 1;
        internal int MyInternalNumber = 2;
        protected int MyProtectedNumber = 3;
        public int MyPublicNumber = 4;

        protected internal int MyInternalProtected = 5;

        public void Test1()
        {
            int i = _myPrivateNumber;
            _myPrivateNumber = i;
            MyInternalNumber = 42;
            Assert.AreEqual(42, MyInternalNumber);
            MyProtectedNumber = 42;
            Assert.AreEqual(42, MyProtectedNumber);
            MyPublicNumber = 42;
            Assert.AreEqual(42, MyPublicNumber);
            MyInternalProtected = 42;
            Assert.AreEqual(42, MyInternalProtected);
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
                // _MyPrivateNumber = 42 //Error   CS0122  'MyClassAccessibility._myPrivateNumber' is inaccessible due to its protection level AccessibilityProject    E:\C_Sharp\C_Sharp\AccessibilityProject\MyClassAccessibility.cs  55  Active
                MyInternalNumber = 42, 
                MyProtectedNumber = 42, 
                MyPublicNumber = 42, 
                MyInternalProtected = 42
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


    public abstract class MyInterfaceVisibility
    {
        public static void ExplicitImplicitInterfaceImplementation()
        {
            MyExplicitIncrementer myExplicitIncrementer = new MyExplicitIncrementer();

            // cast necessary
            int threeA = ((IIncrement)myExplicitIncrementer).Increment(2);
            Assert.AreEqual(3, threeA);

            MyImplicitIncrementer myImplicitIncrementer = new MyImplicitIncrementer();

            // cast not necessary
            int threeB = myImplicitIncrementer.Increment(2);
            Assert.AreEqual(3, threeB);
        }
    }
}