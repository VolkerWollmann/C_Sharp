using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
	public class MyCast
	{
		private class MyCastClass1
        {

        }

		private class MyCastClass2
		{

		}

		// #type-testing #cast #is #as #is variable name
		private static void DoCasts(object x)
        {

			// cannot cast tp abstract class
			Action InvalidTypeCastAction = () => { MyCastClass2 t1 = (MyCastClass2)x; Assert.IsNull(t1); };
            Assert.ThrowsException<InvalidCastException>(InvalidTypeCastAction);

			// cannot cast independent classes on each another, even if they look equal
			MyCastClass2 t2 = x as MyCastClass2;
			Assert.IsNull(t2);

			// check type with cast and assignment 
            if (x is MyCastClass1 t4)
            {
                Assert.IsNotNull(t4);
			}
            
			// check type with is
			bool t5 = x is MyCastClass2;
			Assert.IsFalse(t5);


            bool t7 = (x is MyCastClass1 t6);
			Assert.IsTrue(t7);
			
			// not accessible
            //Assert.IsNotNull(t6);
            

		}
		// #cast #int #long 
		public static void Test()
		{
			long l = 3;
			object o;
			int i;

            Action invalidBaseTypeCastAction = () =>
            {
                o = l;
                i = (int) o;
                o = i;
            };

            Assert.ThrowsException<InvalidCastException>(invalidBaseTypeCastAction);


			MyCastClass1 myCastClass1 = new MyCastClass1();
            DoCasts((object)myCastClass1);

		}
	}
}
