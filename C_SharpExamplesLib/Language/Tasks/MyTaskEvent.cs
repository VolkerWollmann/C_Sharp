using System.Collections.Concurrent;

namespace C_SharpExamplesLib.Language.Tasks
{
	/// <summary>
	/// #task #event
	/// </summary>
	public class MyTaskEvent
	{
		static readonly Random Random = new();

		public delegate void ProduceEventHandler(int e, bool final);

		private class Consumer
        {
            readonly BlockingCollection<int> _data = new();

			public void AddData(int i, bool final)
			{
				_data.Add(i);
				if (final)
					_data.CompleteAdding();
			}

			public void Run()
			{
				Thread.CurrentThread.Name = "Consumer";
				while (!_data.IsCompleted)
				{
					try
					{
						Thread.Sleep(Random.Next(100, 200));
						int v = _data.Take();
						Console.WriteLine("Data {0} taken successfully.", v);
					}
					catch (InvalidOperationException) { }
				}
			}
		}

		private class Producer
		{
			static event ProduceEventHandler? ProduceEvent;
			public Producer(ProduceEventHandler produceEventHandler)
			{
				ProduceEvent += produceEventHandler;
			}

			public void Run()
			{ 
				Thread.CurrentThread.Name = "Producer";

				int max = 10;

				for (int i = 0; i < max; i++)
				{
					Thread.Sleep(Random.Next(100, 150));
                    ProduceEvent?.Invoke(i, (i == (max - 1)));
                    Console.WriteLine("Data {0} produced successfully.", i);
				}
			}

		}
		public static void TaskEvent()
        {
			Consumer consumer = new Consumer();
			Producer producer = new Producer(consumer.AddData);

			var tasks = new List<Task>();
			
			Task producerTask = new Task(() => producer.Run());
			Task consumerTask = new Task(() => consumer.Run());

			tasks.Add(producerTask);
			tasks.Add(consumerTask);

			producerTask.Start();
			consumerTask.Start();

			Task.WhenAll(tasks).Wait();
			Console.WriteLine("Test_BlockingCollection end");
		}
	}
}
