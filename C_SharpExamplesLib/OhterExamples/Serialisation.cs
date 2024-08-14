using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp.OhterExamples
{
	internal class Person
	{
		public string Name { get; set; } = "";

		public Person Friend { get; set; }
	}

	public class SerialisationExample
	{
		public static void DoSerialisation()
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

			Person person3 = deserializedPersonList.First(p => p.Name == "Bob");
			Assert.AreEqual("Bob", person3.Name);
			Assert.AreEqual("Alice",person3.Friend.Name);
			

			Person person4 = deserializedPersonList.First(p => p.Name == "Alice");
			Assert.AreEqual("Alice", person4.Name);
			Assert.AreEqual("Bob", person4.Friend.Name);
		}
	}
}
