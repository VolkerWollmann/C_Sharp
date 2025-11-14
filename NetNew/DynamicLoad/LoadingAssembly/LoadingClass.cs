using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Reflection;
using IMyCalculatorInterface;

namespace LoadingAssembly
{
    public abstract class LoadingClass
    {
        // Enforce, that dll is nearby
        private static readonly MyCalculator.MyCalculator Dummy = new();
        public static void Execute()
        {
            Assert.IsNotNull(Dummy);

            // #Load the #assembly #dynamically
            Assembly assembly = Assembly.Load("MyCalculator");

            // get the classes by interface
            Type dli = typeof(IMyCalculator);
            Type desiredClass = assembly
                .GetTypes()
                .First(c => dli.IsAssignableFrom(c));

            IMyCalculator? dynamicCreatedInstance
                = Activator.CreateInstance(desiredClass)
                    as IMyCalculator;

            int result = dynamicCreatedInstance!.Add(1, 2);
            Assert.AreEqual(3, result);
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
            Assert.StartsWith("1.2.3-UnitTestInfo", fileVersionInfo.ProductVersion);
        }
    }
}