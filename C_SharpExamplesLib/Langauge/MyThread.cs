using System;
using System.Threading;

namespace C_Sharp
{
	public partial class MyThread
	{
		#region private threads
		private static Semaphore Bouncer { get; set; }
		private static void Guest(object args)
		{
			// Wait to enter the nightclub (a semaphore to be released).
			Console.WriteLine("Guest {0} is waiting to entering nightclub.", args);
			Bouncer.WaitOne();

			// Do some dancing.
			Console.WriteLine("Guest {0} is doing some dancing.", args);
			Thread.Sleep(500);

			// Let one guest out (release one semaphore).
			Console.WriteLine("Guest {0} is leaving the nightclub.", args);
			Bouncer.Release(1);
		}
		private static void OpenNightclub()
		{
			for (int i = 1; i <= 50; i++)
			{
				// Let each guest enter on an own thread.
				Thread thread = new Thread(new ParameterizedThreadStart(Guest));
				thread.Start(i);
			}
		}

		public static void SemaphoreExample()
		{
			// Create the semaphore with 3 slots, where 3 are available.
			Bouncer = new Semaphore(3, 3);

			// Open the nightclub.
			OpenNightclub();
		}

		#endregion

		#region threadlocal
		/// <summary>
		/// both threads work on thread local data
		/// </summary>
		public static ThreadLocal<Random> RandomGenerator =
		new ThreadLocal<Random>(() =>
		{
			return new Random(2);
		});

		public static ThreadLocal<int> ThreadInt =
			new ThreadLocal<int>();

		public static void TestThreadLocalData()
		{
			Thread t1 = new Thread(() =>
			{
				for (int i = 0; i < 5; i++)
				{
					ThreadInt.Value = 5;
					
					Console.WriteLine("Thread 1: {0} {1}", RandomGenerator.Value.Next(10), ThreadInt.Value );
					Thread.Sleep(500);
				}
			});

			Thread t2 = new Thread(() =>
			{
				for (int i = 0; i < 5; i++)
				{
					Console.WriteLine("Thread 2: {0} {1}", RandomGenerator.Value.Next(10), ThreadInt.Value);
					Thread.Sleep(500);
				}
			});

			t1.Start();
			t2.Start();

			//Console.ReadKey();
			Thread.Sleep(5000);
		}
		#endregion

		#region threadstatic
		// #thread #threadStaticData #threadPool

		// A static field marked with ThreadStaticAttribute is not shared between threads.
		// Each executing thread has a separate instance of the field with random value
		[ThreadStatic]
		private static int ThreadStaticLocalState;

		// common field for all threads : might fail if threads are fast
		private static int LocalState;

		// common field for all threads : will be treated by atomic operation
		private static int AtomicLocalState;

		// common field for all threads : will be treated by semaphore
		private static Semaphore Semaphore;
		private static int SemphoreProtectedLocalState;

		private static void DoWork(object state)
		{
			Console.WriteLine("Doing work: {0}", state);
			
			LocalState += 1;
			ThreadStaticLocalState += 1;
			Interlocked.Increment(ref AtomicLocalState);

			Semaphore.WaitOne();
			Console.WriteLine("Entering");
			SemphoreProtectedLocalState += 1;
			Console.WriteLine("Leaving");
			Semaphore.Release();

			Console.WriteLine($"Local: {LocalState} AtomicLocal: {AtomicLocalState} ThreadStatic Local : {ThreadStaticLocalState} Semaphore Local: {SemphoreProtectedLocalState} ",
				ThreadStaticLocalState, LocalState, AtomicLocalState, SemphoreProtectedLocalState);
			Thread.Sleep(500);
			Console.WriteLine("Work finished: {0}", state);
		}

		private static void WaitForThreads()
		{
			int timeOutSeconds = 10;

			//Now wait until all threads from the Threadpool have returned
			while (timeOutSeconds > 0)
			{
				//figure out what the max worker thread count it
				ThreadPool.GetMaxThreads(out int maxThreads, out _);
				ThreadPool.GetAvailableThreads(out int availThreads, out _);

				if (availThreads == maxThreads) 
					break;
				// Sleep
				System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(1000));
				--timeOutSeconds;
			}
			// You can add logic here to log timeouts
		}

		public static void TestThreadStaticData()
		{
			ThreadStaticLocalState = 0;
			LocalState = 0;
			AtomicLocalState = 0;
			SemphoreProtectedLocalState = 0;
			Semaphore = new Semaphore(1, 1);
			ThreadPool.SetMaxThreads(4, 4);

			for (int i = 0; i < 50; i++)
			{
				int stateNumber = i;
				ThreadPool.QueueUserWorkItem(state => DoWork(stateNumber));
			}

			WaitForThreads();
			string result = string.Format($"Local: {LocalState} AtomicLocal: {AtomicLocalState} ThreadStatic Local : {ThreadStaticLocalState} Semaphore Local: {SemphoreProtectedLocalState} ",
				ThreadStaticLocalState, LocalState, AtomicLocalState, SemphoreProtectedLocalState);
			Console.WriteLine(result);
		}

		#endregion

		#region simple thread
		// #thread
		private static void SimpleThreadHello()
		{
			Console.WriteLine("Hello from the thread");
			Thread.Sleep(2000);
		}

		public static void ThreadSimple()
		{
			Thread thread = new Thread(SimpleThreadHello);
			thread.Start();

			ThreadStart ts = new ThreadStart(SimpleThreadHello);
			thread = new Thread(ts);
			thread.Start();
		}
		#endregion

		#region ParameterizedThreadStart
		// #thread #ParameterizedThreadStart
		private static void WorkOnData(object data)
		{
			Console.WriteLine("Working on: {0}", data);
			Thread.Sleep(1000);
		}

		public static void TestParameterizedThreadStart()
		{
			ParameterizedThreadStart ps = new ParameterizedThreadStart(WorkOnData);

			Thread thread = new Thread(ps);

			thread.Start(99);
		}

		public static void TestLambdaThreadWithData()
		{
			Thread myThread = new Thread((data) => { Console.WriteLine( $"Hello {data}!", data); Thread.Sleep(1000); });
			myThread.Start("outer space");
		}
		#endregion

		#region thread abort
		// #thread #abort

		public static void Thread_Abort()
		{
			Thread tickThread = new Thread(() =>
			{
				int i = 0;
				while (true)
				{
					Console.WriteLine("Tick" + i++.ToString());
					Thread.Sleep(1000);
				}
			});

			tickThread.Start();
			Thread.Sleep(3500);
			tickThread.Abort();
		}
		#endregion

		#region thread join
		public static void Thread_Join()
		{
			Thread thread1 = new Thread(() =>
			{
				int i = 0;
				while (i < 10)
				{
					Console.WriteLine("Thread 1 Tick " + i++.ToString());
					Thread.Sleep(1000);
				}
			});

			Thread thread2 = new Thread(() =>
			{
				int i = 0;
				while (i < 3)
				{
					Console.WriteLine("Thread 2 Tick " + i++.ToString());
					Thread.Sleep(1000);
				}

				Console.WriteLine("Thread 2 actual finished");
				thread1.Join();
				Console.WriteLine("Thread 2 has performed join with thread 1");
			});

			thread1.Start();
			thread2.Start();
			thread2.Join();
		}
		#endregion

		#region thread administrative data
		// #thread #administrative data
		static void DisplayThread(Thread t)
		{
			Console.WriteLine("Name: {0}", t.Name);
			Console.WriteLine("Culture: {0}", t.CurrentCulture);
			Console.WriteLine("Priority: {0}", t.Priority);
			Console.WriteLine("Context: {0}", t.ExecutionContext);
			Console.WriteLine("IsBackground?: {0}", t.IsBackground);
			Console.WriteLine("IsPool?: {0}", t.IsThreadPoolThread);
		}

		public static void Thread_AdministrativeData()
        {
			Thread.CurrentThread.Name = "Heinz";
			DisplayThread(Thread.CurrentThread);
		}

		#endregion
	}
}
