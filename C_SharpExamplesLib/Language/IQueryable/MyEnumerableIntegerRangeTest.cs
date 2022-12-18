using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using C_Sharp.Language.MyEnumerableIntegerRangeLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language.IQueryable
{
    public class MyEnumerableIntegerRangeTest
    {

        public static void Test_IEnumerable()
        {
            // uses public IEnumerator<int> GetEnumerator()
            // uses public bool MoveNext()
            // uses int IEnumerator<int>.Current
            MyEnumerableIntegerRange myIntegerRange = new MyEnumerableIntegerRange(1, 10);
            int test = 0;
            foreach (int i in myIntegerRange)
            {
                test++;
                if (i > 5)
                    break;
                Assert.IsTrue(test < 6);
            }

            foreach (int i in myIntegerRange)
            {
                test = i;
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
    }
}
