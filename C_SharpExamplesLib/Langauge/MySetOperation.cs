using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp
{
	public class MySetOperation
	{
		public static void Test()
		{
			IEnumerable<int> setA = Enumerable.Range(1,6); // {1,2,3,4,5,6}
			IEnumerable<int> setB = Enumerable.Range(4,6); //       {4,5,6,7,8,9}


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
