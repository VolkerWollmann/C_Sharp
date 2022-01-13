using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language.Task
{
	[SuppressMessage("ReSharper", "ConvertToLocalFunction")]
    [SuppressMessage("ReSharper", "ConvertClosureToMethodGroup")]
    public class MyTask
	{
		#region Task Continue with
		// #task #ContinueWith #TaskContinuationOptions
		public static void TestContinueWith()
		{
			Func<int> constFunctionEight = () => 8;
			Func<object, int> constFunction = (n) => Convert.ToInt32(n);

			//Task<int> myTask = new Task<int>(ConstFunctionEight);
            int eight = constFunctionEight();
			Task<int> myTask = new Task<int>(constFunction, eight);

			Func<Task<int>, int> multiplyByTwo = (previous) => previous.Result * 2;
			Task<int> myTask2 = myTask.ContinueWith(multiplyByTwo, TaskContinuationOptions.OnlyOnRanToCompletion);

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

			Console.WriteLine("Start child task attached to parent " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
			parentTask1.Start();
			parentTask1.Wait();
			Console.WriteLine("Start child task attached to parent " + DateTime.Now.ToString(CultureInfo.InvariantCulture));

			Action parentAction2 = () =>
			{
				System.Threading.Tasks.Task childTask = new System.Threading.Tasks.Task(sleepAction);
				childTask.Start();
			};

			System.Threading.Tasks.Task parentTask2 = new System.Threading.Tasks.Task(parentAction2);

			Console.WriteLine("Start child task unattached to parent " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
			parentTask2.Start();
			parentTask2.Wait();
			Console.WriteLine("Start child task unattached to parent " + DateTime.Now.ToString(CultureInfo.InvariantCulture));

		}

		#endregion

		#region thread saftey violation
		// #thread safe
		// make an array that holds the values 0 to 50000000

		static long _sharedTotalThreadSafetyViolation;
		static readonly int[] ItemsThreadSafetyViolation = Enumerable.Range(0, 50000001).ToArray();
		static void AddRangeOfValuesThreadSafeViolation(int start, int end)
		{
			while (start < end)
			{
				_sharedTotalThreadSafetyViolation = _sharedTotalThreadSafetyViolation + ItemsThreadSafetyViolation[start];
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

				_sharedTotalThreadSafetyViolation = 0;
				while (rangeStart < ItemsThreadSafetyViolation.Length)
				{
					int rangeEnd = rangeStart + rangeSize;

					if (rangeEnd > ItemsThreadSafetyViolation.Length)
						rangeEnd = ItemsThreadSafetyViolation.Length;

					// create local copies of the parameters
					int rs = rangeStart;
					int re = rangeEnd;

					tasks.Add(System.Threading.Tasks.Task.Run(() => AddRangeOfValuesThreadSafeViolation(rs, re)));
					rangeStart = rangeEnd;
				}

				System.Threading.Tasks.Task.WaitAll(tasks.ToArray());

				Console.WriteLine("{0}.Run The total is: {1}", i, _sharedTotalThreadSafetyViolation);
			}
			
		}
		#endregion

		

		#region Monitor
		//#Monitor #task #waitall
		static long _sharedTotalMonitor;

		// make an array that holds the values 0 to 50000000
		static readonly int[] ItemsMonitor = Enumerable.Range(0, 50000001).ToArray();

		static readonly object SharedTotalMonitorLock = new object();

		static void AddRangeOfValuesMonitor(int start, int end)
		{
			long subTotal = 0;
			Random random = new Random();

			while (start < end)
			{
				subTotal = subTotal + ItemsMonitor[start];
				start++;
			}

			bool done = false;
			while (!done)
			{
				if (Monitor.TryEnter(SharedTotalMonitorLock))
				{
					_sharedTotalMonitor = _sharedTotalMonitor + subTotal;
					System.Threading.Thread.Sleep(random.Next(0, 10));
					Console.WriteLine($"{System.Threading.Tasks.Task.CurrentId} : Adding subtotal ");
					Monitor.Exit(SharedTotalMonitorLock);
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
		/// show usage of synchronization with lock statement on an object
		/// add number 0 to 50000000 with 25 threads 
		/// </summary>
		public static void TestTaskMonitor()
		{
			List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();

			int rangeSize = 2000000;
			int rangeStart = 0;

			while (rangeStart < ItemsMonitor.Length)
			{
				int rangeEnd = rangeStart + rangeSize;

				if (rangeEnd > ItemsMonitor.Length)
					rangeEnd = ItemsMonitor.Length;

				// create local copies of the parameters
				int rs = rangeStart;
				int re = rangeEnd;

				tasks.Add(System.Threading.Tasks.Task.Run(() => AddRangeOfValuesMonitor(rs, re)));
				rangeStart = rangeEnd;
			}

			System.Threading.Tasks.Task.WaitAll(tasks.ToArray());

			Console.WriteLine("The total is: {0}", _sharedTotalMonitor);
		}
		#endregion

		#region Interlocked Operation
		//#Interlocked operation #task #waitall
		static long _sharedTotalInterlocked;

		// make an array that holds the values 0 to 50000000
		static readonly int[] ItemsInterlocked = Enumerable.Range(0, 50000001).ToArray();

		static void AddRangeOfValuesInterlocked(int start, int end)
		{
			long subTotal = 0;

            while (start < end)
			{
				subTotal = subTotal + ItemsInterlocked[start];
				start++;
			}

			Console.WriteLine($"{System.Threading.Tasks.Task.CurrentId} : Before adding subtotal ");
			Interlocked.Add(ref _sharedTotalInterlocked, subTotal);
			Console.WriteLine($"{System.Threading.Tasks.Task.CurrentId} : After adding subtotal ");
		}

		/// <summary>
		/// show usage of synchronization with lock statement on an object
		/// add number 0 to 50000000 with 25 threads 
		/// </summary>
		public static void TestTaskInterlocked()
		{
			List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();

			int rangeSize = 2000000;
			int rangeStart = 0;

			while (rangeStart < ItemsInterlocked.Length)
			{
				int rangeEnd = rangeStart + rangeSize;

				if (rangeEnd > ItemsInterlocked.Length)
					rangeEnd = ItemsInterlocked.Length;

				// create local copies of the parameters
				int rs = rangeStart;
				int re = rangeEnd;

				tasks.Add(System.Threading.Tasks.Task.Run(() => AddRangeOfValuesInterlocked(rs, re)));
				rangeStart = rangeEnd;
			}

			System.Threading.Tasks.Task.WaitAll(tasks.ToArray());

			Console.WriteLine("The total is: {0}", _sharedTotalInterlocked);
		}
		#endregion

		#region Async_await
		/// #async #await

		private static int DoSomethingAsync()
		{
			Console.WriteLine("Something async started");
			System.Threading.Thread.Sleep(1000);
			Console.WriteLine("Something async finished");

			return 1;
		}

		private static async void PerformSomethingAsync()
		{
			Console.WriteLine("Perform Something async started");
			int result = await (System.Threading.Tasks.Task.Run(DoSomethingAsync));
			Assert.AreEqual(result,1);
			Console.WriteLine("Perform Something async finished");
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
		/// #async #await #exception
		private static int RaiseException()
		{
			Console.WriteLine("Raise Exception started");
			System.Threading.Thread.Sleep(1000);
			throw new Exception("Bang");
			//Console.WriteLine("Raise Exception finished");
			//return 1;
		}
		private static async void PerformException()
		{
			try
			{
				Console.WriteLine("Perform Exception started");
				int result = await (System.Threading.Tasks.Task.Run(RaiseException));
				Assert.AreEqual(result, 2, "This assert must not occur");
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
            Dictionary<string, int> d = new Dictionary<string, int> {{"A", 300}, {"B", 200}, {"C", 100}};

            foreach (KeyValuePair<string, int> k in d.ToList())
			{
				tasks.Add(System.Threading.Tasks.Task.Run(DoSomethingAsyncParallel(k.Key, k.Value)));
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
               System.Threading.Thread.CurrentThread.Name = "Consumer";
			   while (!data.IsCompleted)
			   {
				   try
				   {
					   System.Threading.Thread.Sleep(20);
					   var v = data.Take();
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

		#region deadlock
		// #deadlock
		static bool _done1;  // by default false
		static bool _done2;
		static readonly object Lock1 = new object();
		static readonly object Lock2 = new object();

		static void Method1()
		{
			lock (Lock1)
			{
				Console.WriteLine("Method 1 got lock 1");
				System.Threading.Thread.Sleep(500);
				Console.WriteLine("Method 1 waiting for lock 2");
				lock (Lock2)
				{
					_done1 = true;
					Console.WriteLine("Method 1 got lock 2");
				}
				Console.WriteLine("Method 1 released lock 2");
			}
			Console.WriteLine("Method 1 released lock 1");
		}

		static void Method2()
		{
			lock (Lock2)
			{
				Console.WriteLine("Method 2 got lock 2");
				System.Threading.Thread.Sleep(500);
				Console.WriteLine("Method 2 waiting for lock 1");
				lock (Lock1)
				{
					_done2 = true;
					Console.WriteLine("Method 2 got lock 1");
				}
				Console.WriteLine("Method 2 released lock 1");
			}
			Console.WriteLine("Method 2 released lock 2");
		}

		public static void TaskDeadLock()
		{
            List<System.Threading.Tasks.Task> allTasks = new List<System.Threading.Tasks.Task>
            {
                System.Threading.Tasks.Task.Run(Method1), System.Threading.Tasks.Task.Run(Method2)
            };
            Console.WriteLine("waiting for tasks");

			System.Threading.Tasks.Task.WhenAll(allTasks).Wait(2000);

			Assert.IsFalse(_done1);
			Assert.IsFalse(_done2);

			Console.WriteLine("Finished Deadlock");
		}

		#endregion

		#region volatile
		// #volatile prevents variable from optimization
		//volatile int volatileInt=0;
		#endregion

		#region cancellation
		// #CancellationToken
		static readonly CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();

		static void Clock()
		{
			while (!CancellationTokenSource.IsCancellationRequested)
			{
				Console.WriteLine("Tick");
				System.Threading.Thread.Sleep(500);
			}
		}

		public static void Task_Cancellation()
		{
			System.Threading.Tasks.Task.Run(Clock);
			Console.WriteLine("Cancel clock after random time");
			Random random = new Random();
			System.Threading.Thread.Sleep(random.Next(1000, 3000));
			CancellationTokenSource.Cancel();
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
			CancellationTokenSource localCancellationTokenSource = new CancellationTokenSource();

			System.Threading.Tasks.Task clock = System.Threading.Tasks.Task.Run(() => Clock(localCancellationTokenSource.Token), localCancellationTokenSource.Token);

			System.Threading.Thread.Sleep(500);
			try
			{
				localCancellationTokenSource.Cancel();
                // ReSharper disable once MethodSupportsCancellation
                clock.Wait();
			}
			catch (AggregateException ex)
			{
				Console.WriteLine("Clock stopped: {0}", ex.InnerExceptions[0].GetType());
			}

		}
		#endregion
	}
}
