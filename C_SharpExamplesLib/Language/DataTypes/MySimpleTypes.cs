using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language.DataTypes
{
    public class MySimpleTypes
    {
        public static void Test()
        {
            char c = 'c';

            Assert.AreEqual('c', c);
        }
    }
}
