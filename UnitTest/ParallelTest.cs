using C_SharpExamplesLib.Language.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;


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
