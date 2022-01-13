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
    }
}
