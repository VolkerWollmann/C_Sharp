using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace C_SharpExamplesLib.Language.DataTypes
{
    public abstract class MyAssignable
    {
        private class C1
        {

        }

        private class C2 : C1
        {

        }

        public static void AssignChecks()
        {
            Type typeInt = typeof(int);

            bool result = typeInt.IsAssignableFrom(typeof(int));
            Assert.IsTrue(result);

            short s = 5;
            int i = s;

            result = typeInt.IsAssignableFrom(typeof(short));
            Assert.IsFalse(result);

            // better use for inbstances/classes
            C2 c2 = new C2();
            C1 c1 = c2;

            result = typeof(C1).IsAssignableFrom(typeof(C2));
            Assert.IsTrue(result);

        }
    }
}
