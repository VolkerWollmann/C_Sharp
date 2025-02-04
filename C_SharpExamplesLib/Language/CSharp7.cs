using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
    public class CSharp7
    {
        internal class Person
        {
            internal string FirstName { get; set; }
            internal string LastName { get; set; }

            internal Person(string firstName, string lastName)
            {
                FirstName = firstName;
                LastName = lastName;
            }

            // #deconstruct
            internal void Deconstruct(out string firstName, out string lastName)
            {
                firstName = FirstName;
                lastName = LastName;
            }
        }

        public static void TestDeconstruct()
        {
            Person p = new Person("Heinz", "Miller");
            var (f, l) = p;
            Console.WriteLine($"FirstName: {f} LastName: {l}");
        }

        // #BigInteger
        public static void TestBigInteger()
        {
            // ~ 150 bit integer
            BigInteger bi = BigInteger.Parse("12345678901234567890123456789012345678901234567890");
            bi = BigInteger.Add(bi, BigInteger.One);
            string bis = bi.ToString();
            Assert.AreEqual("12345678901234567890123456789012345678901234567891", bis);
        }
    }
}
