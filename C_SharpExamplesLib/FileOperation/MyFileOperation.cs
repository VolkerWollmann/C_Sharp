using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace C_SharpExamplesLib.FileOperation
{
    public abstract class MyFileOperation
    {
        private const string FileName = "C_SharpExamplesLib\\FileOperation\\TextFile.txt";

        // comment in master branch
        // #c# #file #read all lines
        public static void ReadLinesFromFile()
        {
            string directory = Directory.GetCurrentDirectory();

            string fullName = directory + "\\..\\..\\..\\..\\" + FileName;

            IEnumerable<string> animals = File.ReadAllLines(fullName);

            IEnumerable<string> testAnimals = ["Donkey", "Dog", "Cat", "Seagull"];

            Assert.IsTrue(animals.ToList().TrueForAll(a => testAnimals.Contains(a)));
        }

        /// <summary>
        /// use nuget package System.IO.Abstractions.TestingHelpers
        /// if IFileSystem is sufficient
        /// </summary>
        public static void MockFile()
        {
            var mockFs = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"C:\temp\file.txt", new MockFileData("hello world") }
            });

            var path = @"C:\temp\file.txt";

            var content = mockFs.File.ReadAllText(path);

            Assert.AreEqual("hello world", content);

            // System.IO.Abstractions.FileSystemStream
            FileSystemStream file = mockFs.File.Open(path, FileMode.Open);

            using var reader = new StreamReader(file);
            var lines = new List<string>();
            while (reader.ReadLine() is { } line)
            {
                lines.Add(line);
            }

            Assert.AreEqual("hello world", lines[0]);

            file.Close();
        }
    }
}
