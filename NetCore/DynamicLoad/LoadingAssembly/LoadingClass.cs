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
            Type dli = typeof(DynamicLoadInterface.DynamicLoadInterface);
            Type desiredClass = assembly.GetTypes()
                .Where(c => dli.IsAssignableFrom(c))
                .First();

            DynamicLoadInterface.DynamicLoadInterface? dynamicCreatedInstance 
                = Activator.CreateInstance(desiredClass)
                  as DynamicLoadInterface.DynamicLoadInterface;

            int result = dynamicCreatedInstance!.Add(1, 2);
            Assert.IsTrue(result == 3);
        }
    }
}