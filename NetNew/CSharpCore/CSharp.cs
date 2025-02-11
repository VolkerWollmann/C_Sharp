using System;
using System.Collections.Concurrent;
using System.Diagnostics;
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
        public static async Task StreamTestUnconventionalUsage()
        {
            string[] lines = { "First line", "Second line", "Third line" };
            await using StreamWriter fileWriter = new("WriteLines2.txt"); // works without brackets in C# 8.0

            foreach (string line in lines)
            {
                if (!line.Contains("Second"))
                {
                    await fileWriter.WriteLineAsync(line);
                }
            }

            fileWriter.Close();
            fileWriter.Close();    // redundant, bit does not hurt

            using StreamReader fileReader = new("WriteLines2.txt");
            while (!fileReader.EndOfStream)
            {
                string line = fileReader.ReadLine();
                Assert.IsTrue(lines.Contains(line));
            }
        }

        // #stream
        public static void StreamTestFlush()
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
        public static void CountDownEventTest()
        {
            // Initialize a queue and a CountdownEvent
            ConcurrentQueue<int> queue = new ConcurrentQueue<int>(Enumerable.Range(0, 10000));
            CountdownEvent cde = new CountdownEvent(10000); // initial count = 10000

            // This is the logic for all queue consumers
            Action consumer = () =>
            {
                int local;
                // decrement CDE count once for each element consumed from queue
                // ReSharper disable once AccessToDisposedClosure
                while (queue.TryDequeue(out local))
                {
                    Assert.IsTrue(local >= 0);
	                cde.Signal();
                }
            };

            // Now empty the queue with a couple of asynchronous tasks
            Task t1 = Task.Factory.StartNew(consumer);
            Task t2 = Task.Factory.StartNew(consumer);

            // And wait for queue to empty by waiting on cde
            cde.Wait(); // will return when cde count reaches 0

            Assert.AreEqual(0, cde.CurrentCount);

            // Proper form is to wait for the tasks to complete, even if you know that their work
            // is done already.
            Task.WaitAll(t1, t2);

            // Resetting will cause the CountdownEvent to un-set, and resets InitialCount/CurrentCount
            // to the specified value
            cde.Reset(10);

            // AddCount will affect the CurrentCount, but not the InitialCount
            cde.AddCount(2);

            int path;
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

        #region DebuggerDisplay

        [DebuggerDisplay("Ship: Name = {Name}, Tonnage = {Tonnage}" )]
        private class Ship
        {
            internal string Name { get; private set; }
            internal int  Tonnage { get; private set; }

            internal Ship(string name, int tonnage)
            {
                Name = name;
                Tonnage = tonnage;
            }
        }

        public static void TestDebuggerDisplay()
        {
            Ship ship = new Ship("SMS rubber boat", 5);
            Assert.IsTrue(ship != null);

            ; // watch in ship in Debugger
        }

        #endregion

        #region floating point numeric types

        public static void TestFloatingPointNumericTypes()
        {
            Console.WriteLine("TestFloatingPointNumericTypes");

            float f = (float) Math.Sqrt(2);
            double d = Math.Sqrt(2);
            decimal dec = 1.4142135623730950488016887242097m;

			Console.WriteLine(f);
			Console.WriteLine(d);
            Console.WriteLine(dec);
        }
        #endregion

        #region #override #virtual #new

        private class BaseClass
        {
	        internal readonly string V1 = "B1";

	        public virtual string V2 => "B2";

	        internal readonly string V3 = "B3";
		}

        private class DerivedClass : BaseClass
        {
			#pragma warning disable CS0108
			internal readonly string V1 = "D1";
			#pragma warning restore CS0108

	        public override string V2 => "D2";

	        internal new readonly string V3 = "D3";
		}

        public static void TestFieldHiding()
        {
            DerivedClass derivedClass = new DerivedClass();
            Assert.AreEqual("D1", derivedClass.V1);
            Assert.AreEqual("D2", derivedClass.V2);
            Assert.AreEqual("D3", derivedClass.V3);
			BaseClass baseClass = derivedClass;
            Assert.AreEqual("B1", baseClass.V1);
            Assert.AreEqual("D2", baseClass.V2);
            Assert.AreEqual("B3", baseClass.V3);
		}

        #endregion
    }
}
