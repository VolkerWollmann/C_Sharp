using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
    public class MyYield
    {
        private static IEnumerable<int> OneToThree()
        {
            yield return 1;
            yield return 2;
            yield return 3;
        }

        private static IEnumerable<int> FourToSix()
        {
            yield return 4;
            yield return 5;
            yield return 6;
        }

        private static IEnumerable<int> OneToSix()
        {
            foreach (int i in OneToThree())
            {
                yield return i;
            }

            foreach (int i in FourToSix())
            {
                yield return i;
            }
        }

        public static void Test()
        {
            int i = 1;
            foreach (int j in MyYield.OneToSix())
            {
                Assert.AreEqual(i, j);
                i++;
            }
        }
    }
}
