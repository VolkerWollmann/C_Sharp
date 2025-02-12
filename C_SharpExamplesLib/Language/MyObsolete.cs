// ReSharper disable UnusedMember.Global
namespace C_SharpExamplesLib.Language
{
    internal class MyObsoleteUsed
    {
        [Obsolete("Use NewMethod", true)]
        internal void OldMethod()
        {

        }

        [Obsolete("Use NewMethod",false)]
        internal void Method()
        {

        }

        internal void NewMethod()
        {

        }
    }
    public abstract class MyObsolete
    {
        public static void Test()
        {
            MyObsoleteUsed myObsoleteUsed = new MyObsoleteUsed();

            //will raise compile error
            //myObsoleteUsed.OldMethod();

            //will raise compile warning
            //myObsoleteUsed.Method();

            myObsoleteUsed.NewMethod();
        }
    }
}
