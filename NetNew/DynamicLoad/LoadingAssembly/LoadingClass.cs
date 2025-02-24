using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Reflection;

namespace LoadingAssembly
{
    public abstract class LoadingClass
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

        public static void TestDllVersion()
        {
            // #assembly #version info defined in csproj-File
            Assembly assembly = Assembly.Load("AssemblyToLoad");

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