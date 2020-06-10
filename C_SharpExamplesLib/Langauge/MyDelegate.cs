using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp
{
	/// <summary>
	/// #delegate
	/// </summary>
	public class MyDelegate
	{
		Func<int, int> f;

		// #delegate : like a #function pointer type
		delegate int IntegerFunction(int i);

		// Delegate : like instance of typed function pointer
		IntegerFunction f2;

		// Delegate : like instance of untyped function pointer
		Delegate _delegate;

		#region Integer Functions       
		int Square(int i)
		{
			return i * i;
		}

		int Double(int i)
		{
			return 2 * i;
		}

		int BadFunction(int i, int j)
		{
			return i + j;
		}

		static int StaticDouble(int i)
		{
			return 2 * i;
		}
		#endregion

		Action<string> WriteMessage = (msg) => { Console.WriteLine(msg); };

		Predicate<int> LessThanTree = (i) => i < 3;

		private MyDelegate()
		{			
		}

		public static void Test()
		{
			MyDelegate myDelegate = new MyDelegate();
			int i;

			myDelegate.f = myDelegate.Double;
		    i = myDelegate.f(3);
			Assert.IsTrue(i == 6);

			myDelegate.f = myDelegate.Square;
			i = myDelegate.f(3);
			Assert.IsTrue(i == 9);

			myDelegate.f2 = myDelegate.Double;
			i = myDelegate.f2(3);
			Assert.IsTrue(i == 6);

			myDelegate.f2 = new IntegerFunction(myDelegate.Square);
			i = myDelegate.f2(3);
			Assert.IsTrue(i == 9);

			// Assignment of lamda expression
			myDelegate.f = (n) => { return n * n; };
			i = myDelegate.f(4);
			Assert.IsTrue(i == 16);

			myDelegate.f2 = (n) => { return n * n; };
			i = myDelegate.f2(5);
			Assert.IsTrue(i == 25);

			myDelegate.WriteMessage("Hello World");

			Assert.IsTrue(myDelegate.LessThanTree(2));
		}

		public static void Test2()
		{
			MyDelegate myDelegate = new MyDelegate();
			int i;
			Delegate[] fInvocationList, f2InvocationList;

			myDelegate.f = myDelegate.Double;
			myDelegate.f += myDelegate.Square;

			// invocationList
		    fInvocationList = myDelegate.f.GetInvocationList();

			i = myDelegate.f(3);
			Assert.IsTrue(i == 9);

			myDelegate.f2 = StaticDouble;
			myDelegate.f2 += myDelegate.Square;

			f2InvocationList = myDelegate.f2.GetInvocationList();

			i = myDelegate.f2(3);
			Assert.IsTrue(i == 9);

		}

		public static void Test3()
		{
			MyDelegate myDelegate = new MyDelegate();
			int delegateResult;
			
			myDelegate.f2 = myDelegate.Square;

			// will not compile because of wrong type
			// myDelegate.f2 = myDelegate.BadFunction;

			myDelegate._delegate = Delegate.CreateDelegate(typeof(Func<int, int>), myDelegate, "Square" );
			delegateResult = (int)myDelegate._delegate.DynamicInvoke(3);
			Assert.IsTrue(delegateResult == 9);

			myDelegate._delegate = Delegate.CreateDelegate(typeof(Func<int, int, int>), myDelegate, "BadFunction");
			delegateResult = (int)myDelegate._delegate.DynamicInvoke(41,1);
			Assert.IsTrue(delegateResult == 42);

			myDelegate.f2 = (IntegerFunction)Delegate.CreateDelegate(typeof(IntegerFunction), myDelegate, "Square");
			delegateResult = myDelegate.f2(4);
			Assert.IsTrue(delegateResult == 16);

			try
			{
				myDelegate.f2 = (IntegerFunction)Delegate.CreateDelegate(typeof(IntegerFunction), myDelegate, "BadFunction");
				delegateResult = myDelegate.f2(5);
			}
			catch (ArgumentException exp )
			{
				// runtime error : BadFunction cannot be assigned to f2
				string expDescription = exp.ToString();
			}
		}

		public static void Test4()
		{
			MyDelegate myDelegate = new MyDelegate();
			int delegateResult;
			Delegate[] fRawInvocationList, fInvocationList, f2InvocationList;

			Type t = myDelegate.GetType();
			TypeInfo ti = t.GetTypeInfo();
			MethodInfo mSquare = ti.GetDeclaredMethod("Square");

			myDelegate._delegate = Delegate.CreateDelegate(typeof(Func<int,int>), myDelegate, mSquare);
			fRawInvocationList = myDelegate._delegate.GetInvocationList();

			delegateResult = (int)myDelegate._delegate.DynamicInvoke(3);
			Assert.IsTrue(delegateResult == 9);

			myDelegate._delegate = Delegate.CreateDelegate(typeof(IntegerFunction), myDelegate, mSquare);
			fInvocationList = myDelegate._delegate.GetInvocationList();

			delegateResult = (int)myDelegate._delegate.DynamicInvoke(3);
			Assert.IsTrue(delegateResult == 9);

			MethodInfo mDouble = ti.GetDeclaredMethod("Double");
			myDelegate._delegate = System.Delegate.Combine(myDelegate._delegate,
				Delegate.CreateDelegate(typeof(IntegerFunction), myDelegate, mDouble));
			f2InvocationList = myDelegate._delegate.GetInvocationList();

			delegateResult = (int)myDelegate._delegate.DynamicInvoke(3);
			Assert.IsTrue(delegateResult == 6);

			MethodInfo mStaticDouble = ti.GetDeclaredMethod("StaticDouble");
			myDelegate._delegate = Delegate.CreateDelegate(typeof(IntegerFunction), mStaticDouble);
			delegateResult = (int)myDelegate._delegate.DynamicInvoke(4);
			Assert.IsTrue(delegateResult == 8);
		} 
	}
}
