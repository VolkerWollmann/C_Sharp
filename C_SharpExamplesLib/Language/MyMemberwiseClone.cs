namespace C_SharpExamplesLib.Language
{
    public abstract class MyMemberwiseClone
    {
        internal class IdInfo(int idNumber)
        {
            public int IdNumber = idNumber;
        }

        internal class Person
        {
            public int Age;
            public string Name;
            internal IdInfo IdInfo;

            public Person ShallowCopy()
            {
                return (Person)MemberwiseClone();
            }

            public Person DeepCopy()
            {
                Person other = new Person
                {
	                IdInfo = new IdInfo(IdInfo.IdNumber),
	                Name = new string(Name),
	                Age = Age
                };
                return other;
            }

            internal Person()
            {
                Name = String.Empty;
                IdInfo = new IdInfo(0);
            }
        }

        public static void Test()
        {
            // Create an instance of Person and assign values to its fields.
            Person p1 = new Person
            {
	            Age = 42,
	            Name = "Sam",
	            IdInfo = new IdInfo(6565)
            };

            // Perform a shallow copy of p1 and assign it to p2.
            Person p2 = p1.ShallowCopy();

            // Display values of p1, p2
            Console.WriteLine("Original values of p1 and shallow copy p2:");
            Console.WriteLine("   p1 instance values: ");
            DisplayValues(p1);
            Console.WriteLine("   p2 instance values:");
            DisplayValues(p2);

            // Change the value of p1 properties and display the values of p1 and p2.
            p1.Age = 32;
            p1.Name = "Frank";
            p1.IdInfo.IdNumber = 7878;
            Console.WriteLine("\nValues of p1 and shallow copy p2 after changes to p1:");
            Console.WriteLine("   p1 instance values: ");
            DisplayValues(p1);
            Console.WriteLine("   p2 instance values:");
            DisplayValues(p2);

            // Make a deep copy of p1 and assign it to p3.
            Person p3 = p1.DeepCopy();
            // Change the members of the p1 class to new values to show the deep copy.
            p1.Name = "George";
            p1.Age = 39;
            p1.IdInfo.IdNumber = 8641;
            Console.WriteLine("\nValues of p1 and deep copy p3 after changes to p1:");
            Console.WriteLine("   p1 instance values: ");
            DisplayValues(p1);
            Console.WriteLine("   p3 instance values:");
            DisplayValues(p3);
        }

        internal static void DisplayValues(Person p)
        {
            Console.WriteLine("      Name: {0}, Age: {1:d}", p.Name, p.Age);
            Console.WriteLine("      IdInfo.IdNumber: {0:d}", p.IdInfo.IdNumber);
        }
    }
}
