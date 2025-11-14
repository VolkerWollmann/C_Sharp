using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;
using System.Collections.Generic;
using MyEnumerableIntegerRangeLibrary;

namespace UnitTest
{
    [TestClass]
    public class IEnumerable
    {
        private MyIntegerSetFactory _myIntegerSetFactory;

        [TestInitialize]
        public void Initialize()
        {
            _myIntegerSetFactory = new MyIntegerSetFactory();

            _myIntegerSetFactory.GetIntegerSets();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _myIntegerSetFactory.Dispose();
        }

        [TestMethod]
        public void Test_IEnumerable()
        {
            // uses public IEnumerator<int> GetEnumerator()
            // uses public bool MoveNext()
            // uses int IEnumerator<int>.Current
            MyEnumerableIntegerRange myIntegerRange = new MyEnumerableIntegerRange(1, 10, "Macchi");
            int test = 0;
            foreach (int i in myIntegerRange)
            {
                test++;
                if (i > 5)
                    break;
                Assert.IsLessThan(6, test);
            }

            Assert.AreEqual("Macchi", myIntegerRange.Name);
        }

        [TestMethod]
        public void Test_IEnumerable_TwoEnumeratorsOnIEnumerable()
        {
            MyEnumerableIntegerRange myIntegerRange = new MyEnumerableIntegerRange(1, 10);
            foreach (int i in myIntegerRange)
            {
                var test = i;
                foreach (int j in myIntegerRange)
                {
                    Assert.AreEqual(test, i);
                    if (j > 3)
                        break;

                    Console.WriteLine("i:{0} j:{1}", i, j);
                }

                Assert.AreEqual(test, i);

                if (i > 3)
                    break;
            }

            // Does not compile, because it is internal and that is wanted. 
            // myIntegerRange.ValueAt(-1);

            // does work
            // uses public IEnumerator<int> GetEnumerator()
            var d = myIntegerRange.ToList();
            Assert.IsNotNull(d);
        }

        [TestMethod]
        public void Test_IEnumerable_Where()
        {
            MyEnumerableIntegerRange myIntegerRange = new MyEnumerableIntegerRange(1, 10);

            List<int> test = [2, 4, 6, 8, 10];
            foreach (int i in myIntegerRange.Where(i => i % 2 == 0))
            {
                Assert.Contains(i, test);
            }
        }

        [TestMethod]
        public void Test_IEnumerable_FromMemoryIntegerSet()
        {
            MyMemoryIntegerSet myMemoryIntegerSet = new MyMemoryIntegerSet([1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);
            int test = 0;
            foreach (int i in myMemoryIntegerSet)
            {
                test++;
                if (i > 5)
                    break;
                Assert.IsLessThan(6, test);
            }
        }
    }
}