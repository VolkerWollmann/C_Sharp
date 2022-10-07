using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CSharpCore
{
    public class CSharp
    {
        public static async void StreamTest()
        {
            string[] lines = { "First line", "Second line", "Third line" };
            using StreamWriter file = new("WriteLines2.txt"); // works without brackets in C# 8.0

            foreach (string line in lines)
            {
                if (!line.Contains("Second"))
                {
                    await file.WriteLineAsync(line);
                }
            }

            file.Close();
            file.Close();    // rendundant, bit does not hurt
        }
    }
}
