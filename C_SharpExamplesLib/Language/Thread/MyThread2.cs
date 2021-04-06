using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language.Thread
{
	/// <summary>
	/// #partial class
	/// </summary>
	public partial class MyThread
	{

		private static void Method()
		{
			System.Threading.Thread.Sleep(100);
			Console.WriteLine("Method in thread " + System.Threading.Thread.CurrentThread.ManagedThreadId); 
			return;
		}

		static void Task1()
		{
			Console.WriteLine("Task 1 starting in thread " + System.Threading.Thread.CurrentThread.ManagedThreadId);
			System.Threading.Thread.Sleep(2000);
			Console.WriteLine("Task 1 ending");
		}

		static void Task2()
		{
			Console.WriteLine("Task 2 starting in thread " + System.Threading.Thread.CurrentThread.ManagedThreadId);
			System.Threading.Thread.Sleep(1000);
			Console.WriteLine("Task 2 ending");
		}

		// #Invoke #Parallel #Dispatcher
		public static void Thread_Dispatcher()
		{
			Dispatcher.CurrentDispatcher.Invoke(new Action(() => { MyThread.Method(); }));
			Console.WriteLine("After asynchronous start of method within thread " + System.Threading.Thread.CurrentThread.ManagedThreadId);

			Parallel.Invoke(() => Task1(), () => Task2());
			Console.WriteLine("Finished processing within thread " + System.Threading.Thread.CurrentThread.ManagedThreadId);
		}

		static void WorkOnItem(object item)
		{
			Console.WriteLine("Started working on: " + item + " within thread " + System.Threading.Thread.CurrentThread.ManagedThreadId);
			System.Threading.Thread.Sleep(100);
			Console.WriteLine("Finished working on: " + item);
		}

		// #Parallel #foreach #for #ParallelLoopState
		public static void ParallelFor()
		{ 
			var items = Enumerable.Range(0, 500);
			Parallel.ForEach(items, item =>     // also works with Parallel.For(0, 500, ...
			{
				WorkOnItem(item);
			});

			Console.WriteLine("----");

			var itemsArray = Enumerable.Range(0, 500).ToArray();
			ParallelLoopResult result = Parallel.For(0, itemsArray.Count(), (int i, ParallelLoopState loopState) =>
			{
                // break : all lambda expressions below 200 are completed, 
                // stop  : lambda expressions below 200 might be killed.
				if (i == 200)
					loopState.Break();		

				WorkOnItem(itemsArray[i]);
			});

			Assert.IsFalse(result.IsCompleted);

		}
	}
}
