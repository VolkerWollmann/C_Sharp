using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp.AccessiblityBase
{
	/// <summary>
	/// #internal	: current assembly
	/// #private	: private
	/// #public		: unrestricted 
	/// #protected  : containing class or types derived
	/// </summary>
	public class MyClassAccessiblity
	{

		private int MyPrivateNumber = 1;
		internal int MyInternalNumber = 2;
		protected int MyProtectedNumber = 3;
		public int MyPublicNumber = 4;

		internal protected int MyInternalProtected = 5;

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

	public static class MyClassAccessiblityTestA
	{
		public static void Test()
		{
			MyClassAccessiblity mca = new MyClassAccessiblity();
		    //mca.MyPrivateNumber = 42; //Error CS0122  'MyClassAccessiblity.MyNumber' is inaccessible due to its protection level 
			mca.MyInternalNumber = 42;
			//mca.MyProtectedNumber = 42;  //Error CS0122  'MyClassAccessiblity.MyNumber' is inaccessible due to its protection level
			mca.MyPublicNumber = 42;
			mca.MyInternalProtected = 42;
		}
	}

	public class MyClassAccessiblityTestB : MyClassAccessiblity
	{
		public static void Test()
		{
			MyClassAccessiblityTestB mca2 = new MyClassAccessiblityTestB();
			// mca2.MyPrivateNumber = 42; //Error CS0122  'MyClassAccessiblity.MyNumber' is inaccessible due to its protection level
			mca2.MyInternalNumber = 42;
			mca2.MyProtectedNumber = 42;
			mca2.MyPublicNumber = 42;
			mca2.MyInternalProtected = 42;
		}
	}
}

namespace C_Sharp.AccessiblityNeigbor
{
	public static class MyClassAccessiblityTest
	{
		public static void Test()
		{
			C_Sharp.AccessiblityBase.MyClassAccessiblityTestB mcaB = new C_Sharp.AccessiblityBase.MyClassAccessiblityTestB();
			//mcaB.MyPrivateNumber = 42; //Error CS0122  'MyClassAccessiblity.MyNumber' is inaccessible due to its protection level 
			mcaB.MyInternalNumber = 42;
			//mcaB.MyProtectedNumber = 42;  //Error CS0122  'MyClassAccessiblity.MyNumber' is inaccessible due to its protection level
			mcaB.MyPublicNumber = 42;
			mcaB.MyInternalProtected = 42;
		}
	}
}

namespace C_Sharp.InferfaceImplementation
{
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
			return i++;
        }
    }

    class MyImplicitIncrementer : IIncrement
    {
		// implict interface implementation
		public int Increment(int i)
        {
			return i++;
        }
    }


    public class MyInterfaceVisibility
    {
		public static void ExplicitImplicitInterfaceImplementation()
		{
			MyExplicitIncrementer myExplicitIncrementer = new MyExplicitIncrementer();

			// cast necessary
			int threeA = ((IIncrement)myExplicitIncrementer).Increment(2);

			MyImplicitIncrementer myImplicitIncrementer = new MyImplicitIncrementer();

			// cast not necessary
			int threeB = myImplicitIncrementer.Increment(2);
		}
    }
}