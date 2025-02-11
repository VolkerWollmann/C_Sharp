using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.XPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language.XML
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

        // xml 3.0 and lower
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

        static void CheckXmlFileWithXsdFileValidationCallback(object? sender, ValidationEventArgs e)
        {
	        if (e.Severity == XmlSeverityType.Warning)
	        {
		        Console.WriteLine($"Warning: {e.Message}");
	        }
	        else if (e.Severity == XmlSeverityType.Error)
	        {
		        Console.WriteLine($"Error: {e.Message}");
	        }
        }
		public static void CheckXmlFileWithXsdFile()
		{
			bool isValid = true;
			
			string xsdFile = "..\\..\\..\\..\\C_SharpExamplesLib\\Language\\XML\\TestData.xsd";
			string xmlFile = "..\\..\\..\\..\\C_SharpExamplesLib\\Language\\XML\\TestData.xml";


			XmlSchemaSet schemas = new XmlSchemaSet();
			schemas.Add("", xsdFile);

			XmlReaderSettings settings = new XmlReaderSettings
			{
				Schemas = schemas,
				ValidationType = ValidationType.Schema
			};
			settings.ValidationEventHandler += CheckXmlFileWithXsdFileValidationCallback;

			using (XmlReader reader = XmlReader.Create(xmlFile, settings))
			{
				try
				{
					string indent = "";
					while (reader.Read())
					{
						switch (reader.NodeType)
						{
							case XmlNodeType.Element:
								Console.WriteLine($"{indent} {reader.Name}");
                                indent = indent + "   ";
								break;
							case XmlNodeType.Text:
								Console.WriteLine($"{indent}{reader.Value}");
								break;
							case XmlNodeType.EndElement:
								indent = indent.Substring(0, indent.Length - 3);
								Console.WriteLine($"{indent} /{reader.Name}");
								break;
						}
					}
					Console.WriteLine("XML file is valid.");
				}
				catch (XmlException ex)
				{
					Console.WriteLine($"XML file is invalid: {ex.Message}");
                    isValid = false;
				}
			}
            
            Assert.AreEqual(true, isValid);
		}

		public class Animal(string name, string description)
		{
            public string Name { get; set; } = name;

            public string Description = description;

            public Animal? Friend { get; set; }

            // ReSharper disable once UnusedMember.Global
            public Animal() : this("", "")
            {
			}
		}
		public static void XmlSerializerExample()
		{
			Animal macchi = new Animal("Macchi", "Famous police donkey");
            Assert.AreEqual("Famous police donkey", macchi.Description);
			Animal amica = new Animal("Amica", "Friend of Macchi");
            macchi.Friend = amica;

			List<Animal> animals = [macchi, amica];

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Animal>));

            MemoryStream memoryStream = new MemoryStream();

			xmlSerializer.Serialize(memoryStream, animals);

			memoryStream.Position = 0;

			StreamReader reader = new StreamReader(memoryStream);
			string result = reader.ReadToEnd();
			reader.Close();
			memoryStream.Close();

            // Friend reference is resolved explicit
            Assert.AreEqual(2, Regex.Matches(result, amica.Name).Count);
		}
    }
}
