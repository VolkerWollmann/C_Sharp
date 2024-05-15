using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Sharp.Language;

namespace UnitTest
{
    [TestClass]
    public class LinqUnitTest
    {
		[TestMethod]
		public void Linq_Zip()
		{
			MyLinq.Linq_Zip ();
		}

		[TestMethod]
		public void Linq_FirstOrDefault()
		{
			MyLinq.Linq_FirstOrDefault();
		}

		[TestMethod]
		public void List_Range_Where_Take()
		{
			MyLinq.List_Range_Where_Take();
		}

		[TestMethod]
		public void Linq_SetOperation()
		{
			MyLinq.Linq_SetOperation();
		}

		[TestMethod]
		public void Linq_SelectMany()
		{
			MyLinq.Linq_SelectMany();
		}

        [TestMethod]
        public void Linq_Single()
        {
            MyLinq.Linq_Single();
        }

        [TestMethod]
		public void ParallelLinq()
		{
			MyLinq.TestParallelLinq();
		}

		[TestMethod]
		public void ParallelLinq_Exception()
        {
			MyLinq.PLinqExceptions();
		}

		[TestMethod]
		public void Linq_Syntax()
		{
			MyLinq.Linq_Syntax();
		}

		[TestMethod]
		public void Linq_OrderBy()
		{
			MyLinq.Linq_OrderBy();
		}

		[TestMethod]
		public void SelectImplicitType()
		{
			MyLinq.SelectImplicitType();

		}

		[TestMethod]
		public void Expression1()
		{
			MyLinqExpression.WalkExpression1();
		}

		[TestMethod]
		public void Expression2()
		{
			MyLinqExpression.WalkExpression2();
		}

        [TestMethod]
        public void LinkedList()
        {
            MyLinq.Linq_LinkedList();
        }

		[TestMethod]
		public void LinqChunking()
		{
			MyLinq.LinqChunking();
		}
	}
}
