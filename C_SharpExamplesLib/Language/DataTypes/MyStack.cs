using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language.DataTypes
{
    public abstract class MyStackTest
    {
        // #stack #struct
        private struct MyStackElement(int x, int y)
        {
            public readonly int X = x;
            public readonly int Y = y;
        }

        private class MyStack(int capacity) : Stack<MyStackElement>(capacity);

        public static void Test()
        {

            MyStack stack = new MyStack(5);

            MyStackElement pair = new MyStackElement(1, 42);
            Assert.AreEqual(1, pair.X);
            Assert.AreEqual(42, pair.Y);
            stack.Push(pair);

            Assert.HasCount(1, stack);

            var pair2 = stack.Pop();
            Assert.IsEmpty(stack);

            Assert.AreEqual(pair, pair2);

        }
    }
}