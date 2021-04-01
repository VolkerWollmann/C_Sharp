using System;

namespace C_Sharp
{
	public class MyCast
	{
		private class MyCastClass1
        {

        }

		private class MyCastClass2
		{

		}

		// #type-testing #cast #is #as #is varname
		private static void DoCasts(object x)
        {
			try
			{
				MyCastClass2 t1 = (MyCastClass2)x;
			}
			catch (InvalidCastException )
			{
				;
			}
			MyCastClass2 t2 = x as MyCastClass2;
			bool t3 = (x is MyCastClass1 t4);
			bool t5 = (x is MyCastClass2);

			if (x is MyCastClass1 t6 )
            {
				;
            }

		}
		// #cast #int #long 
		public static void Test()
		{
			long l = 3;
			object o;
			int i;

			try
			{
				o = l;
				i = (int)o;
			}
			catch( InvalidCastException )
			{
				;
			}

			MyCastClass1 myCastClass1 = new MyCastClass1();
            DoCasts((object)myCastClass1);

		}
	}
}
