using AccessibilityLibrary;

namespace C_SharpAccessibility2
{ 
	public static class MyClassAccessibilityTest
	{
		public static void Test()
		{
			MyClassAccessibility mcaA = new MyClassAccessibility();
			mcaA.MyPublicNumber = 42;

			MyClassAccessibilityTestB mcaB = new MyClassAccessibilityTestB();
			//mcaB.MyPrivateNumber = 42; //Error CS0122  'MyClassAccessibility.MyNumber' is inaccessible due to its protection level 
			//mcaB.MyInternalNumber = 42; //Error CS0122  'MyClassAccessibility.MyNumber' is inaccessible due to its protection level 
			//mcaB.MyProtectedNumber = 42; //Error CS0122  'MyClassAccessibility.MyNumber' is inaccessible due to its protection level
			mcaB.MyPublicNumber = 42;
			//mcaB.MyInternalProtected = 42; //Error CS0122  'MyClassAccessibility.MyNumber' is inaccessible due to its protection level
		}
	}
} 