using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp.AccessiblityOtherLibrary
{ 
	public static class MyClassAccessiblityTest
	{
		public static void Test()
		{
			C_Sharp.AccessiblityBase.MyClassAccessiblity mcaA = new C_Sharp.AccessiblityBase.MyClassAccessiblity();
			mcaA.MyPublicNumber = 42;

			C_Sharp.AccessiblityBase.MyClassAccessiblityTestB mcaB = new C_Sharp.AccessiblityBase.MyClassAccessiblityTestB();
			//mcaB.MyPrivateNumber = 42; //Error CS0122  'MyClassAccessiblity.MyNumber' is inaccessible due to its protection level 
			//mcaB.MyInternalNumber = 42; //Error CS0122  'MyClassAccessiblity.MyNumber' is inaccessible due to its protection level 
			//mcaB.MyProtectedNumber = 42; //Error CS0122  'MyClassAccessiblity.MyNumber' is inaccessible due to its protection level
			mcaB.MyPublicNumber = 42;
			//mcaB.MyInternalProtected = 42; //Error CS0122  'MyClassAccessiblity.MyNumber' is inaccessible due to its protection level
		}
	}
} 