using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp.Language;
using C_Sharp.Language.Event;


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

        [TestMethod]
        public void WeakEventDelegate()
        {
            MyWeakEventHandlerExample.Test();
        }
    }
}
