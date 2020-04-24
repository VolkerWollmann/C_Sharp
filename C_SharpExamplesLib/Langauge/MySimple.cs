using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp
{
	public class MySimple
	{
		static void Method(ref string s)
		{
			s = "donkey";
		}

		public static void Test()
		{
			string s=null;
			Method(ref s);
			Assert.IsTrue(s == "donkey");
		}

	}
}
