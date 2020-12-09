using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp
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
			Task<int> myTask = new Task<int>(ConstFunction,8);

			Func<Task<int>,int> MultiplyByTwo = (previous) => previous.Result * 2;
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
			Action sleepAction = () => { Thread.Sleep(2000); };
			Action parentAction1 = () =>
			{
				Task childTask = new Task(sleepAction, TaskCreationOptions.AttachedToParent);
				childTask.Start();
			};

			Task parentTask1 = new Task( parentAction1 );

			Console.WriteLine("Start child task attached to parent " + DateTime.Now.ToString());
			parentTask1.Start();
			parentTask1.Wait();
			Console.WriteLine("Start child task attached to parent " + DateTime.Now.ToString());

			Action parentAction2 = () =>
			{
				Task childTask = new Task(sleepAction);
				childTask.Start();
			};

			Task parentTask2 = new Task(parentAction2);

			Console.WriteLine("Start child task unattached to parent " + DateTime.Now.ToString());
			parentTask2.Start();
			parentTask2.Wait();
			Console.WriteLine("Start child task unattached to parent " + DateTime.Now.ToString());

		}

		#endregion

		#region lock statement
		//#lock #task #waitall
		static long sharedTotal;

		// make an array that holds the values 0 to 50000000
		static int[] items = Enumerable.Range(0, 50000001).ToArray();

		static object sharedTotalLock = new object();

		static void addRangeOfValues(int start, int end)
		{
			long subTotal = 0;

			while (start < end)
			{
				subTotal = subTotal + items[start];
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
			List<Task> tasks = new List<Task>();

			int rangeSize = 1000000;
			int rangeStart = 0;

			while (rangeStart < items.Length)
			{
				int rangeEnd = rangeStart + rangeSize;

				if (rangeEnd > items.Length)
					rangeEnd = items.Length;

				// create local copies of the parameters
				int rs = rangeStart;
				int re = rangeEnd;

				tasks.Add(Task.Run(() => addRangeOfValues(rs, re)));
				rangeStart = rangeEnd;
			}

			Task.WaitAll(tasks.ToArray());

			Console.WriteLine("The total is: {0}", sharedTotal);
		}
		#endregion

		#region Async_await
		/// #async #awit
		
		private static int DoSomethingAsync()
        {
			Console.WriteLine("Something async started");
			Thread.Sleep(1000);
			Console.WriteLine("Something async finished");

			return 1;
        }

		private static async void PerformSomethingAsync()
		{
			Console.WriteLine("Perfom Something async started");
			int result = await( Task<int>.Run(DoSomethingAsync));
			Console.WriteLine("Perfom Something async finished");
		}

		public static void Test_AsyncAwait()
        {
			PerformSomethingAsync();
			for(int i = 0; i < 10; i++)
            {
				Thread.Sleep(200);
				Console.WriteLine($"Test_AsyncAwait:{i}");
            }

		}
		#endregion

		#region Async_await_exception
		/// #async #awit #exception
		private static int RaiseException()
		{
			Console.WriteLine("Raise Exception started");
			Thread.Sleep(1000);
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
				Thread.Sleep(200);
				Console.WriteLine($"Test_AsyncAwaitException:{i}");
			}
		}

		#endregion

		#region Async_await_many
		/// #async #awit #WhenAll
		private static Func<int> DoSomethingAsyncParallel(string input, int delay)
		{
			Func<int>  a  =
				() =>
				{
					Console.WriteLine($"Do Something Async Parallel started {input}");
					Thread.Sleep(delay);
					Console.WriteLine($"Do Something Async Parallel finished {input}");

					return 1;
				}; 

			return a;
		}

		private static async void PerformSomethingAsyncParallel()
		{
			var tasks = new List<Task<int>>();
			Console.WriteLine("Perform Something Async Parallel started");
			Dictionary<string, int> d = new Dictionary<string, int>();
			d.Add("A", 300);
			d.Add("B", 200);
			d.Add("C", 100);

			foreach( KeyValuePair<string,int> k in d.ToList())
			{
				tasks.Add(Task<int>.Run(DoSomethingAsyncParallel(k.Key, k.Value)));
			}
			await Task.WhenAll(tasks);
			Console.WriteLine("Perform Something Async Parallel finished");
		}

		public static void Test_AsyncAwaitWhenAll()
		{
			PerformSomethingAsyncParallel();
			for (int i = 0; i < 10; i++)
			{
				Thread.Sleep(100);
				Console.WriteLine($"Test_AsyncAwait:{i}");
			}

		}
		#endregion
	}
}
