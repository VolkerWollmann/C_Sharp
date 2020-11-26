using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace C_Sharp
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

		internal double SqaureRoot(int i) 
		{
			double result;
			result = Math.Sqrt(i);   // using System.Math and only Sqrt ?
			return result;
		}

		internal double SqaureRoot2(int i) => Math.Sqrt(i);

		// #string interpolation
		internal string StringInterpolation()
		{
			return $"({X})";
		}

		internal string StringInterpolation2() => $"({X})";

		internal (int Zahl, bool Wahrheit) GetTuple() => (1,true);

		// #Dictionary #initialisation
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
			string s;
			int divisor = 0;

			Action badAction = () => { int i = 1 / divisor; };

			try
			{
				//throw (new Exception());
				badAction.Invoke();

				Assert.Fail( "If this exception occurs, then prior expected exception had not been thrown." );				
			}
			catch( Exception e )
			{
				s = e.GetType().Name;
			}

			Assert.ThrowsException<DivideByZeroException>(badAction, "Exception DivideByZeroException expected");

		}

		public static void TupleTest()
		{
			CSharp6 cs6 = new CSharp6(5);
			var r = cs6.GetTuple();
			Assert.IsTrue(r.Wahrheit);

			var t = (1, "Hase", true);
			(_, var tier, _) = t;
			Assert.AreEqual(tier, "Hase");
		}
		public static void Test()
		{
			CSharp6 cs6 = new CSharp6(5);

			string s = cs6.StringInterpolation();
			Assert.IsTrue(s == "(5)");

			var dict = cs6.GetDictionary();

			var b = NullableType();

			cs6.Y = 42;

			// Does not compile 
			// Assert.IsNotNull(cs6.ClassProperty); 

			Assert.IsNotNull(CSharp6.ClassProperty); 
		}

		// Get nur mit c# 8.0 ( .NET Core 3 or .NET Standard 2.1)
		//#nullable enable
		public static void NullableRefrences()
		{
		   //string? t = null;
		   //TODO nullable references
		}
	}
}
