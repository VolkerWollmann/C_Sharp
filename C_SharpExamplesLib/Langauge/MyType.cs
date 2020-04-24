/// <summary>
/// #Defining a #alias type
/// </summary>
namespace C_Sharp.Types
{
	// Defining a type alias
	using IntegerList = System.Collections.Generic.List<int>;
}

namespace C_Sharp.Types
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
	// Defining a type alias
	using IntegerList = System.Collections.Generic.List<int>;
	public class MyType
	{
		// Defining a type as class based on base type
		public class IntegerList2 : System.Collections.Generic.List<int>
		{

		}

		public static void Test()
		{
			IntegerList x = new IntegerList();
			IntegerList2 y = new IntegerList2();

			Assert.IsTrue(x.GetType() == typeof(IntegerList));

			Type tx = typeof(IntegerList);
			Type tx2 = typeof(System.Collections.Generic.List<int>);
			Type tx3 = typeof(IntegerList2);

			object tx2o = Activator.CreateInstance(tx2);
			object tx3o = Activator.CreateInstance(tx3);

			if (x is IntegerList il)
				Assert.IsTrue(il != null);

			if (x is System.Collections.Generic.List<int> il2)
				Assert.IsTrue(il2 != null);

			if (y is System.Collections.Generic.List<int> il3)
				Assert.IsTrue(il3 != null);

		}
	}
}

namespace C_Sharp.Types
{
	public class MyType2
	{
	  public static void Test()
		{
			// Compile Error : does not know about type alias
			//IntegerList x = new IntegerList();
		}
	}
}

namespace C_Sharp
{
	// Defining a type as class based on base type
	// Does not know about previous type alias
	using IntegerList = System.Collections.Generic.List<int>;
	public class MyType3
	{
		public static void Test()
		{
			IntegerList x = new IntegerList();
		}
	}
}
