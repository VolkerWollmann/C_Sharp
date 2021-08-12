using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
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
            Dog lessie = Dog.Lessie;
			Assert.AreEqual(lessie, Dog.Lessie);

            Cat cat1 = Cat.Duchesse;
            Assert.AreEqual(cat1, Cat.Duchesse);
			Assert.AreNotEqual(cat1, Dog.Lessie);
            Assert.AreEqual(cat1, (Cat)Dog.Lessie);

			Cat cat2 = Cat.OMailey;
            Assert.AreEqual(cat2, Cat.OMailey);

            Cat cat3 = Cat.Garfield;
            Assert.AreEqual(cat3, Cat.Garfield);

			// #enum number of elements
			int numberOfDogs = Enum.GetValues(typeof(Dog)).Length;
			Assert.AreEqual(numberOfDogs,3);

			// #enum to #array
			var allDogs = Enum.GetValues(typeof(Dog));
			ShowAllElement<Dog>(allDogs);

			// #enum to #iEnumerable
			var iEnumerableDogs = allDogs.Cast<Dog>();
			var allDogs2 = iEnumerableDogs.ToArray();
			ShowAllElement2(allDogs2);

			// #enum max element
			Dog maxDog = Enum.GetValues(typeof(Dog)).Cast<Dog>().Max();
			Assert.AreEqual(maxDog, Dog.Cerberus);

			var allCats = Enum.GetValues(typeof(Cat));
			ShowAllElement<Dog>(allCats);

			var allCats2 = allCats.Cast<Cat>().ToArray();
			ShowAllElement2(allCats2);

			// string to enum
			var dogsFromString = Enum.Parse(typeof(Dog), "KomissarRex");
			Assert.AreEqual(dogsFromString, Dog.KomissarRex);
		}
	}
}
