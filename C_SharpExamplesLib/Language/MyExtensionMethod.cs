using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language
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
			MyMath m = new MyMath(2, 4);
			Assert.AreEqual(2,m.A);
			m.A = 3;
			
            int result = m.Add();
			Assert.AreEqual(7, result);

            MyMath result1 = m.IncA().IncA();
            Assert.AreEqual(5, result1.A);

            MyMath result2 = m.IncB().IncB();
            Assert.AreEqual(6, result2.B);

            MyMath DoubleIncA(MyMath math) => math.IncA().IncA();
            MyMath result3 = DoubleIncA(m);
            Assert.AreEqual(7, result3.A);
		}
	}
}
