using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp.Language;


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
