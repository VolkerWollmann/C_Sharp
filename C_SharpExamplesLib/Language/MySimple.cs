using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
	public class MySimpleCSharp
	{
		// #index operator, #indexer, #range
		private class IndexClass
		{
			public int[] Numbers = new int[10];

			public int this[int index]
			{
				get => Numbers[index];
                set => Numbers[index] = value;
            }
			internal IndexClass()
			{
				Random random = new Random();
				Enumerable.Range(0, 10).ToList().ForEach(i => { Numbers[i] = random.Next(1, 10); });
			}
		}

        //#call by reference
		static void Method(ref string s)
		{
			s = "donkey";
		}

		// #Enumerable #empty list 
		static void EnumerableTest()
		{
			IEnumerable<int> emptyIntegerList = Enumerable.Empty<int>();
		}

		public static void Test()
		{
			string s=null;
			Method(ref s);
			Assert.IsTrue(s == "donkey");

			IndexClass indexClass = new IndexClass();
			for(int i=0; i<10; i++)
			{
				Assert.AreEqual(indexClass[i], indexClass.Numbers[i]);
			}

			EnumerableTest();
		}

	}
}
