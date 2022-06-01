using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpCore
{
    // Karlsruhe, VS2022, 09.03.22, 2
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

        #region switch case guards
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
            Assert.AreEqual(r.X, 6);
        }
        #endregion

        #region pattern matching
        private class Animal
        {
            internal string Name { get; }

            internal Animal(string name)
            {
                Name = name;
            }

            // ReSharper disable once UnusedMember.Local
            public void Deconstruct(out string name)
            {
                name = Name;
            }
        }

        private class Person
        {
            internal string FamilyName { get; }
            internal string FirstName { get; }

            internal int Age { get; }

            internal Animal Pet { get; }

            internal Person(string familyName, string firstName, int age, Animal pet)
            {
                FamilyName = familyName;
                FirstName = firstName;
                Age = age;
                Pet = pet;
            }

            // ReSharper disable once UnusedMember.Local
            public void Deconstruct(out string familyName, out string firstName, out int age, out Animal pet)
            {
                familyName = FamilyName;
                firstName = FirstName;
                age = Age;
                pet = Pet;
            }
        }

        private static string S(string s)
        {
            return s;
        }

        // #pattern matching #recursive #structured
        // https://docs.microsoft.com/en-us/archive/msdn-magazine/2019/may/csharp-8-0-pattern-matching-in-csharp-8-0
        // https://docs.microsoft.com/de-de/dotnet/csharp/language-reference/proposals/csharp-8.0/patterns
        [SuppressMessage("ReSharper", "RedundantAlwaysMatchSubpattern")]
        [SuppressMessage("ReSharper", "RedundantTypeCheckInPattern")]
        private static string PersonMatch(Person person)
        {
            return person switch
            {
                (Person { FamilyName: "Polizia", FirstName: { } firstName, Age: _, Pet: null })
                    => S(firstName + " ohne Macchi"),
                
                (Person { FamilyName: "Polizia", FirstName: { } firstName, Age: _, Pet: (Animal { Name: { } petName }) })
                    => S(firstName + " mit " + petName),
                _ => throw new NotImplementedException(),
            };
        }

        public static void PatternMatching()
        {
            Person wolfgang1 = new Person("Polizia", "Wolfgang", 35, null);
            Assert.AreEqual(PersonMatch(wolfgang1), "Wolfgang ohne Macchi");

            Person wolfgang2 = new Person("Polizia", "Wolfgang", 35, new Animal("Macchi"));
            Assert.AreEqual(PersonMatch(wolfgang2), "Wolfgang mit Macchi");
        }
        #endregion

        #region null forgiving operator
#nullable enable
        public class NullablePerson
        {
            public NullablePerson(string name) => Name = name ?? throw new ArgumentNullException(name);

            public string Name { get; }
        }

        public static void NullNameShouldThrowTest()
        {
            // ! null forgiving operator
            var person = new NullablePerson(null!);
        }
        #endregion
    }
}
