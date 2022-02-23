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
        public class IntegerList2 : List<int>
        {

        }

        public class MyType2
        {
            public static void Test2()
            {
                // Compile Error : does not know about type alias
                // IntegerList x = new IntegerList();
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

            Type tx2 = typeof(List<int>);
            Assert.AreEqual(tx, tx2);

            Type tx3 = typeof(IntegerList2);
            Assert.AreNotEqual(tx, tx3);

            object tx2O = Activator.CreateInstance(tx2);
            Assert.IsInstanceOfType(tx2O, tx2);

            object tx3O = Activator.CreateInstance(tx3);
            Assert.IsInstanceOfType(tx3O, tx3);

            if (x is List<int> il)
                Assert.IsTrue(il != null);

            if (x is List<int> il2)
                Assert.IsTrue(il2 != null);

            if (y is List<int> il3)
                Assert.IsTrue(il3 != null);


            MyType2.Test2();

            MyType3.Test3();

            // #Reading #types from #assembly
            Type myType2Type = typeof(MyType2);
            string mytype2Name = myType2Type.FullName;

            // ReSharper disable once PossibleNullReferenceException
            var ass = Type.GetType(mytype2Name).Assembly;
            Assert.IsNotNull(ass);

            Type myType3Type = typeof(MyType3);
            string mytype3Name = myType3Type.FullName;
            // ReSharper disable once AssignNullToNotNullAttribute
            Type testType = ass.GetType(mytype3Name);

            Assert.AreEqual(myType3Type, testType);
        }
    }
}