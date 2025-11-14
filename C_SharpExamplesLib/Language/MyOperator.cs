using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language
{
    // #Operator ?. (Null-Conditional Operator) ??
    public class MyOperator
    {
        private readonly string _donkey = "Donkey";
        private static void QuestionMarkOperator()
        {
            string? st = null;
            var t = st ?? "Rabbit";
            Assert.AreEqual("Rabbit", t);
            st = "Hedgehog";
            // ReSharper disable once ConstantNullCoalescingCondition
            var u = st ?? "Donkey";
            Assert.AreEqual("Hedgehog", u);

            MyOperator? myOperator11 = null;
            string? e1 = myOperator11?._donkey;
            Assert.IsNull(e1);

            myOperator11 = new MyOperator();
            string e2 = myOperator11._donkey;
            Assert.AreEqual(myOperator11._donkey, e2);

            string e3 = e1 + e2;
            Assert.AreEqual("Donkey", e3);

            // ReSharper disable once HeuristicUnreachableCode
            string s = (0 < 1) ? "always" : "never";
            Assert.AreEqual("always", s);
        }

        public static void Operator_Test()
        {
            QuestionMarkOperator();
        }
    }
}
