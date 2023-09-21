using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CSharpCore
{
    
    public class CSharp
    {
        #region stream
        // #Stream
        public static async void StreamTestUnconventionalUsage()
        {
            string[] lines = { "First line", "Second line", "Third line" };
            using StreamWriter fileWriter = new("WriteLines2.txt"); // works without brackets in C# 8.0

            foreach (string line in lines)
            {
                if (!line.Contains("Second"))
                {
                    await fileWriter.WriteLineAsync(line);
                }
            }

            fileWriter.Close();
            fileWriter.Close();    // rendundant, bit does not hurt

            using StreamReader fileReader = new("WriteLines2.txt");
            while (!fileReader.EndOfStream)
            {
                string line = fileReader.ReadLine();
                Assert.IsTrue(lines.Contains(line));
            }
        }

        // #stream
        public static async void StreamTestFlush()
        {
            using MemoryStream p2MemoryStream = new MemoryStream();
            // open p2File
            var sw = new StreamWriter(p2MemoryStream);

            sw.WriteLine("Esel");
            sw.WriteLine("Hund");
            sw.Flush();
            p2MemoryStream.Position = 0;

            StreamReader streamReader = new StreamReader(p2MemoryStream);
            string s = streamReader.ReadToEnd();

            Assert.IsTrue(s.Contains("Hund"));
        }

        #endregion

        #region CountDownEventTest
        public static async void CountDownEventTest()
        {
            // Initialize a queue and a CountdownEvent
            ConcurrentQueue<int> queue = new ConcurrentQueue<int>(Enumerable.Range(0, 10000));
            CountdownEvent cde = new CountdownEvent(10000); // initial count = 10000

            // This is the logic for all queue consumers
            Action consumer = () =>
            {
                int local;
                // decrement CDE count once for each element consumed from queue
                while (queue.TryDequeue(out local)) cde.Signal();
            };

            // Now empty the queue with a couple of asynchronous tasks
            Task t1 = Task.Factory.StartNew(consumer);
            Task t2 = Task.Factory.StartNew(consumer);

            // And wait for queue to empty by waiting on cde
            cde.Wait(); // will return when cde count reaches 0

            Assert.AreEqual(0, cde.CurrentCount);

            // Proper form is to wait for the tasks to complete, even if you know that their work
            // is done already.
            await Task.WhenAll(t1, t2);

            // Resetting will cause the CountdownEvent to un-set, and resets InitialCount/CurrentCount
            // to the specified value
            cde.Reset(10);

            // AddCount will affect the CurrentCount, but not the InitialCount
            cde.AddCount(2);

            int path=0;
            // Now try waiting with cancellation
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.Cancel(); // cancels the CancellationTokenSource
            try
            {
                cde.Wait(cts.Token);
                path = 1;
            }
            catch (OperationCanceledException)
            {
                path = 2;
            }
            finally
            {
                cts.Dispose();
            }
            // It's good to release a CountdownEvent when you're done with it.
            cde.Dispose();

            Assert.AreEqual(2, path);
        }
        #endregion
    }
}
