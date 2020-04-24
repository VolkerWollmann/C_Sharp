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
				Console.WriteLine(exp.ToString());
				Assert.IsTrue(exp.GetType() == typeof(MissingMethodException));
			}

			object myConcreteClassInstance = Activator.CreateInstance(myConcreteClassType);
			
			Assert.IsNotNull(((MyConcreteClass)myConcreteClassInstance).InstancePorperty);


			if (myConcreteClassInstance is MyAbstractClass myConcreteClassBaseClassInstance)
			{
				Assert.IsNotNull(myConcreteClassBaseClassInstance);

				Type myConcreteClassBaseClassInstanceType = myConcreteClassBaseClassInstance.GetType();
				
				Assert.AreNotEqual(myConcreteClassBaseClassInstanceType, myAbstractClassType);
				Assert.AreEqual(myConcreteClassBaseClassInstanceType, myConcreteClassType);

				//Assert.IsNotNull(myConcreteClassBaseClassInstance.InstancePorperty);
				
			}
		}
	}

}
