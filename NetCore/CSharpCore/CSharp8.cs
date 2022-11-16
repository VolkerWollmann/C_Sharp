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

        #region property, postional pattern matching 
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

        // #switch #pattern matching #recursive #structured #property pattern
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

        private static string PersonPositionalMatch(Person person)
        {
            return person switch
            {
                //("Polizia","Wolfgang", _, ("Macchi")) => "", not recursive
                ("Polizia", "Wolfgang", _, _) => "Polizia and Wolfgang match",
                ("Polizia", _, _, _) => "Polizia matches",
                var (_, _, _, _) => "nothing matches",
                _ => ""
            };
        }

        public static void PropertyPatternMatching()
        {
            Person wolfgang1 = new Person("Polizia", "Wolfgang", 35, null);
            Assert.AreEqual(PersonMatch(wolfgang1), "Wolfgang ohne Macchi");

            Person wolfgang2 = new Person("Polizia", "Wolfgang", 35, new Animal("Macchi"));
            Assert.AreEqual(PersonMatch(wolfgang2), "Wolfgang mit Macchi");
        }

        public static void PositionalPatternMatching()
        {
            Person wolfgang1 = new Person("Polizia", "", 35, null);
            Assert.AreEqual(PersonPositionalMatch(wolfgang1), "Polizia matches");

            Person wolfgang2 = new Person("Polizia", "Wolfgang", 35, new Animal("Macchi"));
            Assert.AreEqual(PersonPositionalMatch(wolfgang2), "Polizia and Wolfgang match");
        }

        #endregion

        #region tuple pattern matching
        // #Tuple #pattern #Tuple pattern #switch expression
        private static string RockPaperScissors(string first, string second)
        => (first, second) switch
        {
            ("rock", "paper") => "rock is covered by paper. Paper wins.",
            ("rock", "scissors") => "rock breaks scissors. Rock wins.",
            ("paper", "rock") => "paper covers rock. Paper wins.",
            ("paper", "scissors") => "paper is cut by scissors. Scissors wins.",
            ("scissors", "rock") => "scissors is broken by rock. Rock wins.",
            ("scissors", "paper") => "scissors cuts paper. Scissors wins.",
            (_, _) => "tie"
        };

        public static void TuplePatternMatching()
        {
            Assert.AreEqual(RockPaperScissors("rock", "scissors"), "rock breaks scissors. Rock wins.");
            Assert.AreEqual(RockPaperScissors("rock","rock"), "tie");
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
            // ! #null forgiving operator : will suppress compiler warning
            var person = new NullablePerson(null!);
        }
        #endregion

        #region Null-coalescing assignment
        // #Null-coalescing assignment #Null #coalescing assignment
        public static void NullCoalescingExample()
        {
            int? i = null!;

            i ??= 5;
            Assert.AreEqual(5, i);

            // ReSharper disable once ConstantNullCoalescingCondition
            i ??= 6;
            Assert.AreEqual(5, i);
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
