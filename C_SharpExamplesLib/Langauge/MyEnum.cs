using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp
{
	// #enum #number of members
	public class MyEnum
	{
		public enum Dog { Lessie, KomissarRex, Cerberus};

		public enum Cat { Duchesse, OMailey, Garfield};

		private static void ShowAllElement<Spieces>( Array array )
		{
			foreach(Spieces spieces in array)
			{
				Console.WriteLine(spieces);
			}
		}

		private static void ShowAllElement2<Spieces>(Spieces[] array)
		{
			foreach (Spieces spieces in array)
			{
				Console.WriteLine(spieces);
			}
		}

		public static void Enum_Test()
		{
			int numberOfDogs = Enum.GetValues(typeof(Dog)).Length;

			var allDogs = Enum.GetValues(typeof(Dog));
			ShowAllElement<Dog>(allDogs);

			var iEnumerableDogs = allDogs.Cast<Dog>();
			var allDogs2 = iEnumerableDogs.ToArray();
			ShowAllElement2<Dog>(allDogs2);

			var allCats = Enum.GetValues(typeof(Cat));
			ShowAllElement<Dog>(allCats);

			var allCats2 = allCats.Cast<Cat>().ToArray();
			ShowAllElement2<Cat>(allCats2);
		}
	}
}
