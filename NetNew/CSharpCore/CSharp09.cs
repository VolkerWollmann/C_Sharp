using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit.Abstractions;

namespace CSharpNew
{
    public abstract class CSharp09
    {
        // Visual Studio 2022 : comment test change

        // #record #value tuple type
        private record Person(string FirstName, string LastName);

        public static void TestRecord()
        {
            Person p = new("Heinz", "Mueller");

            var (f, l) = p;
            Assert.AreEqual(f, p.FirstName);
            Console.WriteLine($"FirstName: {f} LastName: {l}");

            var son = p with { LastName = "Muellerson" };
            Assert.AreEqual(p.FirstName, son.FirstName);
        }

        // #contentEquality vs #referentialEquality
        public static void ContentEquality(ITestOutputHelper testOutputHelper)
        {
            Person p1 = new("Heinz", "Mueller");
            Person p2 = new("Heinz", "Mueller");

            testOutputHelper.WriteLine($"P1 : FirstName: {p1.FirstName} LastName: {p1.LastName}  ");
            testOutputHelper.WriteLine($"P2 : FirstName: {p2.FirstName} LastName: {p2.LastName}  ");
            testOutputHelper.WriteLine($"p1 == p2 : {p1 == p2}");

            Assert.AreEqual(p1, p2);
        }

        // #property #init #accessor #object initializer
        private class Point
        {
            public int X { get; init; }

            public int Y { get; init; }
        }
        public static void TestInit()
        {
            var m = new Point { X = 41, Y = 42 };
            Assert.AreEqual(41, m.X);
            Assert.AreEqual(42, m.Y);

            //Will not compile
            //m.X = 43;
        }
    }
}
