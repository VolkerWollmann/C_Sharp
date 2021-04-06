using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
	// #comparer #IEqualityComparer
	public class MyIEqualityComparer : IEqualityComparer<MyIEqualityComparer>
	{
		public int X { get; }
		public int Y { get; }
		bool IEqualityComparer<MyIEqualityComparer>.Equals(MyIEqualityComparer x, MyIEqualityComparer y)
		{
			return y != null && x != null && (x.X == y.X) && (y.X == y.Y);
		}

		int IEqualityComparer<MyIEqualityComparer>.GetHashCode(MyIEqualityComparer obj)
		{
			return (obj.X | obj.Y);
		}

		public MyIEqualityComparer(int x, int y)
		{
			X = x;
			Y = y;
		}

		public static void Test()
		{
			MyIEqualityComparer myA = new MyIEqualityComparer(1, 1);
			MyIEqualityComparer myB = new MyIEqualityComparer(1, 2);
			MyIEqualityComparer myC = new MyIEqualityComparer(1, 1);

			MyIEqualityComparer myIEqualityComparer = new MyIEqualityComparer(0, 0);
            Dictionary<MyIEqualityComparer, string> dictionary =
                new Dictionary<MyIEqualityComparer, string>(myIEqualityComparer) {{myA, "Test1"}, {myB, "Test2"}};

            Assert.IsTrue(dictionary.ContainsKey(myC));
		}
	}

	// #comparer #IComparable #order #DebuggerDisplay
	[DebuggerDisplay("Version={Version}, Animal={Animal}")]
	public class MyIComparable : IComparable<MyIComparable>
	{
		private const string NONE = "None";
		private const string DONKEY = "Esel";
		private const string DOG = "Hund";
		private const string SEAGULL = "Möwe";
		private const string CAT = "Katze";

		public int Version { get; private set; }
		public string Animal { get; private set; }

		#region Constructor 
		internal MyIComparable(int version, string animal)
		{
			Animal = animal;
			Version = version;

		}
		#endregion

		#region IComparable<MyOrderComparer>
		// <summary>
		// Sort order is by Version, than by Animal
		// Except NONE, which has lowest priority.
		// Important: There must be only one NONE to make order total.
		// <returns>
		// < 0 This instance precedes obj in the sort order.
		// = 0 This instance occurs in the same position in the sort order as obj.
		// > 0 This instance follows obj in the sort order.
        // </returns>
        // </summary>
		int IComparable<MyIComparable>.CompareTo(MyIComparable other)
		{
			if (( this.Animal == NONE ) && ( other.Animal == NONE))
				return 0;

			if (this.Animal == NONE)
				return -1;

			if (other.Animal == NONE)
				return 1;

			if (this.Version < other.Version)
				return -1;

			if (this.Version > other.Version)
				return 1;

			Dictionary<string, int> animalOrder = new Dictionary<string, int> { [DONKEY] = 1, [DOG] = 2, [SEAGULL] = 3, [CAT] = 4 };

			return animalOrder[Animal] < animalOrder[other.Animal] ? -1 : 1;

		}
		#endregion

		#region Test
		public static void TestIComparable()
		{
			List<MyIComparable> l = new List<MyIComparable> {
				new MyIComparable(1, DONKEY),
				new MyIComparable(3, DONKEY),
				new MyIComparable(2, CAT),
				new MyIComparable(3, CAT),
				new MyIComparable(2, DOG),
				new MyIComparable(5, NONE),
			};

			l.Sort();

			for (int i = 0; i < l.Count - 1; i++)
				Assert.IsTrue(((IComparable<MyIComparable>)l[i]).CompareTo(l[i + 1]) <= 0);

		}

		public static void TestComparison()
		{ 
			MyIComparable[] a = new MyIComparable[] { 
				new MyIComparable(1, DONKEY),
				new MyIComparable(5, DONKEY),
				new MyIComparable(2, CAT),
				new MyIComparable(4, CAT),
				new MyIComparable(3, DOG),
			};

			// Sort array with #Comparison
            Comparison<MyIComparable> comparison = (i1, i2) => -i2.Version.CompareTo(i1.Version);
			Array.Sort(a, comparison);

			for (int i = 0; i < a.Length-1; i++)
				Assert.IsTrue( a[i].Version <= a[i+1].Version );
		}
		#endregion
	}


	// #comparer #IEquatable #override #==
	[DebuggerDisplay("Number={Number}, Animal={Animal}")]
	public class MyIEquatable : IEquatable<MyIEquatable>
    {
		public int Number { private set; get; }
		public string Animal { private set; get; }


		public override bool Equals(object other)
        {
			return this.Equals(other as MyIEquatable); 
        }

		public override int GetHashCode()
		{
			return Number;
		}

		#region IEquatable
		//bool IEquatable<MyEquatable>.Equals(MyEquatable other)
        public bool Equals(MyIEquatable other)
		{
			if (Object.ReferenceEquals(other, null))
				return false;

			return  this.Number == other.Number && this.Animal == other.Animal;
		}
		#endregion

		#region override Comparsion Operators
		public static bool operator ==(MyIEquatable obj1, MyIEquatable obj2) => obj1.Equals(obj2);

        public static bool operator !=(MyIEquatable obj1, MyIEquatable obj2) => !obj1.Equals(obj2);

        #endregion

        #region Constructor
        public MyIEquatable(int number, string animal)
        {
			Number = number;
			Animal = animal;
        }
		#endregion

		#region Test
		public static void Test()
        {
			MyIEquatable me1 = new MyIEquatable(1, "Donkey");
			MyIEquatable me2 = new MyIEquatable(2, "Donkey");
			MyIEquatable me3 = new MyIEquatable(1, "Donkey");

            // ReSharper disable once UnusedVariable
            MyIComparable me4 = new MyIComparable(1, "Donkey");

			Assert.IsTrue(me1 == me3);
			Assert.IsTrue(me1 != me2);

			//Does not compile, because of type check during compilation
			//Assert.IsTrue(me1 != me4);
		}

        #endregion
    }
}
