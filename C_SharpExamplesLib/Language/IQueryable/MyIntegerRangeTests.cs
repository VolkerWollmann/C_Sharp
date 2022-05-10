using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
    public class MyIntegerRangeTest
    {

        public static void Test_IQueryable_as_IEnumerable()
        {
            // uses public IEnumerator<int> GetEnumerator()
            // uses public bool MoveNext()
            // uses int IEnumerator<int>.Current
            MyIntegerRange myIntegerRange = new MyIntegerRange(1, 10);
            foreach (int i in myIntegerRange)
            {
                if (i > 5)
                    break;
            }

            // myIntegerRange stands at 6
            // uses int IEnumerator<int>.Current
            var a = ((IEnumerator<int>)myIntegerRange).Current;
            Assert.AreEqual(a, 6);

            // uses object IEnumerator.Current
            var b = ((IEnumerator)myIntegerRange).Current;
            Assert.IsNotNull(b);

            // does work
            // uses public IEnumerator<int> GetEnumerator()
            var d = myIntegerRange.ToList();
            Assert.IsNotNull(d);
        }

        public static void Test_IQueryable()
        {
            MyIntegerRange myIntegerRange = new MyIntegerRange(1, 10);

            // does work
            // uses private any implementation
            var d1 = myIntegerRange.Any();

            // does work
            // uses private conditional any implementation
            var d2 = myIntegerRange.Any(i => i > 15);

            // does work 
            // uses public Expression Expression
            // uses public TResult Execute<TResult>(Expression expression)
            var f = myIntegerRange.Sum();
            Assert.IsTrue(f > 0);

            //does work
            // uses public Expression Expression
            // uses public IQueryable<T> CreateQuery<T>(Expression expression)
            var e = myIntegerRange.Where(i => (i < 5)).ToList();
            Assert.IsNotNull(e.Count == 4);

            //does work lazy evaluation
            // uses public Expression Expression
            // uses public IQueryable<T> CreateQuery<T>(Expression expression)
            var g1 = myIntegerRange.Where(i => (i < 5));
            var g2 = g1.ToList();
            Assert.IsTrue(g2.Count == 4);
        }

        public static void Test_MultipleExpressions()
        {
            //does work lazy evaluation
            //CreateQuery creates a copy  
            //treat more like expressions
            MyIntegerRange myIntegerRange = new MyIntegerRange(1, 10);
            var g1 = myIntegerRange.Where(i => (i < 5));
            var g2 = myIntegerRange.Where(i => (i < 3));

            var g1r = g1.ToList();
            Assert.IsTrue(g1r.Count == 4);
            var g2r = g2.ToList();
            Assert.IsTrue(g2r.Count == 2);
        }


        public static void Test_CascadedExpressions()
        {
            //does work lazy evaluation
            //CreateQuery creates a copy  
            //treat more like expressions
            MyIntegerRange myIntegerRange = new MyIntegerRange(1, 10);
            var g1 = myIntegerRange.Where(i => (i % 2 == 0));
            var g2 = g1.Where(i => (i <= 6));

            var g1r = g1.ToList();
            Assert.IsTrue(g1r.Count == 5);
            var g2r = g2.ToList();
            Assert.IsTrue(g2r.Count == 3);
        }

        public static void Test_ProjectionExpression()
        {
            MyIntegerRange myIntegerRange = new MyIntegerRange(1, 5);
            var g1 = myIntegerRange.Where(i => (i <= 3));
            var g2 = g1.Select(x => new Tuple<int, char>(2 * x, 'A'));
            var g3 = g2.ToList();
            Assert.IsTrue(g3.Last().Item1 == 6);
        }

        public static void Test_Test()
        {
            List<int> l = new List<int>() { 1, 2, 3, 4, 5 };
            var g1 = l.Where(i => (i <= 3));
            var g2 = g1.Select(x => new Tuple<int, char>(2 * x, 'A'));
            var g3 = g2.ToList();
            Assert.IsTrue(g3.Last().Item1 == 3);
        }
    }
}
