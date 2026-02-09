using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpNew
{
    public class CSharp11
    {
        public class User
        {
            public required string Name { get; init; }
            public required int Age { get; init; }
        }

        public static void TestRequiredMembers()
        {
            //User u1 = new User(); // compile error: Name and Age are required

            User u2 = new User { Name = "Heinz", Age = 42 };
        }
    }
}
