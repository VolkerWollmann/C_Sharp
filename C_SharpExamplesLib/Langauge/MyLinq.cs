using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace C_Sharp
{
	public class MyLinq
	{
		// #linq #range
		public static void ListTests()
		{
			List<int> favorites = new List<int> { 0, 7, 14, 21, 28, 35, 42, 49 };
			List<int> all = Enumerable.Range(0, 100).ToList();
			List<int> favoritesFirst = favorites;
			favoritesFirst.AddRange(all.Where(i => !favorites.Contains(i)));

			List<int> allowed = new List<int> { 1, 8, 7, 50, 42, 13, 85, 2, 14, 67 };

			List<int> result = favoritesFirst.Where(i => allowed.Contains(i)).ToList();

			var takeTest = allowed.Take(20).ToList();
		}


		public static void Test()
		{
			ListTests();
		}

		// #linq #set operation
		public static void TestSetOperation()
		{
			IEnumerable<int> setA = Enumerable.Range(1, 6); // {1,2,3,4,5,6}
			IEnumerable<int> setB = Enumerable.Range(4, 6); //       {4,5,6,7,8,9}


			IEnumerable<int> unionSet = setA.Union(setB);
			Assert.IsTrue(unionSet.SequenceEqual<int>(Enumerable.Range(1, 9))); // {1,2,3,4,5,6,7,8,9}

			IEnumerable<int> interserctionSet1 = setA.Intersect(setB);
			Assert.IsTrue(interserctionSet1.SequenceEqual<int>(Enumerable.Range(4, 3))); // {4,5,6}

			IEnumerable<int> interserctionSet2 = setB.Intersect(setA);
			Assert.IsTrue(interserctionSet2.SequenceEqual<int>(Enumerable.Range(4, 3))); // {4,5,6}

			IEnumerable<int> setDifference1 = setA.Except(setB);
			Assert.IsTrue(setDifference1.SequenceEqual<int>(Enumerable.Range(1, 3))); // {1,2,3}

			IEnumerable<int> setDifference2 = setB.Except(setA);
			Assert.IsTrue(setDifference2.SequenceEqual<int>(Enumerable.Range(7, 3))); // {7,8,9}
		}
	}
}
