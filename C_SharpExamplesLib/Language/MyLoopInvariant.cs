using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language
{
    public abstract class MyLoopInvariant
    {
        private static int[] InitArray()
        {
            Random random = new Random();
            int[] rn = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
            for (int m = 0; m < 10; m++)
            {
                int i1 = random.Next(0, 10);
                int i2 = random.Next(0, 10);
                (rn[i1], rn[i2]) = (rn[i2], rn[i1]);
            }

            return rn;
        }

        public static void LoopInvariant1()
        {
            int[] rn = InitArray();
            int n = rn.Length - 1;

            int max = rn[0];
            for (int i = 0; i <= n; i++)
            {
                if (rn[i] > max)
                    max = rn[i];
                
                // Loop invariant: The maximum element found so far is stored in 'max'.
                Assert.IsTrue(rn.Take(i+1).All( rni => rni <= max));
            }

            // Loop invariant: The maximum element found is stored in 'max'.
            Assert.IsTrue(rn.All(rni => rni <= max));
        }
        public static void LoopInvariant2()
        {
            
            int[] rn = InitArray();
            int n = rn.Length - 1;
            
            for (int i = 0; i < n; i++)
            {
                // Outer loop invariant:
                for (int i2 = 0; i2 < i-1; i2++) { Assert.IsTrue(rn[n - i2 - 1] < rn[n - i2]); }
                
                for (int j = 0; j < n - i; j++)
                {
                    if (rn[j] > rn[j + 1])
                    {
                        // Swap elements
                        (rn[j], rn[j + 1]) = (rn[j + 1], rn[j]);
                    }

                    // Inner loop invariant
                    Assert.IsTrue(rn[j] < rn[j+1]); 
                }

                // Inner loop invariant
                Assert.IsTrue(rn[n - i - 1 ] < rn[n-i]);

                // Outer loop invariant:
                for(int i2 = 0; i2 < i; i2++) { Assert.IsTrue(rn[n - i2 - 1] < rn[n - i2]); }
            }

            // Outer loop invariant: the list is sorted
            for (int i =0; i < n; i++) { Assert.IsTrue(rn[n - i - 1] < rn[n - i]); }


            {
                // Outer loop invariant: as LINQ expression
                Assert.IsTrue(rn.Zip(rn.Skip(1), (rni, rni1) => rni < rni1).All(x => x));

                // Test with LINQ
                Assert.IsTrue(rn.OrderBy(rni => rni).SequenceEqual(rn));
            }
        }
    }
}
