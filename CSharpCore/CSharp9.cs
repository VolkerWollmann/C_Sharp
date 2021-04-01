using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xunit.Abstractions;

namespace CSharpCore
{
    public class CSharp9
    {
        // #record 
        protected record Person(string FirstName, string LastName);

        public static void TestRecord()
        {
            Person p = new Person("Heinz", "Mueller");

            var (f, l) = p;
            Assert.AreEqual(f, p.FirstName);
            Console.WriteLine($"FirstName: {f} LastName: {l}");

            Person son = p with { LastName = "Muellerson" };
            Assert.AreEqual(p.FirstName, son.FirstName);
        }

        // #contentEquality vs #referentialEquality
        public static void ContentEquality(ITestOutputHelper testOutputHelper)
        {
            Person p1 = new Person("Heinz", "Mueller");
            Person p2 = new Person("Heinz", "Mueller");

            testOutputHelper.WriteLine($"P1 : FirstName: {p1.FirstName} LastName: {p1.LastName}  ");
            testOutputHelper.WriteLine($"P2 : FirstName: {p2.FirstName} LastName: {p2.LastName}  ");
            testOutputHelper.WriteLine($"p1 == p2 : {p1 == p2}");
            
            Assert.AreEqual(p1, p2);
        }

        // #property #init #accessor
        protected class Point
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
