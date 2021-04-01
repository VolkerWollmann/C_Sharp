namespace C_Sharp.AccessiblityOtherLibrary
{ 
	public static class MyClassAccessiblityTest
	{
		public static void Test()
		{
			C_Sharp.AccessibilityBase.MyClassAccessibility mcaA = new C_Sharp.AccessibilityBase.MyClassAccessibility();
			mcaA.MyPublicNumber = 42;

			C_Sharp.AccessibilityBase.MyClassAccessibilityTestB mcaB = new C_Sharp.AccessibilityBase.MyClassAccessibilityTestB();
			//mcaB.MyPrivateNumber = 42; //Error CS0122  'MyClassAccessibility.MyNumber' is inaccessible due to its protection level 
			//mcaB.MyInternalNumber = 42; //Error CS0122  'MyClassAccessibility.MyNumber' is inaccessible due to its protection level 
			//mcaB.MyProtectedNumber = 42; //Error CS0122  'MyClassAccessibility.MyNumber' is inaccessible due to its protection level
			mcaB.MyPublicNumber = 42;
			//mcaB.MyInternalProtected = 42; //Error CS0122  'MyClassAccessibility.MyNumber' is inaccessible due to its protection level
		}
	}
} 