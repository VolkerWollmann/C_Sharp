using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language.DataTypes
{
    // Data types : #List #IComparable
    public class MyPair(int x, int y) : IComparable<MyPair>
    {
		
		public int X = x;
		public int Y = y;


		// Needed by MySortedListTest
        // < 0 This instance precedes obj in the sort order.
        // = 0 This instance occurs in the same position in the sort order as obj.
        // > 0 This instance follows obj in the sort order.
        public int CompareTo(MyPair? other)
        {
            if (other == null)
                return 1;

            if (X > other.X)
                return 1;

            if (X < other.X)
                return -1;

            if (Y > other.Y)
                return 1;

            if (Y < other.Y)
                return -1;

            return 0;
        }
    }
	public class MyList(int capacity) : List<MyPair>(capacity);
	public class MyListTest
	{
		public static MyPair F(MyList li, int index, int offset)
		{
			return li[index-offset];
		}

		public static void Test()
		{

			MyList li = new MyList(200);

            MyPair r1 = new MyPair(1, 42);
			li.Add(r1);

            MyPair r2 = F(li,1,1);
            Assert.AreEqual(r1, r2);
        }
	}

    // Data types : #SortedList
	public class MySortedListTest
    {
		public static void Test()
        {
            SortedList<MyPair, MyPair> sortedList = new SortedList<MyPair, MyPair>();
            List<MyPair> data =
			[
				new(4,7),
                new(3,8),
                new(3,5),
                new(4,1),
                new(2,2),
                new(5,5),
            ];

            foreach (MyPair myPair in data)
            {
                sortedList.Add(myPair,myPair);
                string s = "";
                int j = 0;
                foreach (var elem in sortedList)
                {
                    s = s + j + ".(" + elem.Value.X + "," + elem.Value.Y + ") ";
                    j++;
                }
                Console.WriteLine(s);
            }
        }
    }
}
