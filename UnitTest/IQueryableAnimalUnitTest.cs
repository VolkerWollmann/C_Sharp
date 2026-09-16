using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_SharpExamplesLib.Language.IQueryable;
using MyEnumerableIntegerRangeLibrary;

namespace UnitTest
{
    /// <summary>
    /// Lazy linq queries on the animal table
    /// <code>
    /// Nr  Name      Art    Futter
    /// 1   Macci     Esel   Möhre
    /// 2   Amica     Hund   Knochen
    /// 3   Heidi     Ziege  Gräser
    /// 4   Fridolin  Möwe   Fisch
    /// </code>
    /// </summary>
    [TestClass]
    public class IQueryableAnimalUnitTest
    {
        private MyAnimalSetFactory _myAnimalSetFactory;
        private List<IMySet<MyAnimal>> _myAnimalSets;

        [TestInitialize]
        public void Initialize()
        {
            _myAnimalSetFactory = new MyAnimalSetFactory();
            _myAnimalSets = _myAnimalSetFactory.GetAnimalSets();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _myAnimalSetFactory.Dispose();
        }

        private static IMyDisposeQueryable<MyAnimal> GetMyQueryable(IMySet<MyAnimal> myAnimalSet)
        {
            return new MyEnumeratorQueryable<MyAnimal>(myAnimalSet.GetEnumerator());
        }

        [TestMethod]
        public void Test_ToList()
        {
            foreach (IMySet<MyAnimal> myAnimalSet in _myAnimalSets)
            {
                using var myQueryableAnimalSet = GetMyQueryable(myAnimalSet);
                List<MyAnimal> result = myQueryableAnimalSet.ToList();

                CollectionAssert.AreEqual(MyAnimalSetFactory.InitialValues, result);
            }
        }

        [TestMethod]
        public void Test_Where_Select_Tuple()
        {
            foreach (IMySet<MyAnimal> myAnimalSet in _myAnimalSets)
            {
                using var myQueryableAnimalSet = GetMyQueryable(myAnimalSet);
                var result = myQueryableAnimalSet
                    .Where(a => a.Name == "Macci")
                    .Select(e => Tuple.Create("Heute auf dem Speiseplan", e.Futter))
                    .ToList();

                Assert.HasCount(1, result);
                Assert.AreEqual(Tuple.Create("Heute auf dem Speiseplan", "Möhre"), result[0]);
            }
        }

        [TestMethod]
        public void Test_Where_Select_String()
        {
            foreach (IMySet<MyAnimal> myAnimalSet in _myAnimalSets)
            {
                using var myQueryableAnimalSet = GetMyQueryable(myAnimalSet);
                var result = myQueryableAnimalSet
                    .Where(a => a.Name == "Macci")
                    .Select(e => "Heute auf dem Speiseplan: " + e.Futter)
                    .ToList();

                CollectionAssert.AreEqual(new List<string> { "Heute auf dem Speiseplan: Möhre" }, result);
            }
        }

        [TestMethod]
        public void Test_Where_UnknownName_IsEmpty()
        {
            foreach (IMySet<MyAnimal> myAnimalSet in _myAnimalSets)
            {
                using var myQueryableAnimalSet = GetMyQueryable(myAnimalSet);
                var result = myQueryableAnimalSet.Where(a => a.Name == "Macchi").ToList();

                Assert.IsEmpty(result);
            }
        }

        [TestMethod]
        public void Test_Where_Count()
        {
            foreach (IMySet<MyAnimal> myAnimalSet in _myAnimalSets)
            {
                using var myQueryableAnimalSet = GetMyQueryable(myAnimalSet);
                int count = myQueryableAnimalSet.Where(a => a.Nr >= 2).Count();

                Assert.AreEqual(3, count);
            }
        }

        [TestMethod]
        public void Test_Where_Any()
        {
            foreach (IMySet<MyAnimal> myAnimalSet in _myAnimalSets)
            {
                using var myQueryableAnimalSet = GetMyQueryable(myAnimalSet);

                Assert.IsTrue(myQueryableAnimalSet.Any(a => a.Art == "Möwe"));
                Assert.IsFalse(myQueryableAnimalSet.Any(a => a.Art == "Katze"));
            }
        }

        [TestMethod]
        public void Test_First()
        {
            foreach (IMySet<MyAnimal> myAnimalSet in _myAnimalSets)
            {
                using var myQueryableAnimalSet = GetMyQueryable(myAnimalSet);
                MyAnimal first = myQueryableAnimalSet.First();

                Assert.AreEqual(MyAnimalSetFactory.Macci, first);
            }
        }

        [TestMethod]
        public void Test_Where_First()
        {
            foreach (IMySet<MyAnimal> myAnimalSet in _myAnimalSets)
            {
                using var myQueryableAnimalSet = GetMyQueryable(myAnimalSet);
                MyAnimal first = myQueryableAnimalSet.Where(a => a.Futter == "Fisch").First();

                Assert.AreEqual(MyAnimalSetFactory.Fridolin, first);
            }
        }

        [TestMethod]
        public void Test_Select_Names()
        {
            foreach (IMySet<MyAnimal> myAnimalSet in _myAnimalSets)
            {
                using var myQueryableAnimalSet = GetMyQueryable(myAnimalSet);
                var names = myQueryableAnimalSet.Select(a => a.Name).ToList();

                CollectionAssert.AreEqual(new List<string> { "Macci", "Amica", "Heidi", "Fridolin" }, names);
            }
        }
    }
}
