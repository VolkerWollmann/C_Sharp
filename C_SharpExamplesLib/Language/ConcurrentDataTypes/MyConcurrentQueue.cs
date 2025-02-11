using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace C_SharpExamplesLib.Language.ConcurrentDataTypes
{
    [SuppressMessage("ReSharper", "ConvertToLocalFunction")]
    [SuppressMessage("ReSharper", "ConvertClosureToMethodGroup")]
    public abstract class MyConcurrentQueue
    {
        #region ConcurentQueue
        // #ConcurrentQueue
        public static void Test_ConcurrentQueue()
        {
            Console.WriteLine("Test_ConcurrentQueue start");

            var tasks = new List<Task>();
            // 
            ConcurrentQueue<int> concurrentQueue = new ConcurrentQueue<int>();

            Task producer = new Task(() =>
            {
                Random random = new Random();
                Thread.CurrentThread.Name = "Producer";
                // attempt to add 10 items to the collection - blocks after 5th
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(random.Next(0, 10));
                    concurrentQueue.Enqueue(i);
                    Console.WriteLine($"Data {i} queued successfully.");
                }

            });

            Task consumer = new Task(() =>
            {
                Thread.CurrentThread.Name = "Consumer";
                int i = 0;
                while (i < 10)
                {
                    int v;
                    while (!concurrentQueue.TryPeek(out v))
                    {
                        Console.WriteLine("Try to peek failed");
                        Thread.Sleep(5);
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

            Task.WhenAll(tasks).Wait();

            Console.WriteLine("Test_ConcurrentQueue end");
        }
        #endregion
    }
}
