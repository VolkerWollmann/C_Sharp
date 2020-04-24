using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp
{
	class MyMath
	{
		internal int A { get; }
		internal int B { get; }
		internal MyMath(int a, int b)
		{
			A = a;
			B = b;
		}
	}
	public static class MyExtensionMethod
	{
		static int Add( this  MyMath m )
		{
			return m.A + m.B; 
		}

		public static void Test()
		{
			MyMath m = new MyMath(3, 4);
			int result = m.Add();
			Assert.AreEqual(7, result);
		}
	}
}
