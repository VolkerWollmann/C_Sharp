﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language.Tasks
{
    public abstract class MyParallel
    {
        #region prime search with parallel

        private static long _maxPrime = 1;
        private static readonly Semaphore PrimeSemaphore = new(1, 1);
        private static void IsPrime(int candidate)
        {
            bool result = true;
            for (int i = 2; i < (candidate / 2) + 1; i++)
            {
                if (candidate % i == 0)
                {
                    result = false;
                }
            }

            if (result)
            {
                PrimeSemaphore.WaitOne();
                if (candidate > _maxPrime)
                    _maxPrime = candidate;
                PrimeSemaphore.Release();
            }
        }

        private static void FindPrimesWithNumberOfTasks(int numTasks)
        {
            int waits = 0;
            DateTime start = DateTime.Now;

            // Define the real grade of parallelism
            ParallelOptions po = new ParallelOptions
            {
                MaxDegreeOfParallelism = numTasks
            };

            int loopEnd = 500000;
            for (int i = 1; i < loopEnd; i = i + numTasks)
            {
                int arraySize = Math.Min(numTasks, loopEnd - i);
                Action[] a = new Action[arraySize];
                for (int j = 0; j < arraySize; j++)
                {
                    int number2 = i + j;
                    a[j] = () => IsPrime(number2);
                }

                Parallel.Invoke(po, a);
            }
            TimeSpan t = DateTime.Now.Subtract(start);
            Console.WriteLine("Time {0} with tasks : {1} Waits:{2} MaxPrime:{3}", t, numTasks, waits, _maxPrime);
        }

        public static void Parallel_GradeOfParallelism()
        {
            FindPrimesWithNumberOfTasks(4);
            FindPrimesWithNumberOfTasks(12);
        }

        #endregion

        #region Parallel.For
        static void WorkOnItem(object item)
        {
            Console.WriteLine("Started working on: " + item + " within thread " +
                              Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(100);
            Console.WriteLine("Finished working on: " + item);
        }

        // #Parallel #foreach #for #ParallelLoopState
        public static void ParallelFor()
        {
            var items = Enumerable.Range(0, 500);
            Parallel.ForEach(items, item => // also works with Parallel.For(0, 500, ...
            {
                WorkOnItem(item);
            });

            Console.WriteLine("----");

            var itemsArray = Enumerable.Range(0, 500).ToArray();
            ParallelLoopResult result = Parallel.For(0, itemsArray.Length, (i, loopState) =>
            {
                // break : all lambda expressions below 200 are completed, 
                // stop  : lambda expressions below 200 might be killed.
                if (i == 200)
                    loopState.Break();

                WorkOnItem(itemsArray[i]);
            });

            Assert.IsFalse(result.IsCompleted);

        }

        #endregion
    }
}
