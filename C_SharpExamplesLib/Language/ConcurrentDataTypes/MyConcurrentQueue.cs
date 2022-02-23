using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace C_Sharp.Language.ConcurrentDataTypes
{
    [SuppressMessage("ReSharper", "ConvertToLocalFunction")]
    [SuppressMessage("ReSharper", "ConvertClosureToMethodGroup")]
    public class MyConcurrentQueue
    {
        #region ConcurentQueue
        // #ConcurrentQueue
        public static void Test_ConcurrentQueue()
        {
            Console.WriteLine("Test_ConcurrentQueue start");

            var tasks = new List<System.Threading.Tasks.Task>();
            // 
            ConcurrentQueue<int> concurrentQueue = new ConcurrentQueue<int>();

            System.Threading.Tasks.Task producer = new System.Threading.Tasks.Task(() =>
            {
                Random random = new Random();
                System.Threading.Thread.CurrentThread.Name = "Producer";
                // attempt to add 10 items to the collection - blocks after 5th
                for (int i = 0; i < 10; i++)
                {
                    System.Threading.Thread.Sleep(random.Next(0, 10));
                    concurrentQueue.Enqueue(i);
                    Console.WriteLine($"Data {i} queued successfully.");
                }

            });

            System.Threading.Tasks.Task consumer = new System.Threading.Tasks.Task(() =>
            {
                System.Threading.Thread.CurrentThread.Name = "Consumer";
                int i = 0;
                while (i < 10)
                {
                    int v;
                    while (!concurrentQueue.TryPeek(out v))
                    {
                        Console.WriteLine("Try to peek failed");
                        System.Threading.Thread.Sleep(5);
                    }

                    if (concurrentQueue.TryDequeue(out v))
                    {
                        Console.WriteLine($"Data {v} dequeued successfully.");
                    }

                    i++;
                }
            });

            tasks.Add(producer);
            tasks.Add(consumer);

            producer.Start();
            consumer.Start();

            System.Threading.Tasks.Task.WhenAll(tasks).Wait();

            Console.WriteLine("Test_ConcurrentQueue end");
        }
        #endregion
	}
}
