using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace C_Sharp.Language.ConcurrentDataTypes
{
    [SuppressMessage("ReSharper", "ConvertToLocalFunction")]
    [SuppressMessage("ReSharper", "ConvertClosureToMethodGroup")]
    public class MyLock
    {
        #region lock statement

        private static int size = 20000001; // 50000001

        public class SimpleTotal
        {
            static long _sharedTotal;

            static readonly int[] ItemsToAdd = Enumerable.Range(0, size).ToArray();

            static readonly object SharedTotalLock = new object();

            static void AddAllInOneThread()
            {
                _sharedTotal = 0;
                foreach (var t in ItemsToAdd)
                {
                    _sharedTotal = _sharedTotal + t;
                }
            }

            static void AddOneThread(int index)
            {
                lock (SharedTotalLock)
                {
                    _sharedTotal = _sharedTotal + ItemsToAdd[index];
                }
            }


            static void AddMultiThreads()
            {
                List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();
                int i = 0;
                _sharedTotal = 0;
                while (i < ItemsToAdd.Length)
                {
                    int j = i;
                    tasks.Add(System.Threading.Tasks.Task.Run(() => AddOneThread(j)));
                    i++;
                }

                System.Threading.Tasks.Task.WaitAll(tasks.ToArray());

            }

            public static void TestSimpleTotal()
            {
                DateTime start = DateTime.Now;
                AddAllInOneThread();
                TimeSpan timeSpan = DateTime.Now.Subtract(start);
                Console.WriteLine("Simple loop          : {0} in: {1,10:000.00000} ", _sharedTotal, timeSpan.TotalSeconds);

                start = DateTime.Now;
                AddMultiThreads();
                timeSpan = DateTime.Now.Subtract(start);
                Console.WriteLine("Values added parallel: {0} in: {1,10:000.00000} ", _sharedTotal, timeSpan.TotalSeconds);
            }
        }

        public class SharedTotal
        {
            //#lock #task #waitall
            static long _sharedTotal;

            // make an array that holds the values 0 to 50000000
            static readonly int[] ItemsToAdd = Enumerable.Range(0, size).ToArray();

            static readonly object SharedTotalLock = new object();

            static void AddRangeOfValuesObjectLock(int start, int end)
            {
                long subTotal = 0;

                while (start < end)
                {
                    subTotal = subTotal + ItemsToAdd[start];
                    start++;
                }

                lock (SharedTotalLock)
                {
                    _sharedTotal = _sharedTotal + subTotal;
                }
            }

            /// <summary>
            /// show usage of synchronization with lock statement on an object
            /// add number 0 to 50000000 with 500 threads 
            /// </summary>
            public static void TestTaskObjectLock()
            {
                List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();

                int rangeSize = 1000000;
                int rangeStart = 0;

                DateTime start = DateTime.Now;
                while (rangeStart < ItemsToAdd.Length)
                {
                    int rangeEnd = rangeStart + rangeSize;

                    if (rangeEnd > ItemsToAdd.Length)
                        rangeEnd = ItemsToAdd.Length;

                    // create local copies of the parameters
                    int rs = rangeStart;
                    int re = rangeEnd;

                    tasks.Add(System.Threading.Tasks.Task.Run(() => AddRangeOfValuesObjectLock(rs, re)));
                    rangeStart = rangeEnd;
                }

                System.Threading.Tasks.Task.WaitAll(tasks.ToArray());

                TimeSpan timeSpan = DateTime.Now.Subtract(start);

                Console.WriteLine("Ranges added parallel: {0} in: {1,10:000.00000} ", _sharedTotal, timeSpan.TotalSeconds);
            }
        }

        #endregion
    }
}
