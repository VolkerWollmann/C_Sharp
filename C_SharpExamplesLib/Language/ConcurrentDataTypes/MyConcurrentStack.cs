using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace C_SharpExamplesLib.Language.ConcurrentDataTypes
{
    [SuppressMessage("ReSharper", "ConvertToLocalFunction")]
    [SuppressMessage("ReSharper", "ConvertClosureToMethodGroup")]
    public class MyConcurrentStack
    {
        #region ConcurrentStack
        public static void Test_ConcurrentStack()
        {
            // #BlockingCollection #whenAll #wait #ConcurrentStack
            Console.WriteLine("Test_ConcurrentStack start");

            var tasks = new List<Task>();
            // Blocking collection(ConcurrentStack) that can hold 5 items
            BlockingCollection<int> data = new BlockingCollection<int>(new ConcurrentStack<int>(), 5);

            Task producer = new Task(() =>
            {
                Thread.CurrentThread.Name = "Producer";
                // attempt to add 10 items to the collection - blocks after 5th
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(10);
                    data.Add(i);
                    Console.WriteLine("Data {0} added successfully.", i);
                }
                // indicate we have no more to add
                data.CompleteAdding();
            });

            Task consumer = new Task(() =>
            {
                Thread.CurrentThread.Name = "Consumer";
                while (!data.IsCompleted)
                {
                    try
                    {
                        Thread.Sleep(20);
                        var v = data.Take();
                        Console.WriteLine("Data {0} taken successfully.", v);
                    }
                    catch (InvalidOperationException) { }
                }
            });
            tasks.Add(producer);
            tasks.Add(consumer);

            producer.Start();
            consumer.Start();

            Task.WhenAll(tasks).Wait();

            Console.WriteLine("Test_ConcurrentStack end");

        }
        #endregion
    }
}
