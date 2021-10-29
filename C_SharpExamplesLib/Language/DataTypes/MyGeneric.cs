using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language.DataTypes
{
	// #interface #generic #default
	internal interface IMyRandomizer<T>
	{
		T GetRandomElement(List<T> list);
		List<T> GetShuffledList(List<T> list);
	}

    internal interface IMyIntRandomizer : IMyRandomizer<int>
    {

    }

    // #generic
	internal class MyIntegerRandomizer : IMyIntRandomizer
	{
        readonly Random _Random = new Random();
		public int GetRandomElement(List<int> list)
		{
			int index = _Random.Next(0, list.Count);
			return list[index];
		}

		public List<int> GetShuffledList(List<int> list)
		{
			List<int> result = new List<int>();
			List<int> work = new List<int>();
			list.ForEach(e => work.Add(e));
			while (work.Count > 0)
			{
				int index = _Random.Next(0, work.Count);
				result.Add(work[index]);
				work.RemoveAt(index);
			}

			return result;
		}
	}

	internal class MyStringRandomizer : IMyRandomizer<string>
	{
        readonly Random _Random = new Random();
		public string GetRandomElement(List<string> list)
		{
			int index = _Random.Next(0, list.Count);
			return list[index];
		}

		public List<string> GetShuffledList(List<string> list)
		{
			List<string> result = new List<string>();
			List<string> work = new List<string>();
			list.ForEach(e => work.Add(e));
			while (work.Count > 0)
			{
				int index = _Random.Next(0, work.Count);
				result.Add(work[index]);
				work.RemoveAt(index);
			}

			return result;
		}
	}


	public class MyGeneric
	{
		private static readonly Random Random = new Random();
		private static T GetRandomElement<T>(List<T> list)
		{
			int index = Random.Next(0, list.Count );
			return list[index];
		}

		private static List<T> GetShuffledList<T>(List<T> list)
		{
			List<T> result = new List<T>();
			List<T> work = new List<T>();
			list.ForEach(e => work.Add(e));
			while( work.Count > 0 )
			{
				int index = Random.Next(0, work.Count);
				result.Add(work[index]);
				work[index] = default(T);
				work.RemoveAt(index);
			}

			return result;
		}

		public static void Test()
		{
			List<string> animals = new List<string>{ "Donkey", "Dog", "Seagull", "Cat", "Goat" };
			var oneAnimal = GetRandomElement(animals);
			Assert.IsNotNull(oneAnimal);

			var shuffledAnimals = GetShuffledList(animals);
			Assert.IsTrue(animals.Contains(shuffledAnimals.First()));

			List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
			var number = GetRandomElement(numbers);
			Assert.IsTrue( number >=1 && number <= 5);
			var shuffledNumbers = GetShuffledList(numbers);
            Assert.IsTrue(numbers.Contains(shuffledNumbers.First()));

            IMyRandomizer<string> myStringRandomizer = new MyStringRandomizer();
            oneAnimal = myStringRandomizer.GetRandomElement(animals);
			Assert.IsNotNull(oneAnimal);
            shuffledAnimals = myStringRandomizer.GetShuffledList(animals);
			CollectionAssert.AllItemsAreNotNull(shuffledAnimals);

            IMyRandomizer<int> myIntegerRandomizer = new MyIntegerRandomizer();
            number = myIntegerRandomizer.GetRandomElement(numbers);
            Assert.IsNotNull(number);
			shuffledNumbers = myIntegerRandomizer.GetShuffledList(numbers);
            CollectionAssert.AllItemsAreNotNull(shuffledNumbers);
		}
	}
}
