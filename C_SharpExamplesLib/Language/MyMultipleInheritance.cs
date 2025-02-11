using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language
{

    class BaseClassA;

    class BaseClassB;

    // TODO : https://stackoverflow.com/questions/178333/multiple-inheritance-in-c-sharp
    // Does not Compile
    // class MyMultipleInheritance : BaseClassA, BaseClassB
	//{
		
	//}

    public class MyMultipleInheritanceTest
    {
        public static void Test()
        {
            var a = new BaseClassA();
            Assert.IsNotNull(a);

            var b = new BaseClassB();
            Assert.IsNotNull(b);
        }
    }
}
