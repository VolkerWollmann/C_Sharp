using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp
{
	public class MyLinq
	{
		
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
	}
}
