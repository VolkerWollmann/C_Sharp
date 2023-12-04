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
            GetRadnomNumner.TestHttpRequestSimple();
        }

        [TestMethod]
        public void HttpRequest2()
        {
            GetRadnomNumner.TestHttpRequest2();
        }
    }
}
