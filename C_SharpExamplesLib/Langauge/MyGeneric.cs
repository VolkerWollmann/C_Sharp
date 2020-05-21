using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp
{
	public class MyGeneric
	{
		private static Random random = new Random();
		private static T GetRandomElement<T>(List<T> list)
		{
			int index = random.Next(0, list.Count );
			return list[index];
		}

		private static List<T> ShuffledList<T>(List<T> list)
		{
			List<T> result = new List<T>();
			List<T> work = new List<T>();
			list.ForEach(e => work.Add(e));
			while( work.Count > 0 )
			{
				int index = random.Next(0, work.Count);
				result.Add(work[index]);
				work.RemoveAt(index);
			}

			return result;
		}


		public static void Test()
		{
			List<string> animals = new List<string>{ "Esel", "Hund", "Möwe", "Katze", "Ziege" };
			var oneAnimal = GetRandomElement<string>(animals);
			var shuffledAnimals = ShuffledList<string>(animals);

			List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
			var number = GetRandomElement<int>(numbers);
			var shuffledNumbers = ShuffledList<int>(numbers);
		}
	}
}
