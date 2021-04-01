using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace C_Sharp
{
    // #array #all elements
	public class MyArray
	{

		public static void Test()
		{
			// does not work for two dimensional array
			int[] xxx = new int[5];
			var y = xxx.AsQueryable().First();

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
			var firstElement = enumerator.Current;

			// get the second column
			var secondColumn =
				Enumerable.Range(0, testArray.GetLength(0))
				.Select(x => testArray[x, 2])
				.ToArray();

			// get a #list of all #Elements of #array
			var allElements = testArray.Cast<string>().ToList();
		}

		// #array #extension
		public static void ArrayExtension()
        {
			string[] friends = new string[] { "Donkey", "Dog" };

			//friends[2] = "Seagull";

			var moreFriends = friends.ToList<string>();
			moreFriends.Add("Seagull");
			friends = moreFriends.ToArray<string>();

			Assert.AreEqual(3, moreFriends.Count());
		}
	}
}
