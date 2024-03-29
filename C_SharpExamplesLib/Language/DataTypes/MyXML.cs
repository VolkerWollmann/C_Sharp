﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language.DataTypes
{
    // #xml
    public class MyXml
    {
        // #linq to #xml
        private static XElement Create_MyAnimals_as_XElements_1()
        {
            return new XElement("My_Animals",
                new XElement("Animal_1", "Donkey"),
                new XElement("Animal_2", "Dog"));
        }

        private static XElement Create_MyAnimals_as_XElements_2()
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
            XElement myAnimalsAsXElements1 = Create_MyAnimals_as_XElements_1();
            XElement myAnimalsAsXElements2 = Create_MyAnimals_as_XElements_2();
            Assert.AreEqual(myAnimalsAsXElements1.ToString(), myAnimalsAsXElements2.ToString());

            XmlDocument myAnimalsAsXmlElements = Create_MyAnimals_as_XmlDocument();

            string s1 = myAnimalsAsXElements1.ToString().Replace("\r\n", "").Replace("  ","");
            string s2 = myAnimalsAsXmlElements.OuterXml;
            Assert.AreEqual(s1, s2);

            // #XNode #XPathSelectElements
            List<XElement> l = myAnimalsAsXElements1.XPathSelectElements("./Animal_2").ToList();
            Assert.IsTrue( l[0].Value=="Dog");

        }

        // #XNode vs #XElement
        public static void TestXmlNodeVsElement()
        {
            XDocument doc = XDocument.Parse("<root><el1 />some text<!-- comment --></root>");
            if (doc.Root != null)
            {
                var nodes = doc.Root.Nodes().ToList();
                Assert.IsTrue(nodes.Count == 3);
                Assert.AreEqual("el1", ((XElement)nodes[0]).Name);

                var elements = doc.Root.Elements().ToList();
                Assert.IsTrue(elements.Count == 1);
                Assert.AreEqual("el1", elements[0].Name);
            }
        }


        // read/write #xml to/from #file
        public static void TestXmlFile()
        {
            XElement myAnimalsAsXElement1 = Create_MyAnimals_as_XElements_1();
            
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
