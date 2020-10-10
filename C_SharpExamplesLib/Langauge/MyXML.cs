using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace C_Sharp
{
    public class MyXML
    {
        // linq to xml
        private static XElement Create_MeineTiere_as_XElement_1()
        {
            return new XElement("Meine_Tiere",
                new XElement("Tier_1", "Esel"),
                new XElement("Tier_2", "Hund"));
        }

        private static XElement Create_MeineTiere_as_XElement_2()
        {
            XElement meineTiere = new XElement("Meine_Tiere");
            XElement esel = new XElement("Tier_1", "Esel");
            XElement hund = new XElement("Tier_2", "Hund");

            meineTiere.Add(esel);
            meineTiere.Add(hund);

            return meineTiere;
        }

        // xml 3.0 an lower
        public static XmlElement Create_MeineTiere_as_XmlElement()
        {
            XmlDocument xmlDocument = new XmlDocument();
            
            XmlElement meineTiere = xmlDocument.CreateElement("Meine_Tiere");
            
            XmlElement esel = xmlDocument.CreateElement("Tier_1");
            esel.InnerText  = "Esel";
            meineTiere.AppendChild(esel);

            XmlElement hund = xmlDocument.CreateElement("Tier_2");
            hund.InnerText = "Hund";
            meineTiere.AppendChild(hund);

            return meineTiere;
        }
        public static void Test()
        {
            XElement meineTiere_as_XElement_1 = Create_MeineTiere_as_XElement_1();
            XElement meineTiere_as_XElement_2 = Create_MeineTiere_as_XElement_2();
            Assert.AreEqual(meineTiere_as_XElement_1.ToString(), meineTiere_as_XElement_2.ToString());

            XmlElement meineTiere_as_XMLElement = Create_MeineTiere_as_XmlElement();

            string s1 = meineTiere_as_XElement_1.ToString().Replace("\r\n", "").Replace("  ","");
            string s2 = meineTiere_as_XMLElement.OuterXml;
            Assert.AreEqual(s1, s2);
        }
    }
}
