using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Langauge.Task
{
	public class MyTask
	{
		#region Task Continue with
		// #task #ContinueWith #TaskContinuationOptions
		public static void TestContinueWith()
		{
			Func<int> ConstFunctionEight = () => 8;
			Func<object, int> ConstFunction = (n) => Convert.ToInt32(n);

			//Task<int> myTask = new Task<int>(ConstFunctionEight);
			Task<int> myTask = new Task<int>(ConstFunction, 8);

			Func<Task<int>, int> MultiplyByTwo = (previous) => previous.Result * 2;
			Task<int> myTask2 = myTask.ContinueWith<int>(MultiplyByTwo, TaskContinuationOptions.OnlyOnRanToCompletion);

			myTask.Start();
			myTask.Wait();
			myTask2.Wait();

			int result = myTask2.Result;
			Assert.AreEqual(result, 16);

		}
		#endregion

		#region Child Tasks
		// #action #TaskCreationOptions #AttachedToParent
		public static void TestChildTask()
		{
			Action sleepAction = () => { System.Threading.Thread.Sleep(2000); };
			Action parentAction1 = () =>
			{
				System.Threading.Tasks.Task childTask = new System.Threading.Tasks.Task(sleepAction, TaskCreationOptions.AttachedToParent);
				childTask.Start();
			};

			System.Threading.Tasks.Task parentTask1 = new System.Threading.Tasks.Task(parentAction1);

			Console.WriteLine("Start child task attached to parent " + DateTime.Now.ToString());
			parentTask1.Start();
			parentTask1.Wait();
			Console.WriteLine("Start child task attached to parent " + DateTime.Now.ToString());

			Action parentAction2 = () =>
			{
				System.Threading.Tasks.Task childTask = new System.Threading.Tasks.Task(sleepAction);
				childTask.Start();
			};

			System.Threading.Tasks.Task parentTask2 = new System.Threading.Tasks.Task(parentAction2);

			Console.WriteLine("Start child task unattached to parent " + DateTime.Now.ToString());
			parentTask2.Start();
			parentTask2.Wait();
			Console.WriteLine("Start child task unattached to parent " + DateTime.Now.ToString());

		}

		#endregion

		#region thread saftey violation
		// #thread safe
		// make an array that holds the values 0 to 50000000

		static long sharedTotalThreadSafetyViolation;
		static int[] itemsThreadSafetyViolation = Enumerable.Range(0, 50000001).ToArray();
		static void addRangeOfValuesThreadSafeViolation(int start, int end)
		{
			while (start < end)
			{
				sharedTotalThreadSafetyViolation = sharedTotalThreadSafetyViolation + itemsThreadSafetyViolation[start];
				start++;
			}
		}

		public static void Task_ThreadSafetyViolation()
		{
			List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();

			for (int i = 0; i < 3; i++)
			{
				int rangeSize = 1000;
				int rangeStart = 0;

				sharedTotalThreadSafetyViolation = 0;
				while (rangeStart < itemsThreadSafetyViolation.Length)
				{
					int rangeEnd = rangeStart + rangeSize;

					if (rangeEnd > itemsThreadSafetyViolation.Length)
						rangeEnd = itemsThreadSafetyViolation.Length;

					// create local copies of the parameters
					int rs = rangeStart;
					int re = rangeEnd;

					tasks.Add(System.Threading.Tasks.Task.Run(() => addRangeOfValuesThreadSafeViolation(rs, re)));
					rangeStart = rangeEnd;
				}

				System.Threading.Tasks.Task.WaitAll(tasks.ToArray());

				Console.WriteLine("{0}.Run The total is: {1}", i, sharedTotalThreadSafetyViolation);
			}
			
		}
		#endregion

		#region lock statement
		//#lock #task #waitall
		static long sharedTotal;

		// make an array that holds the values 0 to 50000000
		static int[] itemsObjectLock = Enumerable.Range(0, 50000001).ToArray();

		static object sharedTotalLock = new object();

		static void addRangeOfValuesObjectLock(int start, int end)
		{
			long subTotal = 0;

			while (start < end)
			{
				subTotal = subTotal + itemsObjectLock[start];
				start++;
			}
			lock (sharedTotalLock)
			{
				sharedTotal = sharedTotal + subTotal;
			}
		}

		/// <summary>
		/// show usage of synchronisation with lock statement on an object
		/// add number 0 to 50000000 with 500 threads 
		/// </summary>
		public static void TestTaskObjectLock()
		{
			List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();

			int rangeSize = 1000000;
			int rangeStart = 0;

			while (rangeStart < itemsObjectLock.Length)
			{
				int rangeEnd = rangeStart + rangeSize;

				if (rangeEnd > itemsObjectLock.Length)
					rangeEnd = itemsObjectLock.Length;

				// create local copies of the parameters
				int rs = rangeStart;
				int re = rangeEnd;

				tasks.Add(System.Threading.Tasks.Task.Run(() => addRangeOfValuesObjectLock(rs, re)));
				rangeStart = rangeEnd;
			}

			System.Threading.Tasks.Task.WaitAll(tasks.ToArray());

			Console.WriteLine("The total is: {0}", sharedTotal);
		}
		#endregion

		#region Monitor
		//#Monitor #task #waitall
		static long sharedTotalMonitor;

		// make an array that holds the values 0 to 50000000
		static int[] itemsMonitor = Enumerable.Range(0, 50000001).ToArray();

		static object sharedTotalMonitorLock = new object();

		static void addRangeOfValuesMonitor(int start, int end)
		{
			long subTotal = 0;
			Random random = new Random();

			while (start < end)
			{
				subTotal = subTotal + itemsMonitor[start];
				start++;
			}

			bool done = false;
			while (!done)
			{
				if (Monitor.TryEnter(sharedTotalMonitorLock))
				{
					sharedTotalMonitor = sharedTotalMonitor + subTotal;
					System.Threading.Thread.Sleep(random.Next(0, 10));
					Console.WriteLine($"{System.Threading.Tasks.Task.CurrentId} : Adding subtotal ");
					Monitor.Exit(sharedTotalMonitorLock);
					done = true;
				}
				else
				{
					Console.WriteLine($"{System.Threading.Tasks.Task.CurrentId} : Have to wait for adding subtotal");
					System.Threading.Thread.Sleep(random.Next(190, 210));
				}
			}
		}

		/// <summary>
		/// show usage of synchronisation with lock statement on an object
		/// add number 0 to 50000000 with 25 threads 
		/// </summary>
		public static void TestTaskMonitor()
		{
			List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();

			int rangeSize = 2000000;
			int rangeStart = 0;

			while (rangeStart < itemsMonitor.Length)
			{
				int rangeEnd = rangeStart + rangeSize;

				if (rangeEnd > itemsMonitor.Length)
					rangeEnd = itemsMonitor.Length;

				// create local copies of the parameters
				int rs = rangeStart;
				int re = rangeEnd;

				tasks.Add(System.Threading.Tasks.Task.Run(() => addRangeOfValuesMonitor(rs, re)));
				rangeStart = rangeEnd;
			}

			System.Threading.Tasks.Task.WaitAll(tasks.ToArray());

			Console.WriteLine("The total is: {0}", sharedTotalMonitor);
		}
		#endregion

		#region Interlocked Operation
		//#Interlocked Poeration #task #waitall
		static long sharedTotalInterlocked;

		// make an array that holds the values 0 to 50000000
		static int[] itemsInterlocked = Enumerable.Range(0, 50000001).ToArray();

		static void addRangeOfValuesInterlocked(int start, int end)
		{
			long subTotal = 0;
			Random random = new Random();

			while (start < end)
			{
				subTotal = subTotal + itemsInterlocked[start];
				start++;
			}

			Console.WriteLine($"{System.Threading.Tasks.Task.CurrentId} : Before adding subtotal ");
			Interlocked.Add(ref sharedTotalInterlocked, subTotal);
			Console.WriteLine($"{System.Threading.Tasks.Task.CurrentId} : After adding subtotal ");
		}

		/// <summary>
		/// show usage of synchronisation with lock statement on an object
		/// add number 0 to 50000000 with 25 threads 
		/// </summary>
		public static void TestTaskInterlocked()
		{
			List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();

			int rangeSize = 2000000;
			int rangeStart = 0;

			while (rangeStart < itemsInterlocked.Length)
			{
				int rangeEnd = rangeStart + rangeSize;

				if (rangeEnd > itemsInterlocked.Length)
					rangeEnd = itemsInterlocked.Length;

				// create local copies of the parameters
				int rs = rangeStart;
				int re = rangeEnd;

				tasks.Add(System.Threading.Tasks.Task.Run(() => addRangeOfValuesInterlocked(rs, re)));
				rangeStart = rangeEnd;
			}

			System.Threading.Tasks.Task.WaitAll(tasks.ToArray());

			Console.WriteLine("The total is: {0}", sharedTotalInterlocked);
		}
		#endregion

		#region Async_await
		/// #async #awit

		private static int DoSomethingAsync()
		{
			Console.WriteLine("Something async started");
			System.Threading.Thread.Sleep(1000);
			Console.WriteLine("Something async finished");

			return 1;
		}

		private static async void PerformSomethingAsync()
		{
			Console.WriteLine("Perfom Something async started");
			int result = await (Task<int>.Run(DoSomethingAsync));
			Console.WriteLine("Perfom Something async finished");
		}

		public static void Test_AsyncAwait()
		{
			PerformSomethingAsync();
			for (int i = 0; i < 10; i++)
			{
				System.Threading.Thread.Sleep(200);
				Console.WriteLine($"Test_AsyncAwait:{i}");
			}

		}
		#endregion

		#region Async_await_exception
		/// #async #awit #exception
		private static int RaiseException()
		{
			Console.WriteLine("Raise Exception started");
			System.Threading.Thread.Sleep(1000);
			throw new Exception("Peng");
			//Console.WriteLine("Raise Exception finished");
			//return 1;
		}
		private static async void PerformException()
		{
			try
			{
				Console.WriteLine("Perform Exception started");
				int result = await (Task<int>.Run(RaiseException));
				Console.WriteLine("Perform Exception finished");
			}
			catch (Exception e)
			{
				Console.WriteLine("Perform Exception caught exception:" + e.Message);
			}
		}

		public static void Test_AsyncAwaitException()
		{
			PerformException();
			for (int i = 0; i < 10; i++)
			{
				System.Threading.Thread.Sleep(200);
				Console.WriteLine($"Test_AsyncAwaitException:{i}");
			}
		}

		#endregion

		#region Async_await_many
		/// #async #await #WhenAll
		private static Func<int> DoSomethingAsyncParallel(string input, int delay)
		{
			return
				() =>
				{
					Console.WriteLine($"Do Something Async Parallel started {input}");
					System.Threading.Thread.Sleep(delay);
					Console.WriteLine($"Do Something Async Parallel finished {input}");

					return 1;
				};
		}

		private static async void PerformSomethingAsyncParallel()
		{
			var tasks = new List<Task<int>>();
			Console.WriteLine("Perform Something Async Parallel started");
			Dictionary<string, int> d = new Dictionary<string, int>();
			d.Add("A", 300);
			d.Add("B", 200);
			d.Add("C", 100);

			foreach (KeyValuePair<string, int> k in d.ToList())
			{
				tasks.Add(Task<int>.Run(DoSomethingAsyncParallel(k.Key, k.Value)));
			}
			await System.Threading.Tasks.Task.WhenAll(tasks);
			Console.WriteLine("Perform Something Async Parallel finished");
		}

		public static void Test_AsyncAwaitWhenAll()
		{
			PerformSomethingAsyncParallel();
			for (int i = 0; i < 10; i++)
			{
				System.Threading.Thread.Sleep(100);
				Console.WriteLine($"Test_AsyncAwait:{i}");
			}

		}
		#endregion

		#region blocking collection
		public static void Test_BlockingCollection()
		{
			// #BlockingCollection #whenAll #wait
			Console.WriteLine("Test_BlockingCollection start");

			var tasks = new List<System.Threading.Tasks.Task>();
			// Blocking collection that can hold 5 items
			BlockingCollection<int> data = new BlockingCollection<int>(5);

			System.Threading.Tasks.Task producer = new System.Threading.Tasks.Task(() =>
			{
				System.Threading.Thread.CurrentThread.Name = "Producer";
				// attempt to add 10 items to the collection - blocks after 5th
				for (int i = 0; i < 10; i++)
				{
					System.Threading.Thread.Sleep(10);
					data.Add(i);
					Console.WriteLine("Data {0} added successfully.", i);
				}
				// indicate we have no more to add
				data.CompleteAdding();
			});

			System.Threading.Tasks.Task consumer = new System.Threading.Tasks.Task(() =>
		   {
			   int v;
			   System.Threading.Thread.CurrentThread.Name = "Consumer";
			   while (!data.IsCompleted)
			   {
				   try
				   {
					   System.Threading.Thread.Sleep(20);
					   v = data.Take();
					   Console.WriteLine("Data {0} taken successfully.", v);
				   }
				   catch (InvalidOperationException) { }
			   }
		   });
			tasks.Add(producer);
			tasks.Add(consumer);

			producer.Start();
			consumer.Start();

			System.Threading.Tasks.Task.WhenAll(tasks).Wait();

			Console.WriteLine("Test_BlockingCollection end");

		}

		#endregion

		#region ConcurrentStack
		public static void Test_ConcurrentStack()
		{
			// #BlockingCollection #whenAll #wait #ConcurrentStack
			Console.WriteLine("Test_ConcurrentStack start");

			var tasks = new List<System.Threading.Tasks.Task>();
			// Blocking collection(ConcurrentStack) that can hold 5 items
			BlockingCollection<int> data = new BlockingCollection<int>(new ConcurrentStack<int>(), 5);

			System.Threading.Tasks.Task producer = new System.Threading.Tasks.Task(() =>
			{
				System.Threading.Thread.CurrentThread.Name = "Producer";
				// attempt to add 10 items to the collection - blocks after 5th
				for (int i = 0; i < 10; i++)
				{
					System.Threading.Thread.Sleep(10);
					data.Add(i);
					Console.WriteLine("Data {0} added successfully.", i);
				}
				// indicate we have no more to add
				data.CompleteAdding();
			});

			System.Threading.Tasks.Task consumer = new System.Threading.Tasks.Task(() =>
			{
				int v;
				System.Threading.Thread.CurrentThread.Name = "Consumer";
				while (!data.IsCompleted)
				{
					try
					{
						System.Threading.Thread.Sleep(20);
						v = data.Take();
						Console.WriteLine("Data {0} taken successfully.", v);
					}
					catch (InvalidOperationException) { }
				}
			});
			tasks.Add(producer);
			tasks.Add(consumer);

			producer.Start();
			consumer.Start();

			System.Threading.Tasks.Task.WhenAll(tasks).Wait();

			Console.WriteLine("Test_ConcurrentStack end");

		}
		#endregion

		#region ConcurentQueue
		// #ConcurentQueue
		public static void Test_ConccurentQueue()
		{
			Console.WriteLine("Test_ConccurentQueue start");

			var tasks = new List<System.Threading.Tasks.Task>();
			// 
			ConcurrentQueue<int> conccurentQueue = new ConcurrentQueue<int>();

			System.Threading.Tasks.Task producer = new System.Threading.Tasks.Task(() =>
			{
				Random random = new Random();
				System.Threading.Thread.CurrentThread.Name = "Producer";
				// attempt to add 10 items to the collection - blocks after 5th
				for (int i = 0; i < 10; i++)
				{
					System.Threading.Thread.Sleep(random.Next(0, 10));
					conccurentQueue.Enqueue(i);
					Console.WriteLine($"Data {i} enqueued successfully.");
				}

			});

			System.Threading.Tasks.Task consumer = new System.Threading.Tasks.Task(() =>
			{
				int v;
				System.Threading.Thread.CurrentThread.Name = "Consumer";
				int i = 0;
				while (i < 10)
				{
					while (!conccurentQueue.TryPeek(out v))
					{
						Console.WriteLine($"Try to peek failed");
						System.Threading.Thread.Sleep(5);
					}

					if (conccurentQueue.TryDequeue(out v))
					{
						Console.WriteLine($"Data {v} dequeued successfully.");
					}

					i++;
				}
			});

			tasks.Add(producer);
			tasks.Add(consumer);

			producer.Start();
			consumer.Start();

			System.Threading.Tasks.Task.WhenAll(tasks).Wait();

			Console.WriteLine("Test_ConccurentQueue end");
		}
		#endregion

		#region ConcurrentDictionary
		// #Concurrent ConcurrentDictionary
		public static void Test_ConcurrentDictionary()
		{
			Console.WriteLine("Test_ConcurrentDictionary start");

			ConcurrentDictionary<int, string> dicitionary = new ConcurrentDictionary<int, string>();

			for (int i = 1; i < 10; i++)
				dicitionary.TryAdd(i, "A");

			var tasks = new List<System.Threading.Tasks.Task>();
			foreach (string t in new List<string> { "Cosumer1", "Consumer2" })
			{
				tasks.Add(new System.Threading.Tasks.Task(
					() =>
					{
						Random random = new Random();
						for (int i = 1; i < 10; i++)
						{
							if (dicitionary.TryUpdate(i, "B", "A"))
							{
								Console.WriteLine($"{t} updated {i}");
							}
							System.Threading.Thread.Sleep(random.Next(0, 10));
						}
					}
					));
			}

			tasks[0].Start();
			tasks[1].Start();

			System.Threading.Tasks.Task.WhenAll(tasks).Wait();

			Console.WriteLine("Test_ConcurrentDictionary end");
		}
		#endregion

		#region deadlock
		// #deadlock
		static bool done1 = false;
		static bool done2 = false;
		static object lock1 = new object();
		static object lock2 = new object();

		static void Method1()
		{
			lock (lock1)
			{
				Console.WriteLine("Method 1 got lock 1");
				System.Threading.Thread.Sleep(500);
				Console.WriteLine("Method 1 waiting for lock 2");
				lock (lock2)
				{
					done1 = true;
					Console.WriteLine("Method 1 got lock 2");
				}
				Console.WriteLine("Method 1 released lock 2");
			}
			Console.WriteLine("Method 1 released lock 1");
		}

		static void Method2()
		{
			lock (lock2)
			{
				Console.WriteLine("Method 2 got lock 2");
				System.Threading.Thread.Sleep(500);
				Console.WriteLine("Method 2 waiting for lock 1");
				lock (lock1)
				{
					done2 = true;
					Console.WriteLine("Method 2 got lock 1");
				}
				Console.WriteLine("Method 2 released lock 1");
			}
			Console.WriteLine("Method 2 released lock 2");
		}

		public static void TaskDeadLock()
		{
			List<System.Threading.Tasks.Task> allTasks = new List<System.Threading.Tasks.Task>();
			allTasks.Add(System.Threading.Tasks.Task.Run(() => Method1()));
			allTasks.Add(System.Threading.Tasks.Task.Run(() => Method2()));
			Console.WriteLine("waiting for tasks");

			System.Threading.Tasks.Task.WhenAll(allTasks).Wait(2000);

			Assert.IsFalse(done1);
			Assert.IsFalse(done2);

			Console.WriteLine("Finished Deadlock");
		}

		#endregion

		#region volatile
		// #volatile prevents variable from optimisation
		//volatile int volatileInt=0;
		#endregion

		#region cancellation
		// #CancellationToken
		static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

		static void Clock()
		{
			while (!cancellationTokenSource.IsCancellationRequested)
			{
				Console.WriteLine("Tick");
				System.Threading.Thread.Sleep(500);
			}
		}

		public static void Task_Cancellation()
		{
			System.Threading.Tasks.Task.Run(() => Clock());
			Console.WriteLine("Cancel clock after random time");
			Random random = new Random();
			System.Threading.Thread.Sleep(random.Next(1000, 3000));
			cancellationTokenSource.Cancel();
			Console.WriteLine("Clock stopped");

		}
		#endregion

		#region OperationCanceledException
		static void Clock(CancellationToken cancellationToken)
		{
			int tickCount = 0;

			while (!cancellationToken.IsCancellationRequested &&
				   tickCount < 5)
			{
				tickCount++;
				Console.WriteLine("Tick");
				System.Threading.Thread.Sleep(500);
			}

			cancellationToken.ThrowIfCancellationRequested();
		}

		public static void Task_OperationCanceledException()
		{
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

			System.Threading.Tasks.Task clock = System.Threading.Tasks.Task.Run(() => Clock(cancellationTokenSource.Token));

			System.Threading.Thread.Sleep(500);
			try
			{
				cancellationTokenSource.Cancel();
				clock.Wait();
			}
			catch (AggregateException ex)
			{
				Console.WriteLine("Clock stopped: {0}", ex.InnerExceptions[0].GetType().ToString());
			}

		}
		#endregion
	}
}
