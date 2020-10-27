using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace C_Sharp
{
	public class MyProcess
	{

		private static void Method()
		{
			Thread.Sleep(100);
			Console.WriteLine("Method in thread " + Thread.CurrentThread.ManagedThreadId); 
			return;
		}

		static void Task1()
		{
			Console.WriteLine("Task 1 starting in thread " + Thread.CurrentThread.ManagedThreadId);
			Thread.Sleep(2000);
			Console.WriteLine("Task 1 ending");
		}

		static void Task2()
		{
			Console.WriteLine("Task 2 starting in thread " + Thread.CurrentThread.ManagedThreadId);
			Thread.Sleep(1000);
			Console.WriteLine("Task 2 ending");
		}

		// #Invoke #Parallel #Dispatcher #foreach
		public static void Test()
		{
			Dispatcher.CurrentDispatcher.Invoke(new Action(() => { MyProcess.Method(); }));
			Console.WriteLine("After asynchronus start of method within thread " + Thread.CurrentThread.ManagedThreadId);

			Parallel.Invoke(() => Task1(), () => Task2());
			Console.WriteLine("Finished processing within thread " + Thread.CurrentThread.ManagedThreadId);
		}

		static void WorkOnItem(object item)
		{
			Console.WriteLine("Started working on: " + item + " within thread " + Thread.CurrentThread.ManagedThreadId);
			Thread.Sleep(100);
			Console.WriteLine("Finished working on: " + item);
		}

		// # Parallel #foreach #for #ParallelLoopState
		public static void Test2()
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
				if (i == 200)
					loopState.Break();

				WorkOnItem(itemsArray[i]);
			});

		}
	}
}
