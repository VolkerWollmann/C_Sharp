using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp.Language.Task;


namespace UnitTest
{
    [TestClass]
    public class ParallelTest
    {
        [TestMethod]
        public void Parallel_GradeOfParallelism()
        {
            MyParallel.Parallel_GradeOfParallelism();
        }

        [TestMethod]
        public void Parallel_ParallelFor()
        {
            MyParallel.ParallelFor();
        }
    }
}
