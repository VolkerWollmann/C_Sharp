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
    }
}
