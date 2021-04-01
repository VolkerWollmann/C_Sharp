using System;
using C_Sharp.Langauge.Thread;

namespace C_Sharp
{
	class Program
	{
		static void Main(string[] args)
        {
            var a = args;
			//MyEnum.Test();
			//MyOperators.Test();
			//MyAnonymousType.Test();
			//MyDelegate.Test();
			//MyException.Test();
			//MyListTest.Test();
			//MyThread.SemaphoreExample();
			MyThread.TestThreadStaticData();
			Console.ReadKey();
		}
	}
}
