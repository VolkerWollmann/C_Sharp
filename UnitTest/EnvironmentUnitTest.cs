using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp.Language.Event;
using C_Sharp.Language;

namespace UnitTest
{
    [TestClass]
    public class EnvironmentUnitTest
    {

        [TestMethod]

        public void ReadSettings()
        {
            MyProjectSettings.ReadSettings();
        }

        [TestMethod]

        public void CurrentDirectory()
        {
            MyProjectSettings.CurrentDirectory();
        }
    }
}
