using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
	public class MyLinq
	{
		// #linq #range #where #take
		public static void List_Range_Where_Take()
		{

			List<int> favorites = new List<int> { 0, 7, 14, 21, 28, 35, 42, 49 };
			List<int> all = Enumerable.Range(0, 100).ToList();
			List<int> favoritesFirst = favorites;
			favoritesFirst.AddRange(all.Where(i => !favorites.Contains(i))); // {0, 7, ... 49, 1, 2, ... }

			List<int> allowed = new List<int> { 1, 8, 7, 50, 42, 13, 85, 2, 14, 67 };

			List<int> result = favoritesFirst.Where(i => allowed.Contains(i)).ToList(); // { 7, 14, ... 42, 1, ... }
			Assert.IsTrue(result.All( i => allowed.Contains(i)));

			var takeTest = allowed.Take(20).ToList(); // take takes up to maximum 20
			Assert.IsTrue(takeTest.Count == 10);
		}

		public static void Linq_Syntax()
		{
			List<int> numbers = Enumerable.Range(0, 11).ToList();

			// #Linq #Method_syntax
			IEnumerable<int> q1 = numbers.Where(i => (i < 9)).Where(i => (i % 2 == 0));

			// #Linq #Query_syntax
			IEnumerable<int> q2 = from m in (from n in numbers where (n < 9) select n) where (m % 2 == 0) select m;

			// #CollectionAssert #Assert
			CollectionAssert.AreEqual(q1.ToList(), q2.ToList());

		}

		// #linq #zip #firstOrDefault
		public static void Linq_Zip()
		{
			// #Zip
			int[] numbers = { 1, 2, 3, 4 };
			string[] words = { "one", "two", "three" };

			var numbersAndWords = numbers.Zip(words, (first, second) => first + " " + second);

			Assert.IsTrue(numbersAndWords.First().Contains("one"));
			Assert.IsTrue(numbersAndWords.First().Contains("1"));

		}

		// #linq #orderBy
		public static void Linq_OrderBy()
		{
			List<int> numbers = new List<int> { 2, 3, 1, 4 };

			List<int> sortedNumbers = numbers.OrderBy(i => i).ToList();

			Assert.IsTrue(sortedNumbers.First() == 1);
		}

		// #linq #FirstOrDefault
		public static void Linq_FirstOrDefault()
		{
			// #FirstOrDefault for value type
			int i = 42;
			Assert.AreEqual(i, 42);
			List<int> l = new List<int>();

			i = l.FirstOrDefault<int>();
			Assert.AreEqual(i, 0);

			// #First
			int j = 43;

			Action first = () => j = l.First();
            Assert.ThrowsException<InvalidOperationException>(first);
			Assert.AreEqual(j, 43);

			// #FirstOrDefault for non value types
			string s = null;
			List<string> ls = new List<string>();

			s = ls.FirstOrDefault();

			Assert.AreEqual(s, null);
		}

		// #linq #set operation #union #intersect #except
		public static void Linq_SetOperation()
		{
			List<int> setA = Enumerable.Range(1, 6).ToList(); // {1,2,3,4,5,6}
			List<int> setB = Enumerable.Range(4, 6).ToList(); //       {4,5,6,7,8,9}


			IEnumerable<int> unionSet = setA.Union(setB);
			Assert.IsTrue(unionSet.SequenceEqual<int>(Enumerable.Range(1, 9))); // {1,2,3,4,5,6,7,8,9}

			IEnumerable<int> intersectionSet1 = setA.Intersect(setB);
			Assert.IsTrue(intersectionSet1.SequenceEqual<int>(Enumerable.Range(4, 3))); // {4,5,6}

			IEnumerable<int> intersectionSet2 = setB.Intersect(setA);
			Assert.IsTrue(intersectionSet2.SequenceEqual<int>(Enumerable.Range(4, 3))); // {4,5,6}

			IEnumerable<int> setDifference1 = setA.Except(setB);
			Assert.IsTrue(setDifference1.SequenceEqual<int>(Enumerable.Range(1, 3))); // {1,2,3}

			IEnumerable<int> setDifference2 = setB.Except(setA);
			Assert.IsTrue(setDifference2.SequenceEqual<int>(Enumerable.Range(7, 3))); // {7,8,9}
		}

		// #linq #selectMany
		public static void Linq_SelectMany()
        {
			IEnumerable<int> setA = Enumerable.Range(1, 3);

			List<List<int>> result1 = setA.Select(i => new List<int> { i, i * i }).ToList();
			Assert.IsTrue(result1[0].SequenceEqual<int>(new List<int> { 1, 1 }));
			Assert.IsTrue(result1[1].SequenceEqual<int>(new List<int> { 2, 4 }));
			Assert.IsTrue(result1[2].SequenceEqual<int>(new List<int> { 3, 9 }));

			List<int> result2 = setA.SelectMany(i => new List<int> { i, i * i }).ToList();
			Assert.IsTrue(result2.SequenceEqual<int>(new List<int> { 1, 1, 2, 4, 3, 9 })); 
		}

		#region PLINQ
		private static bool IsPrime(int i)
		{
			if (i <= 3)
				return true;
			var divisors = Enumerable.Range(2, (i / 2) - 1).ToList();
			return !divisors.Any(d => (i % d == 0));
		}

		// #linq #parallel  #pLinq #WithDegreeOfParallelism #rime
		public static void TestParallelLinq()
		{
			var numbers = Enumerable.Range(10000000, 500);

			DateTime t0 = DateTime.Now;
			Console.WriteLine(t0.ToString(CultureInfo.InvariantCulture));
			
            var primes = numbers.Where(n => IsPrime(n)).ToList();
			Assert.IsNotNull(primes);

			DateTime t1 = DateTime.Now;
			Console.WriteLine("Time sequential:" + t1.Subtract(t0).ToString());

			var primes2 = numbers.AsParallel().Where(n => IsPrime(n)).ToList();
			Assert.IsNotNull(primes2);

			DateTime t2 = DateTime.Now;
			Console.WriteLine("Time parallel:" + t2.Subtract(t1).ToString());

			numbers = ParallelEnumerable.Range(10000000, 500);
			var primes3 = numbers.AsParallel()
				.AsOrdered()
				.WithDegreeOfParallelism(3)
				.WithExecutionMode(ParallelExecutionMode.ForceParallelism)
				.Where(n => IsPrime(n))
				.AsSequential()
				.Take(500)
				.ToList();
			Assert.IsNotNull(primes3);

			DateTime t3 = DateTime.Now;
			Console.WriteLine("Time parallel (degree 3):" + t3.Subtract(t2).ToString());
		}
		#endregion

		#region PLinqException
		public static bool CheckCity(string name)
		{
			if (name == "")
				throw new ArgumentException(name);
			return name == "Seattle";
		}

		class Person
		{
			public string Name { get; set; }
			public string City { get; set; }
		}

		public static void PLinqExceptions()
		{
			Person[] people = new Person[] {
				new Person { Name = "Alan", City = "Hull" },
				new Person { Name = "Beryl", City = "Seattle" },
				new Person { Name = "Charles", City = "London" },
				new Person { Name = "David", City = "Seattle" },
				new Person { Name = "Eddy", City = "" },
				new Person { Name = "Fred", City = "" },
				new Person { Name = "Gordon", City = "Hull" },
				new Person { Name = "Henry", City = "Seattle" },
				new Person { Name = "Isaac", City = "Seattle" },
				new Person { Name = "James", City = "London" }};

			try
			{
				var result = from person in
					people.AsParallel()
							 where CheckCity(person.City)
							 select person;
				result.ForAll(person => Console.WriteLine(person.Name));
			}
			catch (AggregateException e)
			{
				Console.WriteLine(e.InnerExceptions.Count + " exceptions.");
			}
		}

		#endregion


		// #linq #select implicit type
		public static void SelectImplicitType()
		{
			Person[] people = new Person[] {
				new Person { Name = "Alan", City = "Hull" },
				new Person { Name = "Beryl", City = "Seattle" },
				new Person { Name = "Charles", City = "London" },
				new Person { Name = "David", City = "Seattle" },
				new Person { Name = "Eddy", City = "" },
				new Person { Name = "Fred", City = "" },
				new Person { Name = "Gordon", City = "Hull" },
				new Person { Name = "Henry", City = "Seattle" },
				new Person { Name = "Isaac", City = "Seattle" },
				new Person { Name = "James", City = "London" }};

			people.Select(p => new { p.City, p.Name })
				.Where(p2 => (p2.City != ""))
				.ToList().ForEach(p => { Console.WriteLine(p.ToString()); });
		}
	}
}
