using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace C_Sharp
{
    // #comparer #equality
	public class MyEqaulityComparer : IEqualityComparer<MyEqaulityComparer>
	{
		public int X { get; private set; }
		public int Y { get; private set; }
		bool IEqualityComparer<MyEqaulityComparer>.Equals(MyEqaulityComparer x, MyEqaulityComparer y)
		{
			return (x.X == y.X) && (y.X == y.Y);
		}

		int IEqualityComparer<MyEqaulityComparer>.GetHashCode(MyEqaulityComparer obj)
		{
			return (obj.X | obj.Y);
		}

		public MyEqaulityComparer(int x, int y)
		{
			X = x;
			Y = y;
		}

		public static void Test()
		{
			MyEqaulityComparer myA = new MyEqaulityComparer(1, 1);
			MyEqaulityComparer myB = new MyEqaulityComparer(1, 1);
			MyEqaulityComparer myC = new MyEqaulityComparer(1, 2);

			bool b1 = (myA.Equals(myA));
			Assert.IsTrue(b1);

			bool b2 = (myA == myB);
			Assert.IsFalse(b2);

			bool b3 = (myA.Equals(myC));
			Assert.IsFalse(b3);

			bool b4 = (myA == myC);
			Assert.IsFalse(b4);

		}
	}

	// #comparer #order
	public class MyComparer
    {

    }
}
