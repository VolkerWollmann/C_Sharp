using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language
{
    // #yield
    public abstract class MyYield
    {
        private static IEnumerable<int> OneToThree()
        {
            Console.WriteLine("1..3: 1");
            yield return 1;
            Console.WriteLine("1..3: 2");
            yield return 2;
            Console.WriteLine("1..3: 3");
            yield return 3;
        }

        private static IEnumerable<int> FourToSix()
        {
            for(int i = 4; i <= 6; i++)
            {
				Console.WriteLine("4..6: " + i);
				yield return i;
            }
        }

        private static IEnumerable<int> OneToSix()
        {
            foreach (int i in OneToThree().Union(FourToSix()))
            {
                Console.WriteLine("1..6:" + i);
                yield return i;
            }
        }

        private static IEnumerable<int> OneToSixUnionOfLists()
        {
            foreach (int i in OneToThree().ToList().Union(FourToSix().ToList()))
            {
                Console.WriteLine("1..6 Union of Lists:" + i);
                yield return i;
            }
        }

		private static IEnumerable<int> OneToSixUnionAsEnumerableOfUnion()
		{
			foreach (int i in OneToThree().ToList().Union(FourToSix()))
			{
				Console.WriteLine("1..6 as Enumerable of Union:" + i);
				yield return i;
			}
		}

		private static IEnumerable<int> OneToSixUnionOfEnumerableToList()
        {
            foreach (int i in OneToThree().Union(FourToSix()).ToList())
            {
                Console.WriteLine("1..6 Union of Enumerable to List: Return " + i);
                yield return i;
            }
        }

        private static IEnumerable<int> OneToSixAsThreeLists()
        {
            int k = 0;
            List<int> all = OneToThree().Union(FourToSix()).ToList();

            for (int i = 0; i <= 2; i++)
            {
                List<int> l = [all[2 * i], all[(2 * i) + 1]];
                foreach (int j in l)
                {
                    Console.WriteLine("1..6 as two lists: List {0} Element {1}: {2} ", i, k++, j);
                    yield return j;
                }
            }
        }

        private static void Test(IEnumerable<int> iEnumerable)
        {
            int i = 1;
            foreach (int j in iEnumerable)
            {
                Assert.AreEqual(i, j);
                i++;
            }

            Console.WriteLine("--");
        }


        public static void Yield()
        {
            Test(OneToSix());
            Test(OneToSixUnionOfLists());
            Test(OneToSixUnionOfEnumerableToList());
            Test(OneToSixAsThreeLists());
			Test(OneToSixUnionAsEnumerableOfUnion());
		}

        public static void TestIEnumerableAssignment()
        {
            //#IEnumerable OneToSix is only assigned, Debugger F11 will pass by
            var l16 = OneToSix();
           
            var l16Enumerator = l16.GetEnumerator();
            l16Enumerator.MoveNext(); //#IEnumerable is used
            var firstElement = l16Enumerator.Current;
            Assert.AreEqual(1,firstElement);
            l16Enumerator.Dispose();
        }
    }
}
