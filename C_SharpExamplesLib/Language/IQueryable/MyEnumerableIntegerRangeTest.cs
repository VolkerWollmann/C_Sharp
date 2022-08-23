using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            foreach (int i in myIntegerRange)
            {
                if (i > 5)
                    break;
            }

            // myIntegerRange stands at 6
            // uses int IEnumerator<int>.Current
            var a = ((IEnumerator<int>)myIntegerRange).Current;
            Assert.AreEqual(a, 6);

            // uses object IEnumerator.Current
            var b = ((IEnumerator)myIntegerRange).Current;
            Assert.IsNotNull(b);

            // does work
            // uses public IEnumerator<int> GetEnumerator()
            var d = myIntegerRange.ToList();
            Assert.IsNotNull(d);
        }
    }
}
