using AccessibilityProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AccessibilityOtherProject
{ 
	public static class MyClassAccessibilityTest
	{
        /// <summary>
        /// Accesses classes from referenced project AccessibilityProject.
        /// </summary>
		public static void Test()
		{
            MyClassAccessibility mcaA = new MyClassAccessibility {MyPublicNumber = 42};

            MyClassAccessibilityTestB mcaB = new MyClassAccessibilityTestB
            {
                MyPublicNumber = 42
                //MyInternalNumber = 42, //Error CS0122  'MyClassAccessibility.MyNumber' is inaccessible due to its protection level 
                //MyProtectedNumber = 42, //Error CS0122  'MyClassAccessibility.MyNumber' is inaccessible due to its protection level
                //MyInternalProtected = 42 //Error CS0122  'MyClassAccessibility.MyNumber' is inaccessible due to its protection level
            };

            Assert.IsNotNull(mcaA);
            Assert.IsNotNull(mcaB);
        }
	}
} 