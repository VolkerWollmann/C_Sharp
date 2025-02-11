using System.Configuration;
using C_Sharp.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language
{
    public class MyProjectSettings
    {
        // #Project #setting
        public static void ReadSettings()
        {
            // this works
            Settings settings = new Settings();
            var result = settings.ProjectSetting;
            Assert.AreEqual("ProjectSettingValue", result);

            // liked this
            //var appSettings = ConfigurationManager.AppSettings["UnitTestSetting"];

            // #ConfigurationManager
            Configuration configuration =  ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            Assert.IsTrue(configuration.FilePath.Contains("UnitTest"));
        }

        public static void CurrentDirectory()
        {
            var directory = Directory.GetCurrentDirectory();
            Assert.IsTrue(directory.Contains("bin"));
        }
    }
}
