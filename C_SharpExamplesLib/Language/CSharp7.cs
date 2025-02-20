using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language
{
    public abstract class CSharp7
    {
	    private class Person
        {
	        private string FirstName { get; }
	        private string LastName { get; }

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

        private static (int, string) ReturnMacchi()
        {
            return (1, "Macchi");
        }
        // #return tuple
        public static void ReturnTuple()
        {
            var (number, macchi) = ReturnMacchi();

            Assert.AreEqual(1, number);
            Assert.AreEqual("Macchi", macchi);
        }
    }
}
