using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{ 
    public class MyStackTest
    {
        // #stack
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

            Assert.AreEqual(stack.Count, 1);

            var pair2 = stack.Pop();
            Assert.AreEqual(stack.Count, 0);

            Assert.AreEqual(pair, pair2);

        }
    }
}