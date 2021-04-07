using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
    public class MyXml
    {
        // linq to xml
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
        public static XmlElement Create_MyAnimals_as_XmlElement()
        {
            XmlDocument xmlDocument = new XmlDocument();
            
            XmlElement myAnimals = xmlDocument.CreateElement("My_Animals");
            
            XmlElement donkey = xmlDocument.CreateElement("Animal_1");
            donkey.InnerText  = "Donkey";
            myAnimals.AppendChild(donkey);

            XmlElement dog = xmlDocument.CreateElement("Animal_2");
            dog.InnerText = "Dog";
            myAnimals.AppendChild(dog);

            return myAnimals;
        }
        public static void Test()
        {
            XElement myAnimalsAsXElement1 = Create_MyAnimals_as_XElement_1();
            XElement myAnimalsAsXElement2 = Create_MyAnimals_as_XElement_2();
            Assert.AreEqual(myAnimalsAsXElement1.ToString(), myAnimalsAsXElement2.ToString());

            XmlElement myAnimalsAsXmlElement = Create_MyAnimals_as_XmlElement();

            string s1 = myAnimalsAsXElement1.ToString().Replace("\r\n", "").Replace("  ","");
            string s2 = myAnimalsAsXmlElement.OuterXml;
            Assert.AreEqual(s1, s2);
        }
    }
}
