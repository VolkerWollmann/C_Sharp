using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace C_Sharp.OhterExamples
{
	internal class Person
	{
		public string Name { get; set; } = "";

		public Person? Friend { get; set; } 
	}

	internal class Animal
	{
		public string Type { get; set; } = "";
		public string Name { get; set; } = "";
	}

	public class SerialisationExample
	{
		public static void DoSerialisationWithReferences()
		{
			var alice = new Person {Name = "Alice"};
			var bob = new Person {Name = "Bob"};

			// Create circular reference
			alice.Friend = bob;
			bob.Friend = alice;

			List<Person> personList = new List<Person>
			{
				alice,
				bob
			};

			JsonSerializerSettings settings = new JsonSerializerSettings
			{
				PreserveReferencesHandling = PreserveReferencesHandling.Objects,
				Formatting = Formatting.Indented
			};

			string personListJson = JsonConvert.SerializeObject(personList, settings);

			Assert.IsTrue(personListJson.Contains("$id"));

			var deserializedPersonList = JsonConvert.DeserializeObject<List<Person>>(personListJson, settings);
			Assert.IsNotNull(deserializedPersonList);

			Person bob2 = deserializedPersonList.First(p => p.Name == "Bob");
			Assert.AreEqual("Bob", bob2.Name);
			Assert.AreEqual("Alice",bob2.Friend?.Name);
			

			Person alice2 = deserializedPersonList.First(p => p.Name == "Alice");
			Assert.AreEqual("Alice", alice2.Name);
			Assert.AreEqual("Bob", alice2.Friend?.Name);
		}

		public static void DeserializeFile()
		{
			var filePath = "..\\..\\..\\..\\C_SharpExamplesLib\\OhterExamples\\animals.json"; // Replace with the actual file path
			var jsonContent = File.ReadAllText(filePath);

			var settings = new JsonSerializerSettings
			{
				PreserveReferencesHandling = PreserveReferencesHandling.Objects, Formatting = Formatting.Indented
			};

			var deserializedAnimalList = JsonConvert.DeserializeObject<List<Animal>>(jsonContent, settings);
			Assert.IsNotNull(deserializedAnimalList);

			foreach (var animal in deserializedAnimalList) Console.WriteLine($"Name: {animal.Name}, Type: {animal.Type}");
		}
	}
}
