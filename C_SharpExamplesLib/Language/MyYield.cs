using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static IEnumerable<int> OneToSix()
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

    }
}
