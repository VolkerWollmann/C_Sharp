using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit.Abstractions;

namespace CSharpNew
{
	public abstract class CSharp12
	{
		// #primary constructor
		private class Animal(string name, string food)
		{
			public string Name { get; } = name;
			public string Food { get; } = food;
		}		

		public static void TestPrimaryConstructor()
		{
			var macchi = new Animal("Macchi", "Carrot");
			Assert.AreEqual("Macchi", macchi.Name);
			Assert.AreEqual("Carrot", macchi.Food);
		}
	}
}
