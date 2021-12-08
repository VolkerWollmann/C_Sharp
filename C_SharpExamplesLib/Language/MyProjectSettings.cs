using System;
using System.Configuration;
using C_Sharp.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
    public class MyProjectSettings
    {

        public static void ReadSettings()
        {
            Settings settings = new Settings();

            var result = (string)settings.ProjectSetting;
            Assert.AreEqual( result, "ProjectSettingValue");
            //var appSettings = ConfigurationManager.AppSettings["UnitTestSetting"];
        }
    }
}
