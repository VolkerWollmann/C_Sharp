using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CSharpCore
{
    #region stream
    public class CSharp
    {
        // #Stream
        public static async void StreamTestUnconventionalUsage()
        {
            string[] lines = { "First line", "Second line", "Third line" };
            using StreamWriter fileWriter = new("WriteLines2.txt"); // works without brackets in C# 8.0

            foreach (string line in lines)
            {
                if (!line.Contains("Second"))
                {
                    await fileWriter.WriteLineAsync(line);
                }
            }

            fileWriter.Close();
            fileWriter.Close();    // rendundant, bit does not hurt

            using StreamReader fileReader = new("WriteLines2.txt");
            while (!fileReader.EndOfStream)
            {
                string line = fileReader.ReadLine();
                Assert.IsTrue(lines.Contains(line));
            }
        }

        // #stream
        public static async void StreamTestFlush()
        {
            using MemoryStream p2MemoryStream = new MemoryStream();
            // open p2File
            var sw = new StreamWriter(p2MemoryStream);

            sw.WriteLine("Esel");
            sw.WriteLine("Hund");
            sw.Flush();
            p2MemoryStream.Position = 0;
            
            StreamReader streamReader = new StreamReader(p2MemoryStream);
            string s = streamReader.ReadToEnd();

            Assert.IsTrue(s.Contains("Hund"));
        }
        #endregion

        #region IDisposable
        //#IDisposable
        static int step = 0;

   
        private class MyData : IDisposable
        {
            public void Dispose()
            {
                step = 1;
            }
        }

        private static void Use_MyData()
        {
            using MyData data = new MyData();
            Assert.AreEqual(step, 0);
        }
        public static void Test_IDisposable()
        {
            step = 0;
            Use_MyData();
            Assert.AreEqual(step, 1);
        }

        #endregion
    }
}
