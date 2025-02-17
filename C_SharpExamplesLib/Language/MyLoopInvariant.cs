using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language
{
    public abstract class MyLoopInvariant
    {
        public static void LoopInvariant()
        {
            Random random = new Random();
            int[] randomNumbers = new int[10];
            for (int i = 0; i < randomNumbers.Length; i++)
            {
                randomNumbers[i] = random.Next(1, int.MaxValue); // Generates a positive random integer
            }

            for (int i = 0; i < randomNumbers.Length - 1; i++)
            {
                // Loop invariant: At the start of each iteration of the outer loop,
                // the last `i` elements of the array are sorted and in their final positions.
                //Assert.IsTrue( ??? );
                
                // Inner loop: Compares adjacent elements and swaps them if needed
                for (int j = 0; j < randomNumbers.Length - i - 1; j++)
                {
                    if (randomNumbers[j] > randomNumbers[j + 1])
                    {
                        // Swap elements
                        (randomNumbers[j], randomNumbers[j + 1]) = (randomNumbers[j + 1], randomNumbers[j]);
                    }
                }
                // Loop invariant holds: After each iteration of the outer loop,
                // the largest unsorted element is moved to its correct position.
            }

        }
    }
}
