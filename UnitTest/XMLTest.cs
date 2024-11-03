using C_Sharp.Language.XML;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTest
{
	[TestClass]
	public class XMLTest
	{
		[TestMethod]
		public void XmlElement()
		{
			MyXml.TestXmlElement();
		}

		[TestMethod]
		public void XmlFile()
		{
			MyXml.TestXmlFile();
		}

		[TestMethod]
		public void XmlNodeVsElement()
		{
			MyXml.TestXmlNodeVsElement();
		}

		[TestMethod]
		public void CheckXmlFileWithXsdFile()
		{
			MyXml.CheckXmlFileWithXsdFile();
		}

		[TestMethod]
		public void SerializeClassToXml()
		{
			MyXml.SerializeClassToXml();
		}
	}
}
