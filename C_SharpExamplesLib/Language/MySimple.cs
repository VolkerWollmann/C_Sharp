using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language
{
    public abstract class MySimpleCSharp
    {
        // #index operator, #indexer, #range
        private class IndexClass
        {
            public readonly int[] Numbers = new int[10];

            public int this[int index]
            {
                get => Numbers[index];
                private set => Numbers[index] = value;
            }

            internal IndexClass()
            {
                Random random = new Random();
                Enumerable.Range(0, 10).ToList().ForEach(i => { this[i] = random.Next(1, 10); });
            }
        }

        // #Enumerable #empty list 
        static void EnumerableTest()
        {
            IEnumerable<int> emptyIntegerList = [];
            Assert.IsFalse(emptyIntegerList.Any());
        }

        public static void Test()
        {
            string s = "";
            MethodRef(ref s);
            Assert.AreEqual("donkey", s);

            string? s2 = null;
            Method(s2);
            Assert.IsNull(s2);

            IndexClass indexClass = new IndexClass();
            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(indexClass[i], indexClass.Numbers[i]);
            }

            EnumerableTest();
        }

        #region call by reference
        static void MethodRef(ref string s)
        {
            s = s + "donkey";
        }

        static void Method(string? s)
        {
            var x = $"{s}donkey";
            Assert.Contains("donkey", x);
        }

        private class MyClass
        {
            internal string Name { get; set; }

            internal MyClass(string name)
            {
                Name = name;
            }
        }

        static void MethodClassRef(ref MyClass myClass)
        {
            myClass.Name = "dog";
        }

        static void MethodClass(MyClass myClass)
        {
            myClass.Name = "seagull";
        }

        private struct MyStruct
        {
            internal string Name { get; set; }
        }

        static void MethodStructRef(ref MyStruct myStruct)
        {
            myStruct.Name = "dog";
        }

        static void MethodStruct(MyStruct myStruct)
        {
            myStruct.Name = "seagull";
        }

        //#call by reference
        public static void TestCallByReference()
        {
            /* build in type string */
            string s = "";
            MethodRef(ref s);
            Assert.AreEqual("donkey", s);

            string? s2 = null;
            Method(s2);
            Assert.IsNull(s2);

            /* class reference */
            MyClass myClass = new MyClass("donkey");
            MethodClassRef(ref myClass);
            Assert.AreEqual("dog", myClass.Name);

            MethodClass(myClass);
            Assert.AreEqual("seagull", myClass.Name);

            /* struct reference */
            MyStruct myStruct = new MyStruct
            {
                Name = "donkey"
            };

            MethodStructRef(ref myStruct);
            Assert.AreEqual("dog", myStruct.Name);

            MethodStruct(myStruct);
            Assert.AreEqual("dog", myStruct.Name);
        }

        #endregion
    }
}
