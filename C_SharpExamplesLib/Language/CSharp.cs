using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Langauge
{
    public class CSharp
    {
        /// <summary>
        /// test
        /// #local method #local function
        /// </summary>
        public static void LocalFunction()
        {
            int GetOne()
            {
                return 1;
            }

            var one = GetOne();

            Assert.AreEqual(one,1);
        }

        public static void MultiLineStringConstant()
        {
            string animals = @"donkey
                               dog
                               cat";

            Assert.IsNotNull(animals);
        }
    }
}
