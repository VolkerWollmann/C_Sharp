using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
	public struct MyPair
	{
		public int X;
		public int Y;
		public MyPair(int x, int y)
		{
			X = x;
			Y = y;
		}
	}
	public class MyList : List<MyPair>
	{
		public MyList(int capacity): base(capacity)
		{

		}
	}
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
}
