using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		// #type-testing #cast #is #as
		private static void DoCasts(object x)
        {
			try
			{
				MyCastClass2 t1 = (MyCastClass2)x;
			}
			catch (InvalidCastException ivc)
			{
				;
			}
			MyCastClass2 t2 = x as MyCastClass2;
			bool t3 = (x is MyCastClass1 t4);
			bool t5 = (x is MyCastClass2);

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
			catch( InvalidCastException ivc)
			{
				;
			}

			MyCastClass1 myCastClass1 = new MyCastClass1();
            DoCasts((object)myCastClass1);

		}
	}
}
