using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace C_SharpExamplesLib.Language.ConcurrentDataTypes
{
    [SuppressMessage("ReSharper", "ConvertToLocalFunction")]
    [SuppressMessage("ReSharper", "ConvertClosureToMethodGroup")]
    public class MyConcurrentDictionary
    {
        #region ConcurrentDictionary
        // #Concurrent ConcurrentDictionary
        public static void Test_ConcurrentDictionary()
        {
            Console.WriteLine("Test_ConcurrentDictionary start");

            ConcurrentDictionary<int, string> dictionary = new ConcurrentDictionary<int, string>();

            for (int i = 1; i < 10; i++)
                dictionary.TryAdd(i, "A");

            var tasks = new List<Task>();
            foreach (string t in new List<string> { "Cosumer1", "Consumer2" })
            {
                tasks.Add(new Task(
                    () =>
                    {
                        Random random = new Random();
                        for (int i = 1; i < 10; i++)
                        {
                            if (dictionary.TryUpdate(i, "B", "A"))
                            {
                                Console.WriteLine($"{t} updated {i}");
                            }
                            Thread.Sleep(random.Next(0, 10));
                        }
                    }
                ));
            }

            tasks[0].Start();
            tasks[1].Start();

            Task.WhenAll(tasks).Wait();

            Console.WriteLine("Test_ConcurrentDictionary end");
        }
        #endregion
	}
}
