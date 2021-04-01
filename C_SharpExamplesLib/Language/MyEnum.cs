using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Langauge
{
	// #enum #number of members
	public class MyEnum
	{
		public enum Dog { Lessie, KomissarRex, Cerberus};

		public enum Cat { Duchesse, OMailey, Garfield};

		private static void ShowAllElement<TSpecies>( Array array )
		{
			foreach(TSpecies species in array)
			{
				Console.WriteLine(species);
			}
		}

		private static void ShowAllElement2<TSpecies>(TSpecies[] array)
		{
			foreach (TSpecies species in array)
			{
				Console.WriteLine(species);
			}
		}

		public static void Enum_Test()
		{
			// #enum number of elements
			int numberOfDogs = Enum.GetValues(typeof(Dog)).Length;

			// #enum to #array
			var allDogs = Enum.GetValues(typeof(Dog));
			ShowAllElement<Dog>(allDogs);

			// #enum to #iEnumerable
			var iEnumerableDogs = allDogs.Cast<Dog>();
			var allDogs2 = iEnumerableDogs.ToArray();
			ShowAllElement2<Dog>(allDogs2);

			// #enum max element
			Dog maxDog = Enum.GetValues(typeof(Dog)).Cast<Dog>().Max();
			Assert.AreEqual(maxDog, Dog.Cerberus);

			var allCats = Enum.GetValues(typeof(Cat));
			ShowAllElement<Dog>(allCats);

			var allCats2 = allCats.Cast<Cat>().ToArray();
			ShowAllElement2<Cat>(allCats2);
		}
	}
}
