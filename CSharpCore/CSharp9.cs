using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCore
{
    public class CSharp9
    {
        // #record
        internal record Person( string FirstName, string LastName);

        public static void TestRecord()
        {
            Person p = new Person("Heinz", "Müller");
            var (f, l) = p;
            Console.WriteLine($"FirstName: {f} LastName: {l}");
        }
    }
}
