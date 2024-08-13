using C_Sharp.OhterExamples;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
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
        public void HttpRequestJSON()
        {
            DoHttpRequests.TestHttpRequestJSON();
        }

		[TestMethod]
		public void Serialisation()
		{
			SerialisationExample.DoSerialisation();
		}
	}
}
