using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp.Language.Task;

namespace UnitTest
{
    [TestClass]
    public class ConcurrentDataTypeTest
    {
        [TestMethod]
        public void Lock_Granularity()
        {
            MyLock.SimpleTotal.TestSimpleTotal();
            MyLock.SharedTotal.TestTaskObjectLock();
        }

        [TestMethod]
        public void ConcurrentBag()
        {
            MyConcurrentBag.ParallelInsert();
        }

        [TestMethod]
        public void ConcurrentDictionary()
        {
            MyConcurrentDictionary.Test_ConcurrentDictionary();
        }


        [TestMethod]
        public void ConcurrentQueue()
        {
            MyConcurrentQueue.Test_ConcurrentQueue();
        }

        [TestMethod]
        public void ConcurrentStack()
        {
            MyConcurrentStack.Test_ConcurrentStack();
        }


    }
}
