using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language.ConcurrentDataTypes
{
    [SuppressMessage("ReSharper", "ConvertToLocalFunction")]
    [SuppressMessage("ReSharper", "ConvertClosureToMethodGroup")]
    public class MyConcurrentBag
    {
        #region #ConcurrentBag #concurrent list
        // #Parallel access to #list(bag)  #ConcurrentBag #concurrent list
        public static void ParallelInsert()
        {
            ConcurrentBag<int> bag = [];
            Random random = new Random();

            DateTime start = DateTime.Now;
            var items = Enumerable.Range(0, 500);
            foreach (var item in items)
            {
                Thread.Sleep(random.Next(5, 15));
                if (random.Next(0, 100) < 50)
                    bag.Add(item);
            }

            TimeSpan t1 = DateTime.Now.Subtract(start);

            start = DateTime.Now;
            items = Enumerable.Range(0, 500);
            Parallel.ForEach(items, parallelItem => // also works with Parallel.For(0, 500, ...
            {
                Thread.Sleep(random.Next(5, 15));
                if (random.Next(0, 100) < 50)
                    bag.Add(parallelItem);
            });
            TimeSpan t2 = DateTime.Now.Subtract(start);

            Console.WriteLine("Non parallel:" + t1);
            Console.WriteLine("Parallel:" + t2);

            Assert.IsTrue(bag.Count>0);
            Assert.IsTrue(t2 < t1);

        }
        #endregion
    }
}
