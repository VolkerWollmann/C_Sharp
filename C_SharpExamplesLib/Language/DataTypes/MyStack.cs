using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language.DataTypes
{ 
    public class MyStackTest
    {
        // #stack #struct
        internal struct MyStackElement
        {
            public int X;
            public int Y;
            public MyStackElement(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
        internal class MyStack : Stack<MyStackElement>
        {
            public MyStack(int capacity) : base(capacity)
            {

            }
        }
        
        public static void Test()
        {

            MyStack stack = new MyStack(5);

            MyStackElement pair = new MyStackElement(1, 42);
            stack.Push(pair);

            Assert.AreEqual(1, stack.Count);

            var pair2 = stack.Pop();
            Assert.AreEqual(0, stack.Count);

            Assert.AreEqual(pair, pair2);

        }
    }
}