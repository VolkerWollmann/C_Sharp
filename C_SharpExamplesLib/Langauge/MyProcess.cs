using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace C_Sharp
{
	public class MyProcess
	{

		private static void Method()
		{
			return;
		}

		public static void Test()
		{
			Dispatcher.CurrentDispatcher.Invoke(new Action(()=> { MyProcess.Method(); }));
		}
	}
}
