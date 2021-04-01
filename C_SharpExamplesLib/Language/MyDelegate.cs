using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Langauge
{
	/// <summary>
	/// #delegate
	/// </summary>
	public class MyDelegate
	{
		// #action
		static Action<string> WriteMessage = (msg) => { Console.WriteLine(msg); };

		// #func
		static Func<int> funcIntDirect = () => { return 42; };
		static Func<int, int> funcIntIntDirect = (i) => { return i; };

		// #predicate
		static Predicate<int> LessThanTree = (i) => i < 3;

		// #Func : function pointer ( and type )
		Func<int, int> funcIntInt;

		// #delegate : like a #function pointer type
		delegate int IntegerFunction(int i);

		// Delegate : like instance of typed function pointer
		IntegerFunction funcIntegerFunction;

		// Delegate : like instance of untyped function pointer
		Delegate Delegate;

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

		private MyDelegate()
		{			
		}

		public static void TestActionFuncPredicate()
        {
			WriteMessage("Hello World");

			Assert.AreEqual(funcIntDirect(), 42);

			Assert.AreEqual(funcIntIntDirect(42), 42);

			Assert.IsTrue(LessThanTree(2));
		}

		public static void TestDelegateAndFunc()
		{
			MyDelegate myDelegate = new MyDelegate();
			int i;

			myDelegate.funcIntInt = myDelegate.Double;
		    i = myDelegate.funcIntInt(3);
			Assert.IsTrue(i == 6);

			myDelegate.funcIntInt = myDelegate.Square;
			i = myDelegate.funcIntInt(3);
			Assert.IsTrue(i == 9);

			myDelegate.funcIntegerFunction = myDelegate.Double;
			i = myDelegate.funcIntegerFunction(3);
			Assert.IsTrue(i == 6);

			myDelegate.funcIntegerFunction = new IntegerFunction(myDelegate.Square);
			i = myDelegate.funcIntegerFunction(3);
			Assert.IsTrue(i == 9);

			// Assignment of lambda expression
			myDelegate.funcIntInt = (n) => { return n * n; };
			i = myDelegate.funcIntInt(4);
			Assert.IsTrue(i == 16);

			myDelegate.funcIntegerFunction = (n) => { return n * n; };
			i = myDelegate.funcIntegerFunction(5);
			Assert.IsTrue(i == 25);

			// that will not cast : similar but not same
			// myDelegate.funcIntInt = (IntegerFunction)myDelegate.funcIntegerFunction;
			// myDelegate.funcIntegerFunction = (Func<int, int>)myDelegate.funcIntInt;

			myDelegate.funcIntInt = myDelegate.Square;
			myDelegate.funcIntegerFunction = new IntegerFunction(myDelegate.funcIntInt);
			i = myDelegate.funcIntegerFunction(2);
			Assert.IsTrue(i == 4);

			myDelegate.funcIntegerFunction = myDelegate.Square;
			myDelegate.funcIntInt = (Func<int, int>)null;
			myDelegate.funcIntInt = new Func<int,int>(myDelegate.funcIntegerFunction);
			i = myDelegate.funcIntInt(3);
			Assert.IsTrue(i == 9);

		}

		public static void TestDelegateFuncInvocationList()
		{
			MyDelegate myDelegate = new MyDelegate();
			int i;
			Delegate[] fInvocationList, f2InvocationList;

			myDelegate.funcIntInt = myDelegate.Double;
			myDelegate.funcIntInt += myDelegate.Square;

			// invocationList : normally 0 or 1 Element, but more functions can be assigned
			// last wins
		    fInvocationList = myDelegate.funcIntInt.GetInvocationList();

			i = myDelegate.funcIntInt(3);
			Assert.IsTrue(i == 9);

			myDelegate.funcIntegerFunction = StaticDouble;
			myDelegate.funcIntegerFunction += myDelegate.Square;

			f2InvocationList = myDelegate.funcIntegerFunction.GetInvocationList();

			i = myDelegate.funcIntegerFunction(3);
			Assert.IsTrue(i == 9);

		}

		public static void TestDelegateAssignmentByName()
		{
			MyDelegate myDelegate = new MyDelegate();
			int delegateResult;
			
			myDelegate.Delegate = Delegate.CreateDelegate(typeof(Func<int, int>), myDelegate, "Square" );
			delegateResult = (int)myDelegate.Delegate.DynamicInvoke(3);
			Assert.IsTrue(delegateResult == 9);

			myDelegate.Delegate = Delegate.CreateDelegate(typeof(Func<int, int, int>), myDelegate, "BadFunction");
			delegateResult = (int)myDelegate.Delegate.DynamicInvoke(41,1);
			Assert.IsTrue(delegateResult == 42);

			myDelegate.funcIntInt = (Func<int,int>)Delegate.CreateDelegate(typeof(Func<int, int>), myDelegate, "Square");
			int funcIntIntResult = myDelegate.funcIntInt(4);
			Assert.IsTrue(funcIntIntResult == 16);

			myDelegate.funcIntegerFunction = myDelegate.Square;

			// will not compile because of wrong type
			// myDelegate.f2 = myDelegate.BadFunction;

			myDelegate.funcIntegerFunction = (IntegerFunction)Delegate.CreateDelegate(typeof(IntegerFunction), myDelegate, "Square");
			delegateResult = myDelegate.funcIntegerFunction(4);
			Assert.IsTrue(delegateResult == 16);

			try
			{
				myDelegate.funcIntegerFunction = (IntegerFunction)Delegate.CreateDelegate(typeof(IntegerFunction), myDelegate, "BadFunction");
				delegateResult = myDelegate.funcIntegerFunction(5);
			}
			catch (ArgumentException exp )
			{
				// runtime error : BadFunction cannot be assigned to func2
				string expDescription = exp.ToString();
			}
		}

		public static void TestDelegateAssignmentByMethodInfo()
		{
			MyDelegate myDelegate = new MyDelegate();
			int delegateResult;
			Delegate[] fRawInvocationList, fInvocationList, f2InvocationList;

			Type t = myDelegate.GetType();
			TypeInfo ti = t.GetTypeInfo();
			MethodInfo mSquare = ti.GetDeclaredMethod("Square");

			myDelegate.Delegate = Delegate.CreateDelegate(typeof(Func<int,int>), myDelegate, mSquare);
			fRawInvocationList = myDelegate.Delegate.GetInvocationList();

			delegateResult = (int)myDelegate.Delegate.DynamicInvoke(3);
			Assert.IsTrue(delegateResult == 9);

			myDelegate.Delegate = Delegate.CreateDelegate(typeof(IntegerFunction), myDelegate, mSquare);
			fInvocationList = myDelegate.Delegate.GetInvocationList();

			delegateResult = (int)myDelegate.Delegate.DynamicInvoke(3);
			Assert.IsTrue(delegateResult == 9);

			MethodInfo mDouble = ti.GetDeclaredMethod("Double");
			myDelegate.Delegate = System.Delegate.Combine(myDelegate.Delegate,
				Delegate.CreateDelegate(typeof(IntegerFunction), myDelegate, mDouble));
			f2InvocationList = myDelegate.Delegate.GetInvocationList();

			delegateResult = (int)myDelegate.Delegate.DynamicInvoke(3);
			Assert.IsTrue(delegateResult == 6);

			MethodInfo mStaticDouble = ti.GetDeclaredMethod("StaticDouble");
			myDelegate.Delegate = Delegate.CreateDelegate(typeof(IntegerFunction), mStaticDouble);
			delegateResult = (int)myDelegate.Delegate.DynamicInvoke(4);
			Assert.IsTrue(delegateResult == 8);
		} 
	}
}
