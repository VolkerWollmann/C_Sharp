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
			var person1 = new Person {Name = "Alice"};
			var person2 = new Person {Name = "Bob"};

			// Create circular reference
			person1.Friend = person2;
			person2.Friend = person1;

			JsonSerializerSettings settings = new JsonSerializerSettings
			{
				PreserveReferencesHandling = PreserveReferencesHandling.Objects,
				Formatting = Formatting.Indented
			};

			string json = JsonConvert.SerializeObject(person1, settings);

			Assert.IsTrue(json.Contains("$id"));

			var deserializedPerson = JsonConvert.DeserializeObject<Person>(json, settings);

			Assert.AreEqual("Alice",deserializedPerson.Name);
			Assert.AreEqual("Bob", deserializedPerson.Friend.Name);
		}
	}
}
