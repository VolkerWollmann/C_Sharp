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
            Dispatcher.CurrentDispatcher.Invoke(MyThread.Method);
            Console.WriteLine("After asynchronous start of method within thread " +
                              System.Threading.Thread.CurrentThread.ManagedThreadId);

            Parallel.Invoke(Task1, Task2);
            Console.WriteLine("Finished processing within thread " +
                              System.Threading.Thread.CurrentThread.ManagedThreadId);
        }

    }
}
