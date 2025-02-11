using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language
{
	// #comparer #IEqualityComparer
	public class MyIEqualityComparer : IEqualityComparer<MyIEqualityComparer>
	{
		public int X { get; }
		public int Y { get; }

        #region IEqualityComparer<T>
        bool IEqualityComparer<MyIEqualityComparer>.Equals(MyIEqualityComparer? x, MyIEqualityComparer? y)
		{
			return y != null && x != null && (x.X == y.X) && (x.Y == y.Y);
		}

		int IEqualityComparer<MyIEqualityComparer>.GetHashCode(MyIEqualityComparer obj)
		{
			return (obj.X | obj.Y);
		}
        #endregion

        public MyIEqualityComparer(int x, int y)
		{
			X = x;
			Y = y;
		}

		public static void Test()
		{
			MyIEqualityComparer myA = new MyIEqualityComparer(1, 1);
			MyIEqualityComparer myB = new MyIEqualityComparer(1, 2);
			MyIEqualityComparer myC = new MyIEqualityComparer(1, 2);

            // Use reference comparer
            Dictionary<MyIEqualityComparer, string> dictionary1 =
                new Dictionary<MyIEqualityComparer, string> { { myA, "Test1" }, { myB, "Test2" } };

			// Fails, since myC != myA and myC != myB due to reference comparer
            Assert.IsFalse(dictionary1.ContainsKey(myC));

            // Create a comparer
            MyIEqualityComparer myIEqualityComparer = new MyIEqualityComparer(0, 0);

            //Use comparer for the dictionary
            Dictionary<MyIEqualityComparer, string> dictionary2 =
                new Dictionary<MyIEqualityComparer, string>(myIEqualityComparer){{myA, "Test1"}, {myB, "Test2"}};

            // True, since myC == myB due to my MyIEqualityComparer
            Assert.IsTrue(dictionary2.ContainsKey(myC));
		}
	}

	// #comparer #IComparable #order #DebuggerDisplay
	[DebuggerDisplay("Version={Version}, Animal={Animal}")]
	public class MyIComparable : IComparable<MyIComparable>, IComparer<MyIComparable>
	{
		private const string None = "None";
		private const string Donkey = "Esel";
		private const string Dog = "Hund";
		private const string Seagull = "Möwe";
		private const string Cat = "Katze";

		public int Version { get; private set; }
		public string Animal { get; private set; }

		#region Constructor 
		internal MyIComparable(int version, string animal)
		{
			Animal = animal;
			Version = version;

		}
		#endregion

        
		private int CompareInternal(MyIComparable? other)
        {
            if (other == null)
                return 1;

            if ((Animal == None) && (other.Animal == None))
                return 0;

            if (Animal == None)
                return -1;

            if (other.Animal == None)
                return 1;

            if (Version < other.Version)
                return -1;

            if (Version > other.Version)
                return 1;

            Dictionary<string, int> animalOrder = new Dictionary<string, int> { [Donkey] = 1, [Dog] = 2, [Seagull] = 3, [Cat] = 4 };

            if (animalOrder[Animal] < animalOrder[other.Animal])
                return -1;

            if (animalOrder[Animal] > animalOrder[other.Animal])
                return 1;

            return 0;

        }

        #region IComparable<MyIComparable>
        // <summary>
        // Sort order is by Version, then by Animal
        // Except NONE, which has the lowest priority.
        // Important: There must be only one NONE to make order total.
        //
        // ![CDATA[
        // This CompareTo() <= 0 is a pre order:
        // - reflexivity  : (1,Dog) <= (1,Dog)
        // - transitivity : (1,Cat) <= (1,Dog) & (1,Dog) <= (1,Donkey) => (1,Cat) <= (1,Donkey) 
        // ]]>
        /// <param name="other"></param>
        /// <returns></returns>
        // <returns>
        // < 0 This instance precedes obj in the sort order.
        // = 0 This instance occurs in the same position in the sort order as obj.
        // > 0 This instance follows obj in the sort order.
        // </returns>
        // </summary>

        // #IComparable
        int IComparable<MyIComparable>.CompareTo(MyIComparable? other)
        {
            return CompareInternal(other);
        }


		#endregion

		#region IComparer
		// #IComparer
        int IComparer<MyIComparable>.Compare(MyIComparable? x, MyIComparable? y)
        {
            if (x == null && y == null) return 0;       // Consider nulls as equal
            if (x == null) return -1;                   // Null is considered smaller
            if (y == null) return 1;

            return x.CompareInternal(y);
        }
		#endregion

		#region Tests

        public static void TestIComparable()
        {
            List<MyIComparable> l = new List<MyIComparable>
            {
                new MyIComparable(1, Donkey),
                new MyIComparable(3, Donkey),
                new MyIComparable(2, Cat),
                new MyIComparable(3, Cat),
                new MyIComparable(2, Dog),
                new MyIComparable(5, None),
            };

            l.Sort();

            for (int i = 0; i < l.Count - 1; i++)
                Assert.IsTrue(((IComparable<MyIComparable>)l[i]).CompareTo(l[i + 1]) <= 0);

            // #pre-order works for one NONE
            List<MyIComparable> l2 = new List<MyIComparable>
            {
                new MyIComparable(2, Cat),
                new MyIComparable(1, Donkey),
                new MyIComparable(3, Donkey),
                new MyIComparable(2, Cat),
                new MyIComparable(3, Cat),
                new MyIComparable(3, Donkey),
                new MyIComparable(2, Dog),
                new MyIComparable(5, None),
            };

            l2.Sort();
            for (int i = 0; i < l2.Count - 1; i++)
                Assert.IsTrue(((IComparable<MyIComparable>)l2[i]).CompareTo(l2[i + 1]) <= 0);


            // #pre-order works for three NONE
            List<MyIComparable> l3 = new List<MyIComparable>
            {
                new MyIComparable(2, Cat),
                new MyIComparable(1, Donkey),
                new MyIComparable(3, Donkey),
                new MyIComparable(3, None),
                new MyIComparable(5, None),
                new MyIComparable(2, Cat),
                new MyIComparable(3, Cat),
                new MyIComparable(3, Donkey),
                new MyIComparable(2, Dog),
                new MyIComparable(5, None),
            };

            l3.Sort();
            for (int i = 0; i < l3.Count - 1; i++)
                Assert.IsTrue(((IComparable<MyIComparable>)l3[i]).CompareTo(l3[i + 1]) <= 0);

        }

        public static void TestIComparer()
        {
            List<MyIComparable> l = new List<MyIComparable>
            {
                new MyIComparable(1, Donkey),
                new MyIComparable(3, Donkey),
                new MyIComparable(2, Cat),
                new MyIComparable(3, Cat),
                new MyIComparable(2, Dog),
                new MyIComparable(5, None),
            };

            // #IComparer
			IComparer<MyIComparable>comparer = new MyIComparable(0, None);
			l.Sort(comparer);

            for (int i = 0; i < l.Count - 1; i++)
                Assert.IsTrue(((IComparable<MyIComparable>)l[i]).CompareTo(l[i + 1]) <= 0);
		}

		public static void TestComparison()
		{ 
			MyIComparable[] a =
			[
				new(1, Donkey),
				new(5, Donkey),
				new(2, Cat),
				new(4, Cat),
				new(3, Dog)
			];

			// Sort array with #Comparison
            // ReSharper disable once ConvertToLocalFunction
            Comparison<MyIComparable> comparison = (i1, i2) => -i2.Version.CompareTo(i1.Version);
			Array.Sort(a, comparison);

			for (int i = 0; i < a.Length-1; i++)
				Assert.IsTrue( a[i].Version <= a[i+1].Version );
		}

        #endregion
    }


	// #comparer #IEquatable #override #== #<, #>, #!=
	[DebuggerDisplay("Number={Number}, Animal={Animal}")]
	public class MyIEquatable : IEquatable<MyIEquatable>
    {
		public int Number { get; }
		public string Animal { private set; get; }


		public override bool Equals(object? other)
        {
			return Equals(other as MyIEquatable); 
        }

		public override int GetHashCode() => Number;

        #region IEquatable
		//bool IEquatable<MyEquatable>.Equals(MyEquatable other)
        public bool Equals(MyIEquatable? other)
		{
			// #Object #ReferenceEquals 
			if (ReferenceEquals(other, null))
				return false;

			return  Number == other.Number && Animal == other.Animal;
		}
		#endregion

		#region override Comparsion Operators
        // #overload #operator #less #< #== #!=
		public static bool operator ==(MyIEquatable obj1, MyIEquatable obj2) =>  obj1.Equals(obj2);

        public static bool operator !=(MyIEquatable obj1, MyIEquatable obj2) =>  !obj1.Equals(obj2);

        public static bool operator <(MyIEquatable obj1, MyIEquatable obj2) => (obj1.Number < obj2.Number);

        public static bool operator >(MyIEquatable obj1, MyIEquatable obj2) => (obj1.Number > obj2.Number);

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
			Assert.IsTrue(me1 < me2);

			//Does not compile, because of type check during compilation
			//Assert.IsTrue(me1 != me4);
		}

		public static void PartialOrderTest()
        {
            List<int> l = new List<int>() {3, 4, 8, 2, 1, 2, 4, 5, 2, 7,};
            l.Sort();
            Assert.IsTrue(l.SequenceEqual(new List<int>()
            {
                1, 2, 2, 2, 3, 4, 4, 5, 7, 8
            }));
        }

        #endregion
    }
}
