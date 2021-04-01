using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Langauge
{
	//C-Sharp 6 features
	public class CSharp6
	{
		/// <summary>
		/// Const #class property. Must be accessed by class.
		/// </summary>
		public const string ClassProperty = "42";

		/// <summary>
		/// #public field
		/// </summary>
		public int Y;

		/// <summary>
		/// #public property #setter #getter
		/// </summary>
		public int X { get; } = 7; // Only Setter

		internal CSharp6(int x)
		{
			X = x;
		}

		internal double SquareRoot(int i) 
		{
			double result;
			result = Math.Sqrt(i);   // using System.Math and only Sqrt ?
			return result;
		}

		internal double SquareRoot2(int i) => Math.Sqrt(i);

		// #string interpolation
		internal string StringInterpolation()
		{
			return $"({X})";
		}

		internal string StringInterpolation2() => $"({X})";

		internal (int Zahl, bool Wahrheit) GetTuple() => (1,true);

		// #Dictionary #initialization
		internal Dictionary<string,int> GetDictionary()
		{
			return new Dictionary<string, int> { ["X"] = 5, ["Y"] = 6 };
		}

		private static bool NullableType()
		{
			//
			bool? b = false;
			bool b2 = b.Value;

			return b2;
		}

		// #Assert #Exception #Action
		public static void Assert_Test()
		{
            int divisor = 0;

			Action badAction = () => { int i = 1 / divisor; i++; };

			try
			{
				// badAction.Invoke();
				badAction();

				Assert.Fail( "If this exception occurs, then prior expected exception had not been thrown." );				
			}
			catch( Exception e )
			{
				Assert.IsInstanceOfType(e, typeof(DivideByZeroException)); 
			}

			Assert.ThrowsException<DivideByZeroException>(badAction, "Exception DivideByZeroException expected");

		}

		public static void TupleTest()
		{
			CSharp6 cs6 = new CSharp6(5);
			var r = cs6.GetTuple();
			Assert.IsTrue(r.Wahrheit);

			var t = (1, "Rabbit", true);
			var (_, tier, _) = t;
			Assert.AreEqual(tier, "Rabbit");
		}
		public static void Test()
		{
			CSharp6 cs6 = new CSharp6(5);

			string s = cs6.StringInterpolation();
			Assert.IsTrue(s == "(5)");

            string t = cs6.StringInterpolation2();
			Assert.AreEqual(t, "(5)");

			var dict = cs6.GetDictionary();
			Assert.IsInstanceOfType(dict, typeof( Dictionary<string,int>));

            int three = (int)cs6.SquareRoot(9);
			Assert.AreEqual(three, 3);

            int four = (int) cs6.SquareRoot2(16);
			Assert.AreEqual(four,4);

			var b = NullableType();
			Assert.AreEqual(b, false);

			cs6.Y = 42;

			// Does not compile 
			// Assert.IsNotNull(cs6.ClassProperty); 

			Assert.IsNotNull(CSharp6.ClassProperty);
        }

		// Get nur mit c# 8.0 ( .NET Core 3 or .NET Standard 2.1)
		//#nullable enable
		public static void NullableReferences()
		{
		   //string? t = null;
		   //TODO nullable references
		}
	}
}
