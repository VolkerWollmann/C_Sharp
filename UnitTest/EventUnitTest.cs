using C_SharpExamplesLib.Language.Event;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTest
{
    [TestClass]

    public class EventUnitTest
    {
        [TestMethod]

        public void ActionDelegate()
        {
            MyEvent.ActionDelegate();
        }

    }
}
