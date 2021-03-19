using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace C_Sharp
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
				Thread.CurrentThread.Name = "Consumer";
				while (!data.IsCompleted)
				{
					try
					{
						Thread.Sleep(random.Next(100, 200));
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
				Thread.CurrentThread.Name = "Producer";

				int max = 10;

				for (int i = 0; i < max; i++)
				{
					Thread.Sleep(random.Next(100, 150)); ;
					ProduceEvent(i, (i==(max-1)));
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
