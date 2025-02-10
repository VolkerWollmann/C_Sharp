using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
	// #enum #number of members
	public class MyEnum
	{
		public enum Dog { Lessie, KomissarRex, Cerberus};

		private enum Cat { Duchesse, OMailey, Garfield};

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
			Assert.AreEqual(Dog.Lessie, lessie);

            Cat cat1 = Cat.Duchesse;
            Assert.AreEqual(Cat.Duchesse, cat1);
			
			//Does not Compile
            //Assert.AreNotEqual(cat1, Dog.Lessie);
            
            Assert.AreEqual((Cat)Dog.Lessie, cat1);

			Cat cat2 = Cat.OMailey;
            Assert.AreEqual(Cat.OMailey, cat2);

            Cat cat3 = Cat.Garfield;
            Assert.AreEqual(Cat.Garfield, cat3);

			// #enum number of elements
			int numberOfDogs = Enum.GetValues(typeof(Dog)).Length;
			Assert.AreEqual(3, numberOfDogs);

			// #enum to #array, #enumeration of values of enum
			var allDogs = Enum.GetValues(typeof(Dog));
			ShowAllElement<Dog>(allDogs);

			// #enum to #iEnumerable
			var iEnumerableDogs = allDogs.Cast<Dog>();
			var allDogs2 = iEnumerableDogs.ToArray();
			ShowAllElement2(allDogs2);

			// #enum max element
			Dog maxDog = Enum.GetValues(typeof(Dog)).Cast<Dog>().Max();
			Assert.AreEqual(Dog.Cerberus, maxDog);

			var allCats = Enum.GetValues(typeof(Cat));
			ShowAllElement<Dog>(allCats);

			var allCats2 = allCats.Cast<Cat>().ToArray();
			ShowAllElement2(allCats2);

			// string to enum #enum #parse 
			var dogsFromString = Enum.Parse(typeof(Dog), "KomissarRex");
			Assert.AreEqual(Dog.KomissarRex, dogsFromString);
		}
	}
}
