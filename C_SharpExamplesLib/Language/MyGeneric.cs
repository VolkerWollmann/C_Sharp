using System;
using System.Collections.Generic;

namespace C_Sharp.Language
{
	// #interface #generic #default
	internal interface IMyRandomizer<T>
	{
		T GetRandomElement(List<T> list);
		List<T> GetShuffledList(List<T> list);
	}

	// #generic
	internal class MyIntegerRandomizer : IMyRandomizer<int>
	{
		Random random = new Random();
		public int GetRandomElement(List<int> list)
		{
			int index = random.Next(0, list.Count);
			return list[index];
		}

		public List<int> GetShuffledList(List<int> list)
		{
			List<int> result = new List<int>();
			List<int> work = new List<int>();
			list.ForEach(e => work.Add(e));
			while (work.Count > 0)
			{
				int index = random.Next(0, work.Count);
				result.Add(work[index]);
				work.RemoveAt(index);
			}

			return result;
		}
	}

	internal class MyStringRandomizer : IMyRandomizer<string>
	{
        readonly Random _random = new Random();
		public string GetRandomElement(List<string> list)
		{
			int index = _random.Next(0, list.Count);
			return list[index];
		}

		public List<string> GetShuffledList(List<string> list)
		{
			List<string> result = new List<string>();
			List<string> work = new List<string>();
			list.ForEach(e => work.Add(e));
			while (work.Count > 0)
			{
				int index = _random.Next(0, work.Count);
				result.Add(work[index]);
				work.RemoveAt(index);
			}

			return result;
		}
	}


	public class MyGeneric
	{
		private static Random random = new Random();
		private static T GetRandomElement<T>(List<T> list)
		{
			int index = random.Next(0, list.Count );
			return list[index];
		}

		private static List<T> GetShuffledList<T>(List<T> list)
		{
			List<T> result = new List<T>();
			List<T> work = new List<T>();
			list.ForEach(e => work.Add(e));
			while( work.Count > 0 )
			{
				int index = random.Next(0, work.Count);
				result.Add(work[index]);
				work[index] = default(T);
				work.RemoveAt(index);
			}

			return result;
		}

		public static void Test()
		{
			List<string> animals = new List<string>{ "Donkey", "Dog", "Seagull", "Cat", "Goat" };
			var oneAnimal = GetRandomElement<string>(animals);
			var shuffledAnimals = GetShuffledList<string>(animals);

			List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
			var number = GetRandomElement<int>(numbers);
			var shuffledNumbers = GetShuffledList<int>(numbers);
		
			MyStringRandomizer myStringRandomizer = new MyStringRandomizer();
			oneAnimal = myStringRandomizer.GetRandomElement (animals);
			shuffledAnimals = myStringRandomizer.GetShuffledList(animals);

			MyIntegerRandomizer myIntegerRandomizer = new MyIntegerRandomizer();
			number = myIntegerRandomizer.GetRandomElement(numbers);
			shuffledNumbers = myIntegerRandomizer.GetShuffledList(numbers);
		}
	}
}
