using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace C_Sharp.FileOperation
{
	public class MyFileOperation
	{
		private const string FileName = "FileOperation\\TextFile.txt";

		// #c# #file #read all lines
		public static void ReadLinesFromFile()
		{
			string directory = Directory.GetCurrentDirectory();

			string fullName = directory + "\\" + FileName;

			IEnumerable<string> animals = File.ReadAllLines(fullName);

			IEnumerable<string> testAnimals = new string[] { "Esel", "Hund", "Katze", "Möwe" };

			Assert.IsTrue(animals.ToList().TrueForAll(a => testAnimals.Contains(a)));
		}
	}
}
