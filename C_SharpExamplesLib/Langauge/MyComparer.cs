using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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

	// #comparer #order #DebuggerDisplay
	[DebuggerDisplay("Version = {Version}, Animal={Animal}")]
	public class MyComparable : IComparable<MyComparable>
	{
		private const string DONKEY = "Esel";
		private const string DOG = "Hund";
		private const string SEAGULL = "Möwe";
		private const string CAT = "Katze";

		public int Version { get; private set; }
		public string Animal { get; private set; }

		#region Constructor 
		private MyComparable(int version, string animal)
		{
			Animal = animal;
			Version = version;

		}
		#endregion

		#region IComparable<MyOrderComparer>
		// < 0 This instance precedes obj in the sort order.
		// = 0 This instance occurs in the same position in the sort order as obj.
		// > 0 This instance follows obj in the sort order.

		int IComparable<MyComparable>.CompareTo(MyComparable other)
		{
			if (this.Version < other.Version)
				return -1;

			if (this.Version > other.Version)
				return 1;

			Dictionary<string, int> animalOrder = new Dictionary<string, int> { [DONKEY] = 1, [DOG] = 2, [SEAGULL] = 3, [CAT] = 4 };

			return animalOrder[Animal] < animalOrder[other.Animal] ? -1 : 1;

		}
		#endregion

		#region Test
		public static void Test()
		{
			List<MyComparable> l = new List<MyComparable> {
				new MyComparable(1, DONKEY),
				new MyComparable(3, DONKEY),
				new MyComparable(2, CAT),
				new MyComparable(3, CAT),
				new MyComparable(2, DOG),
			};

			l.Sort();
		}
		#endregion
	}
}
