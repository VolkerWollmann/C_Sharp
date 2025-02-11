using System.Collections.Concurrent;
namespace C_SharpExamplesLib.Language.ConcurrentDataTypes
{
	public abstract class MyBlockingCollection
	{

		#region BlockingCollection

		// #ConcurrentQueue
		public static void BlockingCollection()
		{
			// Create a BlockingCollection with a bounded capacity of 5
			BlockingCollection<int> collection = new BlockingCollection<int>(5);
			// Producer Task
			Task producer = Task.Run(() =>
			{
				for (int i = 0; i < 10; i++)
				{
					collection.Add(i);
					Console.WriteLine($"Produced: {i}");
					Thread.Sleep(100); // Simulate work
				}

				collection.CompleteAdding(); // Mark as complete
			});
			// Consumer Task
			Task consumer = Task.Run(() =>
			{
				foreach (var item in collection.GetConsumingEnumerable())
				{
					Console.WriteLine($"Consumed: {item}");
					Thread.Sleep(200); // Simulate work
				}
			});
			Task.WaitAll(producer, consumer);
		}

		#endregion
	}
}
