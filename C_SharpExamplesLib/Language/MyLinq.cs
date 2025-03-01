﻿using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable ConvertToLocalFunction

namespace C_SharpExamplesLib.Language
{
    [SuppressMessage("ReSharper", "ConvertClosureToMethodGroup")]
    public abstract class MyLinq
	{
		// #linq #range #where #take #all
		public static void List_Range_Where_Take()
		{

			List<int> favorites = [0, 7, 14, 21, 28, 35, 42, 49];
			List<int> all = Enumerable.Range(0, 100).ToList();
			List<int> favoritesFirst = favorites;
			favoritesFirst.AddRange(all.Where(i => !favorites.Contains(i))); // {0, 7, ... 49, 1, 2, ... }

			List<int> allowed = [1, 8, 7, 50, 42, 13, 85, 2, 14, 67];

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
			int[] numbers = [1, 2, 3, 4];
			string[] words = ["one", "two", "three"];

			var numbersAndWords = numbers.Zip(words, (first, second) => first + " " + second).ToList();

			Assert.IsTrue(numbersAndWords.First().Contains("one"));
			Assert.IsTrue(numbersAndWords.First().Contains("1"));

		}

		// #linq #orderBy #no comparer #key selector 
		public static void Linq_OrderBy()
		{
			List<Tuple<int, string>> numbers =
			[
				new(2, "Dog"),
				new(3, "Cat"),
				new(1, "Donkey"),
				new(4, "Seagull")
			];

			List<Tuple<int, string>> sortedNumbers = numbers.OrderBy(tuple => tuple.Item1).ToList();

			Assert.IsTrue(sortedNumbers.First().Item1 == 1);
		}

		// #linq #FirstOrDefault
		public static void Linq_FirstOrDefault()
		{
			// #FirstOrDefault for value type : defaults to 0
			int i = 42;
			Assert.AreEqual(42, i);
			List<int> l = [];
			Assert.IsNotNull(l);

			i = l.FirstOrDefault();
			Assert.AreEqual(0, i);

			// #First
			int j = 43;

			Action first = () => j = l.First();
            Assert.ThrowsException<InvalidOperationException>(first);
			Assert.AreEqual(43, j);

			// #FirstOrDefault for non value types : defaults to null
			string? s = null;
			Assert.IsNull(s);
			List<string> ls = [];
			Assert.IsNotNull(ls);

			s = ls.FirstOrDefault();
			Assert.AreEqual(null, s);

			ls.Add("donkey");
			s =ls.FirstOrDefault( s1 => (s1 == "donkey") );
			Assert.IsNotNull(s);

			s = ls.FirstOrDefault(s1 => (s1 == "dog"));
			Assert.IsNull(s);
		}

        /// #linq #all but last #pairs #linkedList
        public static void Linq_LinkedList()
        {
            List<int> list = Enumerable.Range(1, 6).ToList();

			LinkedList<int> linkedList = new LinkedList<int>(list);

            var pairs = linkedList.Select(e => new Tuple<int, int?>(e, linkedList.Find(e)?.Next?.Value)).ToList();

			Assert.IsTrue( pairs.All( t => (t.Item2 == null) || t.Item1 < t.Item2 ));
        }


		// #linq #set operation #union #intersect #except
		public static void Linq_SetOperation()
		{
			List<int> setA = Enumerable.Range(1, 6).ToList(); // {1,2,3,4,5,6}
			List<int> setB = Enumerable.Range(4, 6).ToList(); //       {4,5,6,7,8,9}


			IEnumerable<int> unionSet = setA.Union(setB);
			Assert.IsTrue(unionSet.SequenceEqual(Enumerable.Range(1, 9))); // {1,2,3,4,5,6,7,8,9}

			IEnumerable<int> intersectionSet1 = setA.Intersect(setB);
			Assert.IsTrue(intersectionSet1.SequenceEqual(Enumerable.Range(4, 3))); // {4,5,6}

			IEnumerable<int> intersectionSet2 = setB.Intersect(setA);
			Assert.IsTrue(intersectionSet2.SequenceEqual(Enumerable.Range(4, 3))); // {4,5,6}

			IEnumerable<int> setDifference1 = setA.Except(setB);
			Assert.IsTrue(setDifference1.SequenceEqual(Enumerable.Range(1, 3))); // {1,2,3}

			IEnumerable<int> setDifference2 = setB.Except(setA);
			Assert.IsTrue(setDifference2.SequenceEqual(Enumerable.Range(7, 3))); // {7,8,9}
		}

		// #linq #selectMany
		public static void Linq_SelectMany()
        {
			List<int> setA = Enumerable.Range(1, 3).ToList();

			List<List<int>> result1 = setA.Select(i => new List<int> { i, i * i }).ToList();
			Assert.IsTrue(result1[0].SequenceEqual(new List<int> { 1, 1 }));
			Assert.IsTrue(result1[1].SequenceEqual(new List<int> { 2, 4 }));
			Assert.IsTrue(result1[2].SequenceEqual(new List<int> { 3, 9 }));

			List<int> result2 = setA.SelectMany(i => new List<int> { i, i * i }).ToList();
			Assert.IsTrue(result2.SequenceEqual(new List<int> { 1, 1, 2, 4, 3, 9 })); 
		}

        // #linq #single
        public static void Linq_Single()
        {
            IEnumerable<int> oneElementSet = Enumerable.Range(1, 1);
			Assert.IsTrue(oneElementSet.Single() == 1);

            IEnumerable<int> twoElementSet = Enumerable.Range(1, 2);
			Assert.ThrowsException<InvalidOperationException>( () => { _ = twoElementSet.Single(); });
        }

        public static void Linq_Select_Index()
        {
            List<string> l = ["Donkey", "Dog", "Goat"];
            var result = l.Select((s, i) => new {s, i}).ToList();
            Assert.IsTrue(result[0].s == "Donkey" && result[0].i == 0);
            Assert.IsTrue(result[1].s == "Dog" && result[1].i == 1);
            Assert.IsTrue(result[2].s == "Goat" && result[2].i == 2);
        }


        #region PLINQ
        private static bool IsPrime(int i)
		{
			if (i <= 3)
				return true;
			var divisors = Enumerable.Range(2, (i / 2) - 1).ToList();
			return !divisors.Any(d => (i % d == 0));
		}

		// #linq #parallel  #pLinq #WithDegreeOfParallelism #rime #as parallel
		public static void TestParallelLinq()
		{
			var numbers = Enumerable.Range(10000000, 500).ToList();

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

			numbers = ParallelEnumerable.Range(10000000, 500).ToList();
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

		private static bool CheckCity(string name)
		{
			if (name == "")
				throw new ArgumentException(name);
			return name == "Seattle";
		}

		class Person
		{
			public string Name { get; set; }
			public string City { get; set; }

			internal Person() 
			{
				Name = "";
				City = "";
			}
		}

		public static void PLinqExceptions()
		{
			Person johnDoe = new Person();
			Assert.AreEqual("", johnDoe.City);
			Assert.AreEqual("",johnDoe.City);
			johnDoe.City = "Seattle";
			johnDoe.Name = "John Doe";
			Assert.AreEqual("Seattle",johnDoe.City);
			Assert.AreEqual("John Doe", johnDoe.Name);
			
			Person[] people =
			[
				new() { Name = "Alan", City = "Hull" },
				new() { Name = "Beryl", City = "Seattle" },
				new() { Name = "Charles", City = "London" },
				new() { Name = "David", City = "Seattle" },
				new() { Name = "Eddy", City = "" },
				new() { Name = "Fred", City = "" },
				new() { Name = "Gordon", City = "Hull" },
				new() { Name = "Henry", City = "Seattle" },
				new() { Name = "Isaac", City = "Seattle" },
				new() { Name = "James", City = "London" }
			];

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
			Person[] people =
			[
				new() { Name = "Alan", City = "Hull" },
				new() { Name = "Beryl", City = "Seattle" },
				new() { Name = "Charles", City = "London" },
				new() { Name = "David", City = "Seattle" },
				new() { Name = "Eddy", City = "" },
				new() { Name = "Fred", City = "" },
				new() { Name = "Gordon", City = "Hull" },
				new() { Name = "Henry", City = "Seattle" },
				new() { Name = "Isaac", City = "Seattle" },
				new() { Name = "James", City = "London" }
			];

			people.Select(p => new { p.City, p.Name })
				.Where(p2 => (p2.City != ""))
				.ToList().ForEach(p => { Console.WriteLine(p.ToString()); });
		}


        // #linq #index #group by
		public static void LinqChunking()
		{
			List<int> testList = [1, 2, 3, 4, 5];

			var e1 = testList
				.Select((value, index) => new { Value = value, Index = index / 2 })
				.ToList();

			Assert.IsTrue(e1.Count>0);
			
			var e2 = testList
				.Select((value, index) => new { Value = value, Index = index / 2 })
				.GroupBy(x => x.Index, y => y.Value)
				.ToList();

            Assert.IsTrue(e2.Count > 0);

            var e3 = testList
				.Select((value, index) => new { Value = value, Index = index / 2 })
                .GroupBy(x => x.Index, y => y.Value)
                .Select(g => g.ToList())
                .ToList();
			
			Assert.AreEqual(e3.Count, 3);
            Assert.IsTrue(e3[0].Contains(1));
            Assert.IsTrue(e3[0].Contains(2));
            Assert.IsTrue(e3[1].Contains(3));
            Assert.IsTrue(e3[1].Contains(4));
            Assert.IsTrue(e3[2].Contains(5));
        }
	}
}
