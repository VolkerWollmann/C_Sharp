using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language.DataTypes
{
    public abstract class MySimpleTypes
    {
        public static void Test()
        {
            char c = 'c';

            Assert.AreEqual('c', c);
        }
    }
}
