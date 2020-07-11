using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace C_Sharp
{
    // #comparer #equality
	public class MyComparer : IEqualityComparer<MyComparer>
	{
		public int X { get; private set; }
		public int Y { get; private set; }
		bool IEqualityComparer<MyComparer>.Equals(MyComparer x, MyComparer y)
		{
			return (x.X == y.X) && (y.X == y.Y);
		}

		int IEqualityComparer<MyComparer>.GetHashCode(MyComparer obj)
		{
			return (obj.X | obj.Y);
		}

		public MyComparer(int x, int y)
		{
			X = x;
			Y = y;
		}

		public static void Test()
		{
			MyComparer myA = new MyComparer(1, 1);
			MyComparer myB = new MyComparer(1, 1);
			MyComparer myC = new MyComparer(1, 2);

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
}
