using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_SharpExamplesLib.Language;
using C_SharpExamplesLib.Language.DataAnnotations;

namespace UnitTest
{
    [TestClass]
    public class AnnotationsTests
    {
        [TestMethod]
        public void Validate()
        {
            MyDataAnnotations.Validate();
        }

    }
}
