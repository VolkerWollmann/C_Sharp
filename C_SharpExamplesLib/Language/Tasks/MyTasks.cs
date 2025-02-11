using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language.Tasks
{
	[SuppressMessage("ReSharper", "ConvertToLocalFunction")]
    [SuppressMessage("ReSharper", "ConvertClosureToMethodGroup")]
    public abstract class MyTasks
	{
		#region Task Continue with
		// #task #ContinueWith #TaskContinuationOptions
		public static void TestContinueWith()
		{
			Func<int> constFunctionEight = () => 8;
			Func<object, int> constFunction = (n) => Convert.ToInt32(n);

			//Task<int> myTask = new Task<int>(ConstFunctionEight);
            int eight = constFunctionEight();
			Task<int> myTask = new Task<int>(constFunction!, eight);

			Func<Task<int>, int> multiplyByTwo = (previous) => previous.Result * 2;
			Task<int> myTask2 = myTask.ContinueWith(multiplyByTwo, TaskContinuationOptions.OnlyOnRanToCompletion);

			myTask.Start();
			myTask.Wait();
			myTask2.Wait();

			int result = myTask2.Result;
			Assert.AreEqual(16, result);

        }
        #endregion

        public static void ConstantTaskResult()
        {
            var fast16 = Task.FromResult(16);
			
			if (!fast16.IsCompleted) // actually always true
                fast16.Start();
            fast16.Wait();
			
			Assert.AreEqual(16, fast16.Result);
        }
		

        #region Child Tasks
        // #action #TaskCreationOptions #AttachedToParent
        public static void TestChildTask()
		{
			Action sleepAction = () => { Thread.Sleep(2000); };
			Action parentAction1 = () =>
			{
				Task childTask = new Task(sleepAction, TaskCreationOptions.AttachedToParent);
				childTask.Start();
			};

			Task parentTask1 = new Task(parentAction1);

			Console.WriteLine("Start child task attached to parent " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
			parentTask1.Start();
			parentTask1.Wait();
			Console.WriteLine("Start child task attached to parent " + DateTime.Now.ToString(CultureInfo.InvariantCulture));

			Action parentAction2 = () =>
			{
				Task childTask = new Task(sleepAction);
				childTask.Start();
			};

			Task parentTask2 = new Task(parentAction2);

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
			List<Task> tasks = [];

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

					tasks.Add(Task.Run(() => AddRangeOfValuesThreadSafeViolation(rs, re)));
					rangeStart = rangeEnd;
				}

				Task.WaitAll(tasks.ToArray());

				Console.WriteLine("{0}.Run The total is: {1}", i, _sharedTotalThreadSafetyViolation);
			}
			
		}
		#endregion

		#region Monitor
		//#Monitor #task #waitall
		static long _sharedTotalMonitor;

		// make an array that holds the values 0 to 50000000
		static readonly int[] ItemsMonitor = Enumerable.Range(0, 50000001).ToArray();

		static readonly object SharedTotalMonitorLock = new();

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
					Thread.Sleep(random.Next(0, 10));
					Console.WriteLine($"{Task.CurrentId} : Adding subtotal ");
					Monitor.Exit(SharedTotalMonitorLock);
					done = true;
				}
				else
				{
					Console.WriteLine($"{Task.CurrentId} : Have to wait for adding subtotal");
					Thread.Sleep(random.Next(190, 210));
				}
			}
		}

		/// <summary>
		/// show usage of synchronization with lock statement on an object
		/// add number 0 to 50000000 with 25 threads 
		/// </summary>
		public static void TestTaskMonitor()
		{
			List<Task> tasks = [];

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

				tasks.Add(Task.Run(() => AddRangeOfValuesMonitor(rs, re)));
				rangeStart = rangeEnd;
			}

			Task.WaitAll(tasks.ToArray());

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

			Console.WriteLine($"{Task.CurrentId} : Before adding subtotal ");
			Interlocked.Add(ref _sharedTotalInterlocked, subTotal);
			Console.WriteLine($"{Task.CurrentId} : After adding subtotal ");
		}

		/// <summary>
		/// show usage of synchronization with lock statement on an object
		/// add number 0 to 50000000 with 25 threads 
		/// </summary>
		public static void TestTaskInterlocked()
		{
			List<Task> tasks = [];

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

				tasks.Add(Task.Run(() => AddRangeOfValuesInterlocked(rs, re)));
				rangeStart = rangeEnd;
			}

			Task.WaitAll(tasks.ToArray());

			Console.WriteLine("The total is: {0}", _sharedTotalInterlocked);
		}
		#endregion

		#region Async_await
		/// #async #await

		private static int DoSomethingAsync()
		{
			Console.WriteLine("Something async started");
			Thread.Sleep(1000);
			Console.WriteLine("Something async finished");

			return 1;
		}

		private static async Task PerformSomethingAsync()
		{
			Console.WriteLine("Perform Something async started");
			int result = await (Task.Run(DoSomethingAsync));
			Assert.AreEqual(1, result);
			Console.WriteLine("Perform Something async finished");
		}

		public static void Test_AsyncAwait()
		{
			_ = PerformSomethingAsync();
			for (int i = 0; i < 10; i++)
			{
				Thread.Sleep(200);
				Console.WriteLine($"Test_AsyncAwait:{i}");
			}

		}
		#endregion

		#region Async_await_exception
		/// #async #await #exception
		private static int RaiseException()
		{
			Console.WriteLine("Raise Exception started");
			Thread.Sleep(1000);
			throw new Exception("Bang");
			//Console.WriteLine("Raise Exception finished");
			//return 1;
		}
		private static async void PerformException()
		{
			try
			{
				Console.WriteLine("Perform Exception started");
				int result = await (Task.Run(RaiseException));
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
				Thread.Sleep(200);
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
					Thread.Sleep(delay);
					Console.WriteLine($"Do Something Async Parallel finished {input}");

					return 1;
				};
		}

		private static async Task PerformSomethingAsyncParallel()
		{
			var tasks = new List<Task<int>>();
			Console.WriteLine("Perform Something Async Parallel started");
            Dictionary<string, int> d = new Dictionary<string, int> {{"A", 300}, {"B", 200}, {"C", 100}};

            foreach (KeyValuePair<string, int> k in d.ToList())
			{
				tasks.Add(Task.Run(DoSomethingAsyncParallel(k.Key, k.Value)));
			}
			await Task.WhenAll(tasks);
			Console.WriteLine("Perform Something Async Parallel finished");
		}

		public static void Test_AsyncAwaitWhenAll()
		{
			_ = PerformSomethingAsyncParallel();
			for (int i = 0; i < 10; i++)
			{
				Thread.Sleep(100);
				Console.WriteLine($"Test_AsyncAwait:{i}");
			}

		}
		#endregion

		#region blocking collection
		public static void Test_BlockingCollection()
		{
			// #BlockingCollection #whenAll #wait
			Console.WriteLine("Test_BlockingCollection start");

			var tasks = new List<Task>();
			// Blocking collection that can hold 5 items
			BlockingCollection<int> data = new BlockingCollection<int>(5);

			Task producer = new Task(() =>
			{
				Thread.CurrentThread.Name = "Producer";
				// attempt to add 10 items to the collection - blocks after 5th
				for (int i = 0; i < 10; i++)
				{
					Thread.Sleep(10);
					data.Add(i);
					Console.WriteLine("Data {0} added successfully.", i);
				}
				// indicate we have no more to add
				data.CompleteAdding();
			});

			Task consumer = new Task(() =>
		   {
               Thread.CurrentThread.Name = "Consumer";
			   while (!data.IsCompleted)
			   {
				   try
				   {
					   Thread.Sleep(20);
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

			Task.WhenAll(tasks).Wait();

			Console.WriteLine("Test_BlockingCollection end");

		}

		#endregion

		#region deadlock
		// #deadlock
		static bool _done1;  // by default false
		static bool _done2;
		static readonly object Lock1 = new();
		static readonly object Lock2 = new();

		static void Method1()
		{
			lock (Lock1)
			{
				Console.WriteLine("Method 1 got lock 1");
				Thread.Sleep(500);
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
				Thread.Sleep(500);
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
            List<Task> allTasks = [Task.Run(Method1), Task.Run(Method2)];
            Console.WriteLine("waiting for tasks");

			Task.WhenAll(allTasks).Wait(2000);

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
		static readonly CancellationTokenSource CancellationTokenSource = new();

		static void Clock()
		{
			while (!CancellationTokenSource.IsCancellationRequested)
			{
				Console.WriteLine("Tick");
				Thread.Sleep(500);
			}
		}

		public static void Task_Cancellation()
		{
			Task.Run(Clock);
			Console.WriteLine("Cancel clock after random time");
			Random random = new Random();
			Thread.Sleep(random.Next(1000, 3000));
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
				Thread.Sleep(500);
			}

			cancellationToken.ThrowIfCancellationRequested();
		}

		public static void Task_OperationCanceledException()
		{
			CancellationTokenSource localCancellationTokenSource = new CancellationTokenSource();

			Task clock = Task.Run(() => Clock(localCancellationTokenSource.Token), localCancellationTokenSource.Token);

			Thread.Sleep(500);
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

        #region Scheduler
        
        #region Time
        private static DateTime GetNextStartDateTime()
        {
	        DateTime now = DateTime.Now;

            DateTime start = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);

            var ts = now.Second > 50 ? new TimeSpan(0, 1, 0) : 
	            new TimeSpan(0, 0, ((now.Second / 10) + 1) * 10);

            start += ts;

            return start;
        }

        private static DateTime IncrementStartTime(DateTime start, int seconds)
        {
            TimeSpan ts = TimeSpan.FromSeconds(seconds);
            DateTime newStartTime = start;

            newStartTime = newStartTime.Add(ts);
            while (newStartTime < DateTime.Now)
                newStartTime = newStartTime.Add(ts);

            return newStartTime;
        }

        private static void WaitUntil(string label, DateTime next)
        {
            int calculatedWaitTime = (int)((next - DateTime.Now).TotalMilliseconds + 1);
            Thread.Sleep(calculatedWaitTime);
            Console.WriteLine(label + DateTime.Now.ToString("hh:mm:ss.fff"));
        }

        private static void WaitForTask(Action action)
        {
            List<Task> tasks = [Task.Run(action)];
            Task.WaitAll(tasks.ToArray());
        }
        #endregion

        private static void SchedulerWork(object? data)
        {
			int? taskId = Task.CurrentId;
            Thread thread = Thread.CurrentThread;
			
			Console.WriteLine("Scheduler Work Start:" + DateTime.Now.ToString("hh:mm:ss.fff") + " Task:" + taskId + " Thread:" + thread.ManagedThreadId);
            Random random = new Random();
            int workTime = random.Next(1, 50);
            Thread.Sleep(workTime);
            Console.WriteLine("Scheduler Work End  :" + DateTime.Now.ToString("hh:mm:ss.fff"));
        }

        private static void SchedulerInfiniteLoop()
        {
            DateTime start = GetNextStartDateTime();
           
            while (true)
            {
                WaitUntil("Start:", start);

                WaitForTask(()=>
                {
                    SchedulerWork(null);
                });

                start = IncrementStartTime(start, 10);

            }
            // ReSharper disable once FunctionNeverReturns
        }

        private static void SchedulerStart(object data)
        {
            WaitForTask(() =>
            {
                SchedulerWork(null);
            });
        }

        public static void Task_SchedulerTest_AsInfiniteLoop()
        {
            Task.Run(() => { SchedulerInfiniteLoop(); });

            Thread.Sleep(30 * 1000);
        }

        public static void Task_SchedulerTest_Timer()
        {
            DateTime start = GetNextStartDateTime();
            WaitUntil("Start:", start);
            Timer unused = new Timer(SchedulerWork, null, 0, 10000);
            Thread.Sleep(30 * 1000);
        }

        public static void Task_SchedulerTest_Timer_Task()
        {
            DateTime start = GetNextStartDateTime();
            WaitUntil("Start:", start);
            Timer unused = new Timer(SchedulerStart!, null, 0, 10000);
            Thread.Sleep(30 * 1000);
        }
		
        #endregion
    }
}
