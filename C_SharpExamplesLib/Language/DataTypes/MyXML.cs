using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language.DataTypes
{
    // #xml
    public class MyXml
    {
        // #linq to #xml
        private static XElement Create_MyAnimals_as_XElement_1()
        {
            return new XElement("My_Animals",
                new XElement("Animal_1", "Donkey"),
                new XElement("Animal_2", "Dog"));
        }

        private static XElement Create_MyAnimals_as_XElement_2()
        {
            XElement myAnimals = new XElement("My_Animals");
            XElement donkey = new XElement("Animal_1", "Donkey");
            XElement dog = new XElement("Animal_2", "Dog");

            myAnimals.Add(donkey);
            myAnimals.Add(dog);

            return myAnimals;
        }

        // xml 3.0 an lower
        public static XmlDocument Create_MyAnimals_as_XmlDocument()
        {
            XmlDocument xmlDocument = new XmlDocument();
            
            XmlElement myAnimals = xmlDocument.CreateElement("My_Animals");
            xmlDocument.AppendChild(myAnimals);
            
            XmlElement donkey = xmlDocument.CreateElement("Animal_1");
            donkey.InnerText  = "Donkey";
            myAnimals.AppendChild(donkey);

            XmlElement dog = xmlDocument.CreateElement("Animal_2");
            dog.InnerText = "Dog";
            myAnimals.AppendChild(dog);

            return xmlDocument;
        }
        public static void TestXmlElement()
        {
            XElement myAnimalsAsXElement1 = Create_MyAnimals_as_XElement_1();
            XElement myAnimalsAsXElement2 = Create_MyAnimals_as_XElement_2();
            Assert.AreEqual(myAnimalsAsXElement1.ToString(), myAnimalsAsXElement2.ToString());

            XmlDocument myAnimalsAsXmlElement = Create_MyAnimals_as_XmlDocument();

            string s1 = myAnimalsAsXElement1.ToString().Replace("\r\n", "").Replace("  ","");
            string s2 = myAnimalsAsXmlElement.OuterXml;
            Assert.AreEqual(s1, s2);
        }


        // read/write #xml to/from #file
        public static void TestXmlFile()
        {
            XElement myAnimalsAsXElement1 = Create_MyAnimals_as_XElement_1();
            
            string tempFile = Path.GetTempFileName();
            myAnimalsAsXElement1.Save(tempFile);

            XElement myAnimalsFromFile = XElement.Load(tempFile);

            File.Delete(tempFile);

            string s1 = myAnimalsAsXElement1.ToString().Replace("\r\n", "").Replace("  ", "");
            string s2 = myAnimalsFromFile.ToString().Replace("\r\n", "").Replace("  ", "");
            Assert.AreEqual(s1, s2);
            
        }
    }
}
