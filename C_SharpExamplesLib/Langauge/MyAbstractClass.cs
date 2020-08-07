using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp
{
	/// <summary>
	/// #abstract class
	/// </summary>
	abstract class MyAbstractClass
	{
	}

	class MyConcreteClass : MyAbstractClass
	{
		public string InstancePorperty { get; } = "Hase";
	}

	public class AbstractClassTest
	{
		public static void Test()
		{
			Type myAbstractClassType = typeof(MyAbstractClass);
			Type myConcreteClassType = typeof(MyConcreteClass);

			try
			{
				object myAbstractClassInstance = Activator.CreateInstance(myAbstractClassType);
			}
			catch( Exception exp)
			{
				// you cannot instanciate an abstract class
				Console.WriteLine(exp.ToString());
				Assert.IsTrue(exp.GetType() == typeof(MissingMethodException));
			}

			// create instance of concrete class by using type information
			object myConcreteClassInstance = Activator.CreateInstance(myConcreteClassType);
			Assert.IsNotNull(((MyConcreteClass)myConcreteClassInstance).InstancePorperty);

			// check type with is operator and assign as base class
			if (myConcreteClassInstance is MyAbstractClass myConcreteClassBaseClassInstance)
			{
				// check that casted base class instance exists
				Assert.IsNotNull(myConcreteClassBaseClassInstance);

				// Get type of casted base class instance
				Type myConcreteClassBaseClassInstanceType = myConcreteClassBaseClassInstance.GetType();

				// GetType returns actual type and not casted type
				Assert.AreEqual(myConcreteClassBaseClassInstanceType, myConcreteClassType);
				Assert.AreNotEqual(myConcreteClassBaseClassInstanceType, myAbstractClassType);				
			}
		}
	}

}
