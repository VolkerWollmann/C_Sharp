using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp
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
		public static void F(MyList li, int index, int offset)
		{
			var i = li[index-offset];
		}

		public static void Test()
		{

			MyList li = new MyList(200);
			li.Add(new MyPair(1,42));

			F(li,1,1);
		}
	}
}
