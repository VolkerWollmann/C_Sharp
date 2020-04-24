using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace C_Sharp
{
	public class MyTask
	{
		public static void TestContinueWith()
		{
			Func<int> ConstFunctionEight = () => 8;
			Func<object, int> ConstFunction = (n) => Convert.ToInt32(n);

			//Task<int> myTask = new Task<int>(ConstFunctionEight);
			Task<int> myTask = new Task<int>(ConstFunction,8);

			Func<Task<int>,int> MultiplyByTwo = (previous) => previous.Result * 2;
			Task<int> myTask2 = myTask.ContinueWith<int>(MultiplyByTwo);

			myTask.Start();
			myTask.Wait();
			myTask2.Wait();

			int result = myTask2.Result;

		}

		#region lock statement
		static long sharedTotal;

		// make an array that holds the values 0 to 5000000
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
	}
}
