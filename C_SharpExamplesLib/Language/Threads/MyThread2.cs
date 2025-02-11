namespace C_SharpExamplesLib.Language.Threads
{
    /// <summary>
    /// #partial class
    /// </summary>
    public abstract partial class MyThread
    {
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
        public static void Thread_Dispatcher()
        {
            //Dispatcher.CurrentDispatcher.Invoke(MyThread.Method);
            //Console.WriteLine("After asynchronous start of method within thread " +
            //                  System.Threading.Thread.CurrentThread.ManagedThreadId);

            Parallel.Invoke(Task1, Task2);
            Console.WriteLine("Finished processing within thread " +
                              Thread.CurrentThread.ManagedThreadId);
        }

    }
}
