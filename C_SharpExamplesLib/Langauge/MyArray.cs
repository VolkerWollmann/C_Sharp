using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp
{
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

			// get an enumartor for all emelents
			var enumerator = testArray.GetEnumerator();
			enumerator.MoveNext();
			var firstElement = enumerator.Current;

			// get the second column
			var secondColumn =
				Enumerable.Range(0, testArray.GetLength(0))
				.Select(x => testArray[x, 2])
				.ToArray();

			// get a list of all Elements
			var allElements = testArray.Cast<string>().ToList();
		}
	}
}
