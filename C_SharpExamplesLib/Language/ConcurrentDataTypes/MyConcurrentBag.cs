using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language.ConcurrentDataTypes
{
    [SuppressMessage("ReSharper", "ConvertToLocalFunction")]
    [SuppressMessage("ReSharper", "ConvertClosureToMethodGroup")]
    public class MyConcurrentBag
    {
        #region #ConcurrentBag #concurrent list
        // #Parallel access to #list(bag)  #ConcurrentBag #concurrent list
        public static void ParallelInsert()
        {
            ConcurrentBag<int> bag = new ConcurrentBag<int>();
            Random random = new Random();

            DateTime start = DateTime.Now;
            var items = Enumerable.Range(0, 500);
            foreach (var item in items)
            {
                System.Threading.Thread.Sleep(random.Next(5, 15));
                if (random.Next(0, 100) < 50)
                    bag.Add(item);
            }

            TimeSpan t1 = DateTime.Now.Subtract(start);

            start = DateTime.Now;
            items = Enumerable.Range(0, 500);
            Parallel.ForEach(items, parallelItem => // also works with Parallel.For(0, 500, ...
            {
                System.Threading.Thread.Sleep(random.Next(5, 15));
                if (random.Next(0, 100) < 50)
                    bag.Add(parallelItem);
            });
            TimeSpan t2 = DateTime.Now.Subtract(start);

            Console.WriteLine("Non parallel:" + t1);
            Console.WriteLine("Parallel:" + t2);

            Assert.IsTrue(t2 < t1);

        }
        #endregion
    }
}
