using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CSharpCore
{
    public class CSharp9
    {
        // #record #contentEqualtity #referentialEquality
        internal record Person(string FirstName, string LastName);

        public static void TestRecord()
        {
            Person p = new Person("Heinz", "Müller");

            var (f, l) = p;
            Assert.AreEqual(f, p.FirstName);
            Console.WriteLine($"FirstName: {f} LastName: {l}");

            Person son = p with { LastName = "Müllerson" };
            Assert.AreEqual(p.FirstName, son.FirstName);
        }


        // #property #init #accessor
        internal class Point
        {
            public int X { get; init; }

            public int Y { get; init; }
        }
        public static void TestInit()
        {
            Point m = new Point { X = 41, Y = 42 };
            
            //Will not compile
            //m.X = 43;
        }
    }
}
