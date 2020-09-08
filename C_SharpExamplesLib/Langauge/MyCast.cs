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
		}
	}
}
