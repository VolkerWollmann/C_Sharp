﻿using System.Drawing;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpCore
{
    public class CSharp8
    {
        public static void RangeOperators()
        {
            var array = new [] { 1, 2, 3, 4, 5 };
            var slice1 = array[2..^3];    // array[new Range(2, new Index(3, fromEnd: true))]
            var slice2 = array[..^3];     // array[Range.EndAt(new Index(3, fromEnd: true))]
            var slice3 = array[2..];      // array[Range.StartAt(2)]
            var slice4 = array[..];       // array[Range.All]

            Assert.IsNotNull(slice1);
            Assert.IsNotNull(slice2);
            Assert.IsNotNull(slice3);
            Assert.IsNotNull(slice4);
        }

        private readonly struct Point
        {
            public Point(int x, int y) => (X, Y) = (x, y);

            public int X { get; }
            public int Y { get; }
        }

        private static Point Transform(Point point) => point switch
        {
            { X: 0, Y: 0 } => new Point(0, 0),
            { X: var x, Y: var y } when x < y => new Point(x + y, y),
            { X: var x, Y: var y } when x > y => new Point(x - y, y),
            { X: var x, Y: var y } => new Point(2 * x, 2 * y),
        };

        /// <summary>
        /// #switch #case
        /// </summary>
        public static void CaseGuards()
        {
            Point r = Transform(new Point(3, 3));
        }

        private class Person
        {
            internal string FamilyName { private set; get; }
            internal string FirstName { private set; get; }

            internal int Age { private set; get; }

            internal Person(string familyName, string firstName, int age)
            {
                FamilyName = familyName;
                FirstName = firstName;
                Age = age;
            }

            public void Deconstruct(out string familyName, out string firstName, out int age)
            {
                familyName = FamilyName;
                firstName = FirstName;
                age = Age;
            }
        }

        private static string S(string s)
        {
            return s;
        }
        private static string T(Person person)
        {
            return person switch
            {
                ("Polizei", { } firstName, _) => S(firstName),
            };
        }

        public static void PatternMatching()
        {
            Person macchi = new Person("Polizei", "Macchi", 35);
            Assert.AreEqual(T(macchi), "Macchi");
        }
    }
}
