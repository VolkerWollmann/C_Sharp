using System;
using System.Configuration;
using C_Sharp.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
    public class MyProjectSettings
    {
        // #Project #setting
        public static void ReadSettings()
        {
            // this works
            Settings settings = new Settings();
            var result = (string)settings.ProjectSetting;
            Assert.AreEqual( result, "ProjectSettingValue");

            // liked this
            // var appSettings = ConfigurationManager.AppSettings["UnitTestSetting"];
        }
    }
}
