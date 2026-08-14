using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpNew
{
    // #extension #members
    // Extension blocks only work in a static, non-generic, non-nested class.
    public static class IntSequenceExtensions
    {
        // "source" is the receiver for every member in this block.
        extension(IEnumerable<int> source)
        {
            // An extension *property* - that is the new part, methods existed since C# 3.
            public bool IsAscending
            {
                get
                {
                    int previous = int.MinValue;
                    foreach (int value in source)
                    {
                        if (value < previous)
                            return false;

                        previous = value;
                    }

                    return true;
                }
            }

            public int SecondLargest() => source.OrderDescending().Skip(1).First();
        }

        // No parameter name: these members hang on the type, not on an instance.
        extension(IEnumerable<int>)
        {
            public static IEnumerable<int> Answer => [42];
        }
    }

    public abstract class CSharp14
    {
        #region extension members
        public static void ExtensionMembers()
        {
            int[] numbers = [1, 2, 3, 9];

            Assert.IsTrue(numbers.IsAscending);
            Assert.AreEqual(3, numbers.SecondLargest());

            // Static extension member, called on the type itself.
            Assert.AreEqual(42, IEnumerable<int>.Answer.First());
        }
        #endregion

        #region field keyword
        // #field
        private class Person
        {
            // No backing field is declared anywhere - "field" is the one the
            // compiler generates. Before C# 14 this needed an explicit _name.
            public string Name
            {
                get;
                set => field = value.Trim();
            } = "";
        }

        public static void FieldKeyword()
        {
            Person person = new() { Name = "  Heinz  " };

            Assert.AreEqual("Heinz", person.Name);
        }
        #endregion

        #region null conditional assignment
        // #nullconditional #assignment
        private static int _renameCalls;

        private static string Rename()
        {
            _renameCalls++;
            return "Renamed";
        }

        public static void NullConditionalAssignment()
        {
            _renameCalls = 0;
            Person person = null;

            // No NullReferenceException - and the right hand side is not evaluated.
            person?.Name = Rename();
            Assert.AreEqual(0, _renameCalls);

            person = new Person();
            person?.Name = Rename();

            Assert.AreEqual("Renamed", person.Name);
            Assert.AreEqual(1, _renameCalls);
        }
        #endregion

        #region nameof with unbound generics
        // #nameof #generic
        public static void NameOfUnboundGeneric()
        {
            // No type argument needed any more, List<int> used to be mandatory.
            string listName = nameof(List<>);
            string dictionaryName = nameof(Dictionary<,>);

            Assert.AreEqual("List", listName);
            Assert.AreEqual("Dictionary", dictionaryName);
        }
        #endregion

        #region lambda parameter modifiers
        // #lambda #out
        private delegate bool TryParse<T>(string text, out T result);

        public static void LambdaParameterModifiers()
        {
            // "out result" without repeating the type. Before C# 14 the whole
            // signature had to be spelled out: (string text, out int result) => ...
            TryParse<int> parse = (text, out result) => int.TryParse(text, out result);

            Assert.IsTrue(parse("42", out int number));
            Assert.AreEqual(42, number);
        }
        #endregion

        #region user defined compound assignment
        // #operator #compound
        private class Counter
        {
            public int Value { get; private set; }

            // Modifies in place. With only operator + the compiler would have to
            // build a new Counter for every +=.
            public void operator +=(int amount) => Value += amount;
        }

        public static void CompoundAssignmentOperator()
        {
            Counter counter = new();

            counter += 5;
            counter += 3;

            Assert.AreEqual(8, counter.Value);
        }
        #endregion
    }
}
