/// <summary>
/// #Defining a #alias type
/// </summary>

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Langauge
{
    public class MyType
    {
        // Defining a type as class based on base type
        public class IntegerList2 : System.Collections.Generic.List<int>
        {

        }

        public static void Test()
        {
            List<int> x = new List<int>();
            IntegerList2 y = new IntegerList2();

            Assert.IsTrue(x.GetType() == typeof(List<int>));

            Type tx = typeof(List<int>);
            Type tx2 = typeof(System.Collections.Generic.List<int>);
            Type tx3 = typeof(IntegerList2);

            object tx2o = Activator.CreateInstance(tx2);
            object tx3o = Activator.CreateInstance(tx3);

            if (x is List<int> il)
                Assert.IsTrue(il != null);

            if (x is System.Collections.Generic.List<int> il2)
                Assert.IsTrue(il2 != null);

            if (y is System.Collections.Generic.List<int> il3)
                Assert.IsTrue(il3 != null);

        }
    }

    public class MyType2
    {
        public static void Test()
        {
            // Compile Error : does not know about type alias
            //IntegerList x = new IntegerList();
        }
    }

    public class MyType3
    {
        public static void Test()
        {
            List<int> x = new List<int>();
        }
    }
}