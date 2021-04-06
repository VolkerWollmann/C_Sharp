using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace C_Sharp.Language
{
    /// <summary>
    /// #Defining a #alias type
    /// </summary>
    public class MyType
    {
        // Defining a type as class based on base type
        public class IntegerList2 : System.Collections.Generic.List<int>
        {

        }

        public class MyType2
        {
            public static void Test2()
            {
                // Compile Error : does not know about type alias
                //IntegerList x = new IntegerList();
            }
        }

        public class MyType3
        {
            public static void Test3()
            {
                List<int> x = new List<int> { 1 };
                Assert.IsNotNull(x);

            }
        }

        public static void Test()
        {
            List<int> x = new List<int>();
            IntegerList2 y = new IntegerList2();

            Assert.IsTrue(x.GetType() == typeof(List<int>));

            Type tx = typeof(List<int>);
            Assert.AreEqual(tx, typeof(List<int>));

            Type tx2 = typeof(System.Collections.Generic.List<int>);
            Assert.AreEqual(tx, tx2);

            Type tx3 = typeof(IntegerList2);
            Assert.AreNotEqual(tx, tx3);

            object tx2o = Activator.CreateInstance(tx2);
            Assert.IsInstanceOfType(tx2o, tx2);

            object tx3o = Activator.CreateInstance(tx3);
            Assert.IsInstanceOfType(tx3o, tx3);

            if (x is List<int> il)
                Assert.IsTrue(il != null);

            if (x is System.Collections.Generic.List<int> il2)
                Assert.IsTrue(il2 != null);

            if (y is System.Collections.Generic.List<int> il3)
                Assert.IsTrue(il3 != null);


            MyType2.Test2();

            MyType3.Test3();
        }
    }
}