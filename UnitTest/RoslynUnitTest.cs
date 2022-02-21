using C_Sharp.Language.Roslyn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp.Language.Thread;
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
