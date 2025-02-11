using C_SharpExamplesLib.Language.XML;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTest
{
	[TestClass]
	public class XmlTest
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
		public void XmlSerializer()
		{
			MyXml.XmlSerializerExample();
		}
	}
}
