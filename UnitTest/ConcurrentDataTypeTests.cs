using System.Threading.Tasks;
using C_SharpExamplesLib.Language.ConcurrentDataTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        [TestMethod]
        public void ParallelArrayProcessing()
        {
            MyParallelArrayTest.Test();
        }

        [TestMethod]
        public void BlockingCollection()
        {
            MyBlockingCollection.BlockingCollection();
        }

        [TestMethod]
        public async Task Channel_Unbounded()
        {
            await MyChannel.Test_UnboundedChannel();
        }

        [TestMethod]
        public async Task Channel_BackPressure()
        {
            await MyChannel.Test_BoundedChannelBackPressure();
        }

        [TestMethod]
        public async Task Channel_MultipleConsumers()
        {
            await MyChannel.Test_MultipleConsumers();
        }

        [TestMethod]
        public void Channel_DropOldest()
        {
            MyChannel.Test_BoundedChannelDropsOldest();
        }
    }
}
