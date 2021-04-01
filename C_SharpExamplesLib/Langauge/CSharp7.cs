using System;

namespace C_Sharp
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
            Person p = new Person("Heinz", "Müller");
            var (f, l) = p;
            Console.WriteLine($"FirstName: {f} LastName: {l}");
        }
    }
}
