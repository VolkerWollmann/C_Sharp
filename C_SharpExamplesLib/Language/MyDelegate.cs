using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
	/// <summary>
	/// #delegate
	/// </summary>
	[SuppressMessage("ReSharper", "ConvertClosureToMethodGroup")]
    [SuppressMessage("ReSharper", "ConvertToLambdaExpression")]
    [SuppressMessage("ReSharper", "ConvertToLocalFunction")]
    public class MyDelegate
	{
		// #empty #action
        private static readonly Action EmptyAction = () => { };

        // #action
		static readonly Action<string> WriteMessage = msg => { Console.WriteLine(msg); };

		// #func
		static readonly Func<int> FuncIntDirect = () => { return 42; };
		static readonly Func<int, int> FuncIntIntDirect = (i) => { return i; };

		// #predicate
		static readonly Predicate<int> LessThanTree = (i) => i < 3;

		// #Func : function pointer ( and type )
		Func<int, int> FuncIntInt;

		// #delegate : like a #function pointer type
		delegate int IntegerFunction(int i);

		// Delegate : like instance of typed function pointer
		IntegerFunction FuncIntegerFunction;

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
            EmptyAction();

			WriteMessage("Hello World");

			Assert.AreEqual(FuncIntDirect(), 42);

			Assert.AreEqual(FuncIntIntDirect(42), 42);

			Assert.IsTrue(LessThanTree(2));
		}

		public static void TestDelegateAndFunc()
		{
			MyDelegate myDelegate = new MyDelegate();

            myDelegate.FuncIntInt = myDelegate.Double;
		    var i = myDelegate.FuncIntInt(3);
			Assert.IsTrue(i == 6);

			myDelegate.FuncIntInt = myDelegate.Square;
			i = myDelegate.FuncIntInt(3);
			Assert.IsTrue(i == 9);

			myDelegate.FuncIntegerFunction = myDelegate.Double;
			i = myDelegate.FuncIntegerFunction(3);
			Assert.IsTrue(i == 6);

			myDelegate.FuncIntegerFunction = myDelegate.Square;
			i = myDelegate.FuncIntegerFunction(3);
			Assert.IsTrue(i == 9);

			// Assignment of lambda expression
			myDelegate.FuncIntInt = (n) => { return n * n; };
			i = myDelegate.FuncIntInt(4);
			Assert.IsTrue(i == 16);

			myDelegate.FuncIntegerFunction = (n) => { return n * n; };
			i = myDelegate.FuncIntegerFunction(5);
			Assert.IsTrue(i == 25);

			// that will not cast : similar but not same
			// myDelegate.funcIntInt = (IntegerFunction)myDelegate.funcIntegerFunction;
			// myDelegate.funcIntegerFunction = (Func<int, int>)myDelegate.funcIntInt;

			myDelegate.FuncIntInt = myDelegate.Square;
			myDelegate.FuncIntegerFunction = new IntegerFunction(myDelegate.FuncIntInt);
			i = myDelegate.FuncIntegerFunction(2);
			Assert.IsTrue(i == 4);

			myDelegate.FuncIntegerFunction = myDelegate.Square;
			myDelegate.FuncIntInt = null;
			myDelegate.FuncIntInt = new Func<int,int>(myDelegate.FuncIntegerFunction);
			i = myDelegate.FuncIntInt(3);
			Assert.IsTrue(i == 9);

			// will not compile due to type error
            Func<int, int, int> funcIntIntInt = myDelegate.BadFunction;
			Assert.IsNotNull(funcIntIntInt);
			// myDelegate.funcIntInt = funcIntIntInt;

		}

        public static void TestFuncConcatenation()
        {
            Func<int, int> increment = (n) => { return ++n; };

            Func<int, int> square = (n) => { return n*n; };

			Assert.AreEqual( square(increment(1)),4);

            Func<int, int> both = (n) => square(increment(n));

            Assert.AreEqual(both(1), 4);
		}
		public static void TestDelegateFuncInvocationList()
		{
			MyDelegate myDelegate = new MyDelegate();

            myDelegate.FuncIntInt = myDelegate.Double;
			myDelegate.FuncIntInt += myDelegate.Square;

			// invocationList : normally 0 or 1 Element, but more functions can be assigned
			// last wins
		    var fInvocationList = myDelegate.FuncIntInt.GetInvocationList();
			Assert.IsNotNull(fInvocationList);

			var i = myDelegate.FuncIntInt(3);
			Assert.IsTrue(i == 9);

			myDelegate.FuncIntegerFunction = StaticDouble;
			myDelegate.FuncIntegerFunction += myDelegate.Square;

			var f2InvocationList = myDelegate.FuncIntegerFunction.GetInvocationList();
			Assert.IsNotNull(f2InvocationList);

			i = myDelegate.FuncIntegerFunction(3);
			Assert.IsTrue(i == 9);

		}

		public static void TestDelegateAssignmentByName()
		{
			MyDelegate myDelegate = new MyDelegate();

            myDelegate.Delegate = Delegate.CreateDelegate(typeof(Func<int, int>), myDelegate, "Square" );
			var delegateResult = (int)myDelegate.Delegate.DynamicInvoke(3);
			Assert.IsTrue(delegateResult == 9);

			myDelegate.Delegate = Delegate.CreateDelegate(typeof(Func<int, int, int>), myDelegate, "BadFunction");
			delegateResult = (int)myDelegate.Delegate.DynamicInvoke(41,1);
			Assert.IsTrue(delegateResult == 42);

			myDelegate.FuncIntInt = (Func<int,int>)Delegate.CreateDelegate(typeof(Func<int, int>), myDelegate, "Square");
			int funcIntIntResult = myDelegate.FuncIntInt(4);
			Assert.IsTrue(funcIntIntResult == 16);

			myDelegate.FuncIntegerFunction = myDelegate.Square;

			// will not compile because of wrong type
			// myDelegate.f2 = myDelegate.BadFunction;

			myDelegate.FuncIntegerFunction = (IntegerFunction)Delegate.CreateDelegate(typeof(IntegerFunction), myDelegate, "Square");
			delegateResult = myDelegate.FuncIntegerFunction(4);
			Assert.IsTrue(delegateResult == 16);


            Action createDelegateByMethodName = () =>
            {
                myDelegate.FuncIntegerFunction =
                    (IntegerFunction) Delegate.CreateDelegate(typeof(IntegerFunction), myDelegate, "BadFunction");
            };
            Assert.ThrowsException<ArgumentException>(createDelegateByMethodName);
			
		}

		public static void TestDelegateAssignmentByMethodInfo()
		{
			MyDelegate myDelegate = new MyDelegate();

            Type t = myDelegate.GetType();
			TypeInfo ti = t.GetTypeInfo();
			MethodInfo mSquare = ti.GetDeclaredMethod("Square");

			myDelegate.Delegate = Delegate.CreateDelegate(typeof(Func<int,int>), myDelegate, mSquare);
			var fRawInvocationList = myDelegate.Delegate.GetInvocationList();
			Assert.IsNotNull(fRawInvocationList);

			var delegateResult = (int)myDelegate.Delegate.DynamicInvoke(3);
			Assert.IsTrue(delegateResult == 9);

			myDelegate.Delegate = Delegate.CreateDelegate(typeof(IntegerFunction), myDelegate, mSquare);
			var fInvocationList = myDelegate.Delegate.GetInvocationList();
			Assert.IsNotNull(fInvocationList);

			delegateResult = (int)myDelegate.Delegate.DynamicInvoke(3);
			Assert.IsTrue(delegateResult == 9);

			MethodInfo mDouble = ti.GetDeclaredMethod("Double");
			myDelegate.Delegate = Delegate.Combine(myDelegate.Delegate,
				Delegate.CreateDelegate(typeof(IntegerFunction), myDelegate, mDouble));
			var f2InvocationList = myDelegate.Delegate.GetInvocationList();
            Assert.IsNotNull(f2InvocationList);

			delegateResult = (int)myDelegate.Delegate.DynamicInvoke(3);
			Assert.IsTrue(delegateResult == 6);

			MethodInfo mStaticDouble = ti.GetDeclaredMethod("StaticDouble");
			myDelegate.Delegate = Delegate.CreateDelegate(typeof(IntegerFunction), mStaticDouble);
			delegateResult = (int)myDelegate.Delegate.DynamicInvoke(4);
			Assert.IsTrue(delegateResult == 8);
		} 
	}
}
