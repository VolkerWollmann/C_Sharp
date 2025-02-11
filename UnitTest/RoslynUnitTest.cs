using C_SharpExamplesLib.Language.Roslyn;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class RoslynUnitTest
    {

        [TestMethod]
        public void Roslyn()
        {
            MyRoslyn.Test();
        }
    }
}
