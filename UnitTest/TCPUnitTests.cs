using C_SharpExamplesLib.Language.TCP;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TCPUnitTests
{
    [TestClass]
    public class TcpUnitTest
    {
        [TestMethod]
        public void TCP_Call()
        {
            MYTCPTest.Test();
        }
    }
}
