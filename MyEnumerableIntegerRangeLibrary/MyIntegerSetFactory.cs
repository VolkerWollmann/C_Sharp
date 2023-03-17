using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp.Language.MyEnumerableIntegerRangeLibrary
{
    public class MyIntegerSetFactory
    {
        public static IMyIntegerSet[] GetTestData()
        {
            IMyIntegerSet[] data = {
                new MyIntegerSet(new List<int> {1, 2, 3}),
                new MyDatabaseIntegerSet(new List<int> {1, 2, 3})
            };

            return data;
        }

    }
}
