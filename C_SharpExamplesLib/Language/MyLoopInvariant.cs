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
            int i = 0;
            while(i <= n)
            {
				// Loop invariant: The maximum element found so far is stored in 'max'.
				Assert.IsTrue(rn.Take(i).All(rni => rni <= max))
					;
				if (rn[i] > max)
                    max = rn[i];

                i++;
                
                // Loop invariant: The maximum element found so far is stored in 'max'.
                Assert.IsTrue(rn.Take(i).All( rni => rni <= max));
            }

            // Loop invariant: The maximum element found is stored in 'max'.
	        Assert.IsTrue( i == n + 1 );
            Assert.IsTrue(rn.Take(n).All(rni => rni <= max));
        }
        public static void LoopInvariant2()
        {
            
            int[] rn = InitArray();
            int n = rn.Length - 1;

            int i = 0;
            while( i < n)
            {
                // Outer loop invariant:
                for (int k = 0; k < i-1; k++) { Assert.IsTrue(rn[k] < rn[k+1]); }

                int j = n;
                while(j > i)
                {
                    if (rn[j-1] > rn[j])
                    {
                        // Swap elements
                        (rn[j], rn[j -1]) = (rn[j-1], rn[j]);
                    }

                    j--;
                    // Inner loop invariant
                    Assert.IsTrue(rn[j] < rn[j+1]);

                }

                // Inner loop invariant
                Assert.IsTrue(j == i);
                Assert.IsTrue(rn[i] < rn[i + 1]);

				// Outer loop invariant:
				for (int k = 0; k < i ; k++) { Assert.IsTrue(rn[k] < rn[k + 1]); }

				i++;

				// Outer loop invariant:
				for (int k = 0; k < i - 1; k++) { Assert.IsTrue(rn[k] < rn[k + 1]); }

			}

            // Outer loop invariant: the list is sorted
            Assert.IsTrue( i == n);
			for (int k = 0; k < n - 1; k++) { Assert.IsTrue(rn[k] < rn[k + 1]); }


			{
                // Outer loop invariant: as LINQ expression
                Assert.IsTrue(rn.Zip(rn.Skip(1), (rni, rni1) => rni < rni1).All(x => x));

                // Test with LINQ
                Assert.IsTrue(rn.OrderBy(rni => rni).SequenceEqual(rn));
            }
        }
    }
}
