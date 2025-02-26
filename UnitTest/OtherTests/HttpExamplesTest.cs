using C_SharpExamplesLib.OtherExamples;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.OtherTests
{
    [TestClass]
    public class HttpExamplesTest
    {
        [TestMethod]
        public void HttpRequestSimple()
        {
            DoHttpRequests.TestHttpRequestSimple();
        }

        [TestMethod]
        public void HttpRequestJson()
        {
            DoHttpRequests.TestHttpRequestJson();
        }

		[TestMethod]
		public void SerialisationWithReferences()
		{
			SerialisationExample.DoSerialisationWithReferences();
		}

		[TestMethod]
		public void DeserializeFile()
		{
			SerialisationExample.DeserializeFile();
		}

        [TestMethod]
        public void GenericDeserializeFile()
        {
            SerialisationExample.GenericDeserializeFile();
        }
    }
}
