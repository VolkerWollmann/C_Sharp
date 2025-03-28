﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

// #custom type alias
using IntegerPairs = System.Tuple<int, int>;

namespace C_SharpExamplesLib.Language
{
	//C-Sharp 6 features
	public class CSharp6
	{
		/// <summary>
		/// Const #class property. Must be accessed by class.
		/// </summary>
		private const string ClassProperty = "42";

		/// <summary>
		/// #public field
		/// </summary>
		private int _y;

		/// <summary>
		/// #public property #setter #getter
		/// </summary>
		private int X { get; } = 7; // Only Setter

		private CSharp6(int x)
		{
			Assert.AreEqual(7,X);
			Assert.AreEqual(0,_y);
			X = x;
		}

		private double SquareRoot(int i)
        {
            var result = Math.Sqrt(i);
            return result;
        }

		private double SquareRoot2(int i) => Math.Sqrt(i);

		// #string interpolation $""
		private string StringInterpolation()
		{
			return $"({X})";
		}

		private string StringInterpolation2() => $"({X})";

        // ReSharper disable once UseStringInterpolation
		// This shows, how the string was formatted before c sharp 6
        private string StringFormat() => string.Format("({0})", X);

        // #string #interpolation #alignment
		private const string InternalRabbit = "Rabbit";
		private string StringInterpolation3() => $"({InternalRabbit}:{InternalRabbit, 10}:{InternalRabbit,3}:{InternalRabbit,-10}:)";

		// Method with return type #Tuple
		private (int Zahl, bool Wahrheit) GetTuple() => (1,true);

		// #Dictionary #initialization
		private Dictionary<string,int> GetDictionary()
		{
			return new Dictionary<string, int> { ["X"] = 5, ["Y"] = 6 };
		}

		
		private static void NullableType()
		{
			// #nullable : b might be true, false, null 
			bool? b = null;
			Assert.IsNull(b);

            b = false;
            Assert.IsFalse(b.Value);
        }

        // #Assert #Exception #Action
		public static void Assert_Test()
		{
            int divisor = 0;
            int i=0;

			Action badAction = () => { i = 1 / divisor; };

			try
			{
				badAction.Invoke();

                Assert.IsTrue(i > 1);
				Assert.Fail( "If this exception occurs, then prior expected exception had not been thrown." );
            }
			catch( Exception e )
			{
				Assert.IsInstanceOfType(e, typeof(DivideByZeroException)); 
			}

			Assert.ThrowsException<DivideByZeroException>(badAction, "Exception DivideByZeroException expected");

		}

        // #Tuple
		public static void TupleTest()
        {
            var myTuple = new IntegerPairs (1, 42);
			Assert.AreEqual(1, myTuple.Item1);
			Assert.AreEqual(42, myTuple.Item2);

			CSharp6 cs6 = new CSharp6(5);
			var r = cs6.GetTuple();
			Assert.IsTrue(r.Wahrheit);

			var myTuple2 = Tuple.Create(1, false);
			Assert.IsInstanceOfType(myTuple2.Item2, typeof(bool));
			

			var t = (1, "Rabbit", true);
			var (_, tier, _) = t;
			Assert.AreEqual("Rabbit", tier);
		}
		public static void Test()
		{
			CSharp6 cs6 = new CSharp6(5);

            string s1 = cs6.StringFormat();
            Assert.IsTrue(s1 == "(5)");
			
            string s = cs6.StringInterpolation();
			Assert.IsTrue(s == "(5)");

            string t = cs6.StringInterpolation2();
			Assert.AreEqual("(5)", t);

            string u = cs6.StringInterpolation3();
			Assert.IsNotNull(u);

			var dict = cs6.GetDictionary();
			Assert.IsInstanceOfType(dict, typeof( Dictionary<string,int>));

            int three = (int)cs6.SquareRoot(9);
			Assert.AreEqual(3, three);

            int four = (int) cs6.SquareRoot2(16);
			Assert.AreEqual(4, four);

			NullableType();

            cs6._y = 42;

			// Does not compile 
			// Assert.IsNotNull(cs6.ClassProperty); 

			Assert.IsNotNull(ClassProperty);
        }

        // Get nur mit c# 8.0 ( .NET Core 3 or .NET Standard 2.1)
        //#nullable enable
        public static void NullableReferences()
        {
            //string? t = null;
            //TODO nullable references
        }

		// #Dictionary double usage
		public static void DictionaryDoubleUsage()
		{
			Tuple<int, string> macchi = new Tuple<int, string>(1, "Macchi");
			Tuple<int, string> amica = new Tuple<int, string>(2, "Amcia");
			Tuple<int, string> heidi = new Tuple<int, string>(3, "Heidi");

			List<Tuple<int, string>> friends = [macchi, amica, heidi];

			Dictionary<int, Tuple<int, string>> friendsByNumber = new Dictionary<int, Tuple<int, string>>();
			Dictionary<string, Tuple<int, string>> friendsByName = new Dictionary<string, Tuple<int, string>>();

			foreach (var friend in friends)
			{
				friendsByNumber.Add(friend.Item1, friend);
				friendsByName.Add(friend.Item2, friend);
			}
			
			Assert.AreEqual( macchi, friendsByNumber[1] );
			Assert.AreEqual(amica, friendsByName["Amcia"]);
		}


	}
}
