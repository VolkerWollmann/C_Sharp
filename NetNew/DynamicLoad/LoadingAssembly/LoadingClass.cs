using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Reflection;
using IMyCalculatorInterface;
using MyCalculator;

namespace LoadingAssembly
{
    public abstract class LoadingClass
    {
        // Enforce, that dll is near by
        private static MyCalculator.MyCalculator dummy = new MyCalculator.MyCalculator();
        public static void Execute()
        {
            Assert.IsNotNull(dummy);
            
            // #Load the #assembly #dynamically
            Assembly assembly = Assembly.Load("MyCalculator");

            // get the classes by interface
            Type dli = typeof(IMyCalculatorInterface.IMyCalculator);
            Type desiredClass = assembly
                .GetTypes()
                .First(c => dli.IsAssignableFrom(c));

            IMyCalculatorInterface.IMyCalculator? dynamicCreatedInstance
                = Activator.CreateInstance(desiredClass)
                    as IMyCalculatorInterface.IMyCalculator;

            int result = dynamicCreatedInstance!.Add(1, 2);
            Assert.IsTrue(result == 3);
        }

        public static void TestDllVersion()
        {
            Assembly t = Assembly.Load("IMyCalculatorInterface");
            
            // #assembly #version info defined in csproj-File
            Assembly assembly = Assembly.Load("MyCalculator");

            // Get version information
            Version version = assembly.GetName().Version!;
            Assert.AreEqual(1, version.Major);
            Assert.AreEqual(2, version.Minor);
            Assert.AreEqual(4, version.Revision);
            
            // Get the assembly's full path
            string assemblyPath = assembly.Location;
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assemblyPath);
            Assert.AreEqual("1.2.3.4", fileVersionInfo.FileVersion);
            Assert.IsTrue(fileVersionInfo.ProductVersion!.StartsWith("1.2.3-UnitTestInfo"));
        }
    }
}