using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpNew
{
    internal class CSharp11DummyA;
    file class CSharp11DummyB;
    public abstract partial class CSharp11Partial
    {
        public static void TestFileClasses1()
        {
            CSharp11DummyA cSharp11DummyA = new CSharp11DummyA();
            Assert.IsNotNull(cSharp11DummyA);

            CSharp11DummyB cSharp11DummyB = new CSharp11DummyB();
            Assert.IsNotNull(cSharp11DummyB);
        }
    }
}
