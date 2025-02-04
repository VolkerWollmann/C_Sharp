using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
	/// <summary>
	/// #abstract class
	/// </summary>
	abstract class MyAbstractClass
    {
        
        internal abstract int GetNumber();

    }

	class MyConcreteClass : MyAbstractClass
	{
		public string InstanceProperty { get; } = "Rabbit";

        internal override int GetNumber()
        {
            return 42;
        }
    }

	public class AbstractClassTest
	{
		public static void Test()
		{
			Type myAbstractClassType = typeof(MyAbstractClass);
			Type myConcreteClassType = typeof(MyConcreteClass);

            void CreateAbstractClassInstanceAction() => Activator.CreateInstance(myAbstractClassType);
            Assert.ThrowsException<MissingMethodException>(CreateAbstractClassInstanceAction);

			// create instance of concrete class by using type information
			object myConcreteClassInstance = Activator.CreateInstance(myConcreteClassType)!;
			Assert.IsNotNull(((MyConcreteClass)myConcreteClassInstance!).InstanceProperty);

			// check type with is operator and assign as base class
			if (myConcreteClassInstance is MyAbstractClass myConcreteClassBaseClassInstance)
			{
				// check that cast base class instance exists
				Assert.IsNotNull(myConcreteClassBaseClassInstance);

				// Get type of cast base class instance
				Type myConcreteClassBaseClassInstanceType = myConcreteClassBaseClassInstance.GetType();

				// GetType returns actual type and not cast type
				Assert.AreEqual(myConcreteClassBaseClassInstanceType, myConcreteClassType);
				Assert.AreNotEqual(myConcreteClassBaseClassInstanceType, myAbstractClassType);

                Assert.AreEqual(42, ((MyConcreteClass)myConcreteClassBaseClassInstance).GetNumber());

                Assert.AreEqual(42, myConcreteClassBaseClassInstance.GetNumber());
			}

        }
	}

}
