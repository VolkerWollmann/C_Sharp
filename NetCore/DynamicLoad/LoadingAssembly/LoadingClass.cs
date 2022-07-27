using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace LoadingAssembly
{
    public class LoadingClass
    {
        public static void Execute()
        {
            // #Load the #assembly #dynamically
            Assembly assembly = Assembly.Load("AssemblyToLoad");

            // get the classes by interface
            Type dli = typeof(DynamicLoadInterface.IDynamicLoadInterface);
            Type desiredClass = assembly
                .GetTypes()
                .First(c => dli.IsAssignableFrom(c));

            DynamicLoadInterface.IDynamicLoadInterface? dynamicCreatedInstance 
                = Activator.CreateInstance(desiredClass)
                  as DynamicLoadInterface.IDynamicLoadInterface;

            int result = dynamicCreatedInstance!.Add(1, 2);
            Assert.IsTrue(result == 3);
        }
    }
}