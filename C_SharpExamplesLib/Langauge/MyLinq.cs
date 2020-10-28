using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace C_Sharp
{
	public class MyLinq
	{
		// #linq #range #where #take
		public static void ListTests()
		{
			
			List<int> favorites = new List<int> { 0, 7, 14, 21, 28, 35, 42, 49 };
			List<int> all = Enumerable.Range(0, 100).ToList();
			List<int> favoritesFirst = favorites;
			favoritesFirst.AddRange(all.Where(i => !favorites.Contains(i))); // {0, 7, ... 49, 1, 2, ... }

			List<int> allowed = new List<int> { 1, 8, 7, 50, 42, 13, 85, 2, 14, 67 };

			List<int> result = favoritesFirst.Where(i => allowed.Contains(i)).ToList(); // { 7, 14, ... 42, 1, ... }
			
			var takeTest = allowed.Take(20).ToList(); // take takes up to maxium 20
		}


		// #linq 
		public static void LinqTest()
		{
			// #Zip
			int[] numbers = { 1, 2, 3, 4 };
			string[] words = { "one", "two", "three" };

			var numbersAndWords = numbers.Zip(words, (first, second) => first + " " + second);

			Assert.IsTrue(numbersAndWords.First().Contains("one"));
			Assert.IsTrue(numbersAndWords.First().Contains("1"));

			// #FirstOrDefault
			int i = 42;
			List<int> l = new List<int> { };

			i =  l.FirstOrDefault<int>();
			Assert.AreEqual(i, 0);

			// #First
			int j = 43;
			try { j = l.First<int>(); }
			catch (InvalidOperationException) {}
			finally { Assert.AreEqual(j, 43); }

		}

		public static void Test()
		{
			ListTests();
			LinqTest();
		}

		// #linq #set operation #union #intersect #except
		public static void TestSetOperation()
		{
			IEnumerable<int> setA = Enumerable.Range(1, 6); // {1,2,3,4,5,6}
			IEnumerable<int> setB = Enumerable.Range(4, 6); //       {4,5,6,7,8,9}


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

		private static bool IsPrime(int i)
		{
			if (i <= 3)
				return true;
			var divisors = Enumerable.Range(2, (i / 2) - 1 ).ToList();
			return !divisors.Any(d => (i % d == 0));
		}

		// #linq #parallel
		public static void TestLinqParallel()
        {
			var numbers = Enumerable.Range(10000000, 500);

			DateTime tStart = DateTime.Now;
			Console.WriteLine(tStart.ToString());
			var primes = numbers.Where(n => IsPrime(n)).ToList();
			DateTime t2 = DateTime.Now;
			Console.WriteLine("Time sequential:" + t2.Subtract(tStart).ToString());
			var primes2 = numbers.AsParallel().Where(n => IsPrime(n)).ToList();
			DateTime t3 = DateTime.Now;
			Console.WriteLine("Time parallel:" + t3.Subtract(t2).ToString());
		}
	}
}
