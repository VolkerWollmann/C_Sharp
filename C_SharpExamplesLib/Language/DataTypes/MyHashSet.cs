using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language.DataTypes
{
    public class MyHashSet
    {
        public static void Test()
        {
            HashSet<int> integerHashSet = new HashSet<int>();

            integerHashSet.Add(1);
            integerHashSet.Add(2);
            integerHashSet.Add(1);

            Assert.AreEqual(integerHashSet.Count, 2);
        }

    }
}
