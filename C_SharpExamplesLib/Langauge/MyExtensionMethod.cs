using Microsoft.VisualStudio.TestTools.UnitTesting;

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
		// #Extension method
		// extended class without creating new class or modifying base class
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
