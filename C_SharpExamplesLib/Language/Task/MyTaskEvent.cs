using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace C_Sharp.Langauge.Task
{
	/// <summary>
	/// #task #event
	/// </summary>
	public class MyTaskEvent
	{
		static Random random = new Random();

		public delegate void ProduceEventHandler(int e, bool final);

		private class Consumer
        {
			BlockingCollection<int> data = new BlockingCollection<int>();

			public void AddData(int i, bool final)
			{
				data.Add(i);
				if (final)
					data.CompleteAdding();
			}

			public void Run()
			{
				System.Threading.Thread.CurrentThread.Name = "Consumer";
				while (!data.IsCompleted)
				{
					try
					{
						System.Threading.Thread.Sleep(random.Next(100, 200));
						int v = data.Take();
						Console.WriteLine("Data {0} taken successfully.", v);
					}
					catch (InvalidOperationException) { }
				}
			}
		}

		private class Producer
		{
			static event ProduceEventHandler ProduceEvent;
			public Producer(ProduceEventHandler produceEventHandler)
			{
				ProduceEvent += produceEventHandler;
			}

			public void Run()
			{ 
				System.Threading.Thread.CurrentThread.Name = "Producer";

				int max = 10;

				for (int i = 0; i < max; i++)
				{
					System.Threading.Thread.Sleep(random.Next(100, 150)); ;
					ProduceEvent(i, (i==(max-1)));
					Console.WriteLine("Data {0} produced successfully.", i);
				}
			}

		}
		public static void TaskEvent()
        {
			Consumer consumer = new Consumer();
			Producer producer = new Producer(consumer.AddData);

			var tasks = new List<System.Threading.Tasks.Task>();
			
			System.Threading.Tasks.Task producerTask = new System.Threading.Tasks.Task(() => producer.Run());
			System.Threading.Tasks.Task consumerTask = new System.Threading.Tasks.Task(() => consumer.Run());

			tasks.Add(producerTask);
			tasks.Add(consumerTask);

			producerTask.Start();
			consumerTask.Start();

			System.Threading.Tasks.Task.WhenAll(tasks).Wait();
			Console.WriteLine("Test_BlockingCollection end");
		}
	}
}
