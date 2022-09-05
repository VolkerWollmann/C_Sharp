using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
		// #empty #action #no parameter
        private static readonly Action EmptyAction = () => { };

        // #action #parameter 
		static readonly Action<string> WriteMessage = msg => { Console.WriteLine(msg); };

		// #function #func #no parameter #parameter
		static readonly Func<int> FuncIntDirect = () => { return 42; };
		static readonly Func<int, int> FuncIntIntDirect = (i) => { return i; };

		// #predicate
		static readonly Predicate<int> LessThanTree = (i) => i < 3;

		// #Function  #Func : function pointer ( and type )
		Func<int, int> _funcIntInt;

		// #delegate : like a #function pointer type
		delegate int IntegerFunction(int i);

		// Delegate : like instance of typed function pointer
		IntegerFunction _funcIntegerFunction;

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

            myDelegate._funcIntInt = myDelegate.Double;
		    var i = myDelegate._funcIntInt(3);
			Assert.IsTrue(i == 6);

			myDelegate._funcIntInt = myDelegate.Square;
			i = myDelegate._funcIntInt(3);
			Assert.IsTrue(i == 9);

			myDelegate._funcIntegerFunction = myDelegate.Double;
			i = myDelegate._funcIntegerFunction(3);
			Assert.IsTrue(i == 6);

			myDelegate._funcIntegerFunction = myDelegate.Square;
			i = myDelegate._funcIntegerFunction(3);
			Assert.IsTrue(i == 9);

			// Assignment of lambda expression
			myDelegate._funcIntInt = (n) => { return n * n; };
			i = myDelegate._funcIntInt(4);
			Assert.IsTrue(i == 16);

			myDelegate._funcIntegerFunction = (n) => { return n * n; };
			i = myDelegate._funcIntegerFunction(5);
			Assert.IsTrue(i == 25);

			// that will not cast : similar but not same
			// myDelegate.funcIntInt = (IntegerFunction)myDelegate.funcIntegerFunction;
			// myDelegate.funcIntegerFunction = (Func<int, int>)myDelegate.funcIntInt;

			myDelegate._funcIntInt = myDelegate.Square;
			myDelegate._funcIntegerFunction = new IntegerFunction(myDelegate._funcIntInt);
			i = myDelegate._funcIntegerFunction(2);
			Assert.IsTrue(i == 4);

			myDelegate._funcIntegerFunction = myDelegate.Square;
			myDelegate._funcIntInt = null;
			myDelegate._funcIntInt = new Func<int,int>(myDelegate._funcIntegerFunction);
			i = myDelegate._funcIntInt(3);
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

            myDelegate._funcIntInt = myDelegate.Double;
			myDelegate._funcIntInt += myDelegate.Square;

			// invocationList : normally 0 or 1 Element, but more functions can be assigned
			// last wins
		    var fInvocationList = myDelegate._funcIntInt.GetInvocationList();
			Assert.IsNotNull(fInvocationList.Select( m => m.Method.Name).Contains("Double"));

			var i = myDelegate._funcIntInt(3);
			Assert.IsTrue(i == 9);

			myDelegate._funcIntegerFunction = StaticDouble;
			myDelegate._funcIntegerFunction += myDelegate.Square;

			var f2InvocationList = myDelegate._funcIntegerFunction.GetInvocationList();
            Assert.IsNotNull(f2InvocationList.Select(m => m.Method.Name).Contains("StaticDouble"));

			i = myDelegate._funcIntegerFunction(3);
			Assert.IsTrue(i == 9);

		}

		public static void TestDelegateAssignmentByName()
		{
			MyDelegate myDelegate = new MyDelegate();

            myDelegate._delegate = Delegate.CreateDelegate(typeof(Func<int, int>), myDelegate, "Square" );
			var delegateResult = (int)myDelegate._delegate.DynamicInvoke(3);
			Assert.IsTrue(delegateResult == 9);

			myDelegate._delegate = Delegate.CreateDelegate(typeof(Func<int, int, int>), myDelegate, "BadFunction");
			delegateResult = (int)myDelegate._delegate.DynamicInvoke(41,1);
			Assert.IsTrue(delegateResult == 42);

			myDelegate._funcIntInt = (Func<int,int>)Delegate.CreateDelegate(typeof(Func<int, int>), myDelegate, "Square");
			int funcIntIntResult = myDelegate._funcIntInt(4);
			Assert.IsTrue(funcIntIntResult == 16);

			myDelegate._funcIntegerFunction = myDelegate.Square;

			// will not compile because of wrong type
			// myDelegate.f2 = myDelegate.BadFunction;

			myDelegate._funcIntegerFunction = (IntegerFunction)Delegate.CreateDelegate(typeof(IntegerFunction), myDelegate, "Square");
			delegateResult = myDelegate._funcIntegerFunction(4);
			Assert.IsTrue(delegateResult == 16);


            Action createDelegateByMethodName = () =>
            {
                myDelegate._funcIntegerFunction =
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

			myDelegate._delegate = Delegate.CreateDelegate(typeof(Func<int,int>), myDelegate, mSquare);
			var fRawInvocationList = myDelegate._delegate.GetInvocationList();
			Assert.IsNotNull(fRawInvocationList);

			var delegateResult = (int)myDelegate._delegate.DynamicInvoke(3);
			Assert.IsTrue(delegateResult == 9);

			myDelegate._delegate = Delegate.CreateDelegate(typeof(IntegerFunction), myDelegate, mSquare);
			var fInvocationList = myDelegate._delegate.GetInvocationList();
			Assert.IsNotNull(fInvocationList);

			delegateResult = (int)myDelegate._delegate.DynamicInvoke(3);
			Assert.IsTrue(delegateResult == 9);

			MethodInfo mDouble = ti.GetDeclaredMethod("Double");
			myDelegate._delegate = Delegate.Combine(myDelegate._delegate,
				Delegate.CreateDelegate(typeof(IntegerFunction), myDelegate, mDouble));
			var f2InvocationList = myDelegate._delegate.GetInvocationList();
            Assert.IsNotNull(f2InvocationList);

			delegateResult = (int)myDelegate._delegate.DynamicInvoke(3);
			Assert.IsTrue(delegateResult == 6);

			MethodInfo mStaticDouble = ti.GetDeclaredMethod("StaticDouble");
			myDelegate._delegate = Delegate.CreateDelegate(typeof(IntegerFunction), mStaticDouble);
			delegateResult = (int)myDelegate._delegate.DynamicInvoke(4);
			Assert.IsTrue(delegateResult == 8);
		} 
	}
}
