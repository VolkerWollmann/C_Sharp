using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
    public class MyXML
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
            XElement Donkey = new XElement("Animal_1", "Donkey");
            XElement Dog = new XElement("Animal_2", "Dog");

            myAnimals.Add(Donkey);
            myAnimals.Add(Dog);

            return myAnimals;
        }

        // xml 3.0 an lower
        public static XmlElement Create_MyAnimals_as_XmlElement()
        {
            XmlDocument xmlDocument = new XmlDocument();
            
            XmlElement myAnimals = xmlDocument.CreateElement("My_Animals");
            
            XmlElement Donkey = xmlDocument.CreateElement("Animal_1");
            Donkey.InnerText  = "Donkey";
            myAnimals.AppendChild(Donkey);

            XmlElement Dog = xmlDocument.CreateElement("Animal_2");
            Dog.InnerText = "Dog";
            myAnimals.AppendChild(Dog);

            return myAnimals;
        }
        public static void Test()
        {
            XElement myAnimals_as_XElement_1 = Create_MyAnimals_as_XElement_1();
            XElement myAnimals_as_XElement_2 = Create_MyAnimals_as_XElement_2();
            Assert.AreEqual(myAnimals_as_XElement_1.ToString(), myAnimals_as_XElement_2.ToString());

            XmlElement myAnimals_as_XMLElement = Create_MyAnimals_as_XmlElement();

            string s1 = myAnimals_as_XElement_1.ToString().Replace("\r\n", "").Replace("  ","");
            string s2 = myAnimals_as_XMLElement.OuterXml;
            Assert.AreEqual(s1, s2);
        }
    }
}
