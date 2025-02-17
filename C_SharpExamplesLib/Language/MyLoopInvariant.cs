using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language
{
    public abstract class MyLoopInvariant
    {
        public static void LoopInvariant()
        {
            Random random = new Random();
            int[] rn = new int[10];
            for (int i = 0; i < rn.Length; i++)
            {
                rn[i] = random.Next(1, 100); // Generates a positive random integer
            }

            for (int i = 0; i < rn.Length - 1; i++)
            {
                // Loop invariant: At the start of each iteration of the outer loop,
                // the last `i` elements of the array are sorted and in their final positions.
                
                
                // Inner loop: Compares adjacent elements and swaps them if needed
                for (int j = 0; j < rn.Length - i - 1; j++)
                {
                    if (rn[j] > rn[j + 1])
                    {
                        // Swap elements
                        (rn[j], rn[j + 1]) = (rn[j + 1], rn[j]);
                    }
                }

                // Loop invariant holds: After each iteration of the outer loop,
                // the largest unsorted element is moved to its correct position.
                // In my words all elements up to i are smaller or equal than the elements from i onwards 
                var unsortedSet = rn.Take(rn.Length - i).ToList();
                var sortedSet = rn.Skip(rn.Length - i).ToList();
                Assert.IsTrue(unsortedSet.All(n => sortedSet.All(m => n <= m)));
            }

        }
    }
}
