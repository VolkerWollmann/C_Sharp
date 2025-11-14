using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpNew
{
    public abstract partial class CSharp11
    {
        public static void TestFileClasses2()
        {
            CSharp11DummyA cSharp11DummyA = new CSharp11DummyA();
            Assert.IsNotNull(cSharp11DummyA);

            //file class not accessible
            //CSharp11DummyB cSharp11DummyB = new CSharp11DummyB();
            //Assert.IsNotNull(cSharp11DummyB);
        }

    }
}