﻿using System;
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

        // #Invoke #Parallel #Dispatcher
		public static void Test()
		{
			Dispatcher.CurrentDispatcher.Invoke(new Action(()=> { MyProcess.Method(); }));
			Console.WriteLine("After asynchronus start of method within thread " + Thread.CurrentThread.ManagedThreadId);

			Parallel.Invoke(() => Task1(), () => Task2());
			Console.WriteLine("Finished processing within thread " + Thread.CurrentThread.ManagedThreadId);
		}
	}
}
