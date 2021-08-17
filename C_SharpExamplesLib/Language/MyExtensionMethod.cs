using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
	class MyMath
	{
		internal int A { get; set; }
		internal int B { get; set; }

        internal MyMath IncA()
        {
            A = A + 1;
            return this;
        }

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

        static MyMath IncB(this MyMath m)
        {
            m.B = m.B + 1;
            return m;
        }

		public static void Test()
		{
			MyMath m = new MyMath(3, 4);
			
            int result = m.Add();
			Assert.AreEqual(7, result);

            MyMath resultA = m.IncA().IncA();
            Assert.AreEqual(5, resultA.A);

            MyMath resultB = m.IncB().IncB();
            Assert.AreEqual(6, resultB.B);

            MyMath DoubleIncA(MyMath math) => math.IncA().IncA();
            MyMath resultC = DoubleIncA(m);
            Assert.AreEqual(7, resultC.A);
		}
	}
}
