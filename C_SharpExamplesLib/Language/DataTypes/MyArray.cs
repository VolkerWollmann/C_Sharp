using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
    // #array #all elements
	public class MyArray
	{

		public static void Test()
		{
			// does not work for two dimensional array
			int[] xxx = new int[5];
			var y = xxx.AsQueryable().First();
			Assert.AreEqual(y,0);

			string[,] testArray = new string[10, 10];
			for (int row = 0; row < 10; row++)
			{
				for (int column = 0; column < 10; column++)
				{
					testArray[row, column] = "row:" + row.ToString() + " column:" + column.ToString();
				}
			}

			// get an enumerator for all elements
			var enumerator = testArray.GetEnumerator();
			enumerator.MoveNext();

			// get first element in array
			var firstElement = enumerator.Current;
			Assert.IsInstanceOfType(firstElement, typeof(string));

			// get the second column
			var secondColumn =
				Enumerable.Range(0, testArray.GetLength(0))
				.Select(x => testArray[x, 2])
				.ToArray();
            Assert.IsInstanceOfType(secondColumn, typeof(string[]));

			// get a #list of all #Elements of #array
			var allElements = testArray.Cast<string>().ToList();
			Assert.IsInstanceOfType(allElements, typeof(List<string>) );
		}

		// #array #extension
		public static void ArrayExtension()
        {
			string[] friends = new string[] { "Donkey", "Dog" };

			//friends[2] = "Seagull";

			var moreFriends = friends.ToList();
			moreFriends.Add("Seagull");
            Assert.AreEqual(3, moreFriends.Count);

			friends = moreFriends.ToArray<string>();
			Assert.IsTrue(friends.Contains("Seagull"));
			
		}
	}
}
