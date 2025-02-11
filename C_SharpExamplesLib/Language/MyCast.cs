using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
	public class MyCast
	{
		private class MyCastClass1;

		private class MyCastClass2;

		// #type-testing #cast #is #as #is variable name
		private static void DoCasts(object x)
        {
            var mc2 = new MyCastClass2();
			Assert.IsNotNull(mc2);

			// cannot cast tp abstract class
			Action invalidTypeCastAction = () => { var t1 = (MyCastClass2)x; Assert.IsNull(t1); };
            Assert.ThrowsException<InvalidCastException>(invalidTypeCastAction.Invoke);

			// cast with #as
			// cannot cast independent classes on each another, even if they look equal
			MyCastClass2? t2 = x as MyCastClass2;
			Assert.IsNull(t2);

			// check type with cast and assignment 
            if (x is MyCastClass1 t4)
            {
                Assert.IsNotNull(t4);
			}
            
			// check type with #is
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            bool t5 = x is MyCastClass2;
			Assert.IsFalse(t5);


            if (x is MyCastClass1 t6)
			{ 
				Assert.IsNotNull(t6);
            }
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

            Assert.ThrowsException<InvalidCastException>(invalidBaseTypeCastAction.Invoke);


			MyCastClass1 myCastClass1 = new MyCastClass1();
            DoCasts(myCastClass1);

		}
	}
}
