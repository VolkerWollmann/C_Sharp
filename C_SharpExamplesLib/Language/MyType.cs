using Azure.Core.GeoJson;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language
{
    /// <summary>
    /// #Defining a #alias type
    /// </summary>
    public abstract class MyType
    {
        // Defining a type as class based on base type
        private class IntegerList2 : List<int>;

        private class MyType2
        {
            public static void Test2()
            {
                // Compile Error : does not know about type alias
                // IntegerList x = new IntegerList();
            }
        }

        private class MyType3
        {
            public static void Test3()
            {
                List<int> x = [1];
                Assert.IsNotNull(x);

                bool t1 = typeof(List<int>).IsInstanceOfType(x);
                Assert.IsTrue(t1);

                bool t2 = x is List<int>;
                Assert.IsTrue(t2);

                bool t3 = x is IList<int>;
                Assert.IsTrue(t3);

                object? i = Convert.ChangeType("1", typeof(int));
                Assert.IsInstanceOfType(i,typeof(int));
            }
        }

        public static void Test()
        {


            List<int> x = [];
            IntegerList2 y = [];

            Assert.IsTrue(x.GetType() == typeof(List<int>));

            Type tx = typeof(List<int>);
            Assert.AreEqual(typeof(List<int>), tx);

            Type tx2 = typeof(List<int>);
            Assert.AreEqual(tx, tx2);

            Type tx3 = typeof(IntegerList2);
            Assert.AreNotEqual(tx, tx3);

            object? tx2O = Activator.CreateInstance(tx2);
            Assert.IsInstanceOfType(tx2O, tx2);

            object? tx3O = Activator.CreateInstance(tx3);
            Assert.IsInstanceOfType(tx3O, tx3);

            if (x is { } il)
                Assert.IsNotNull(il);

            if (x is { } il2)
                Assert.IsNotNull(il2);

            if (y is List<int> il3)
                Assert.IsNotNull(il3);

            // IntegerList2 does not implement IConvertible
            // var yFromx = Convert.ChangeType(x, typeof(IntegerList2));
            // Assert.IsInstanceOfType(yFromx, typeof(IntegerList2));

            MyType2.Test2();

            MyType3.Test3();

            // #Reading #types from #assembly
            Type myType2Type = typeof(MyType2);
            string? mytype2Name = myType2Type.FullName;

            var ass = Type.GetType(mytype2Name!)?.Assembly;
            Assert.IsNotNull(ass);

            Type myType3Type = typeof(MyType3);
            string mytype3Name = myType3Type.FullName ?? "";

            Type? testType = ass.GetType(mytype3Name);

            Assert.AreEqual(myType3Type, testType);
        }
    }
}