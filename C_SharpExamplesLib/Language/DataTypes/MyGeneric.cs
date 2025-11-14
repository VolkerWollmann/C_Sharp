using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language.DataTypes
{
    #region Generic Interface 
    // #interface #generic #default
    internal interface IMyRandomizer<T>
    {
        T GetRandomElement(List<T> list);
        List<T> GetShuffledList(List<T> list);
    }

    internal interface IMyIntRandomizer : IMyRandomizer<int>;

    // #generic
    internal class MyIntegerRandomizer : IMyIntRandomizer
    {
        private readonly Random _random = new();
        public int GetRandomElement(List<int> list)
        {
            int index = _random.Next(0, list.Count);
            return list[index];
        }

        public List<int> GetShuffledList(List<int> list)
        {
            List<int> result = [];
            List<int> work = [];
            list.ForEach(e => work.Add(e));
            while (work.Count > 0)
            {
                int index = _random.Next(0, work.Count);
                result.Add(work[index]);
                work.RemoveAt(index);
            }

            return result;
        }
    }

    internal class MyStringRandomizer : IMyRandomizer<string>
    {
        private readonly Random _random = new();
        public string GetRandomElement(List<string> list)
        {
            int index = _random.Next(0, list.Count);
            return list[index];
        }

        public List<string> GetShuffledList(List<string> list)
        {
            List<string> result = [];
            List<string> work = [];
            list.ForEach(e => work.Add(e));
            while (work.Count > 0)
            {
                int index = _random.Next(0, work.Count);
                result.Add(work[index]);
                work.RemoveAt(index);
            }

            return result;
        }
    }


    // #generic #method
    public abstract class MyGenericInterface
    {
        private static readonly Random Random = new();
        private static T GetRandomElement<T>(List<T> list)
        {
            int index = Random.Next(0, list.Count);
            return list[index];
        }

        private static List<T> GetShuffledList<T>(List<T> list)
        {
            List<T> result = [];
            List<T?> work = [];
            list.ForEach(e => work.Add(e));
            while (work.Count > 0)
            {
                int index = Random.Next(0, work.Count);
                result.Add(work[index]!);
                work[index] = default(T);
                work.RemoveAt(index);
            }

            return result;
        }

        public static void Test()
        {
            List<string> animals = ["Donkey", "Dog", "Seagull", "Cat", "Goat"];
            var oneAnimal = GetRandomElement(animals);
            Assert.IsNotNull(oneAnimal);

            var shuffledAnimals = GetShuffledList(animals);
            Assert.Contains(shuffledAnimals.First(), animals);

            List<int> numbers = [1, 2, 3, 4, 5];
            var number = GetRandomElement(numbers);
            Assert.IsTrue(number is >= 1 and <= 5);
            var shuffledNumbers = GetShuffledList(numbers);
            Assert.Contains(shuffledNumbers.First(), numbers);

            IMyRandomizer<string> myStringRandomizer = new MyStringRandomizer();
            oneAnimal = myStringRandomizer.GetRandomElement(animals);
            Assert.IsNotNull(oneAnimal);
            shuffledAnimals = myStringRandomizer.GetShuffledList(animals);
            CollectionAssert.AllItemsAreNotNull(shuffledAnimals);

            IMyRandomizer<int> myIntegerRandomizer = new MyIntegerRandomizer();
            number = myIntegerRandomizer.GetRandomElement(numbers);
#pragma warning disable MSTEST0032 // Assertion condition is always true
            Assert.IsNotNull(number);
#pragma warning restore MSTEST0032 // Assertion condition is always true
            shuffledNumbers = myIntegerRandomizer.GetShuffledList(numbers);
            CollectionAssert.AllItemsAreNotNull(shuffledNumbers);
        }
    }
    #endregion

    #region Generic class
    // #generic #type restriction
    internal class MyBaseClass
    {
        public int BaseClassMethod()
        {
            return 42;
        }
    }

    internal class RefinedClassA : MyBaseClass;

    internal class RefinedClassB : MyBaseClass;

    // be aware of type variable TGenericClassInstanceType in debugger
    internal class MyGenericClass<TGenericClassInstanceType> where TGenericClassInstanceType : MyBaseClass, new()
    {
        private readonly TGenericClassInstanceType _internalClass = new();
        public int GenericClassMethod()
        {
            return _internalClass.BaseClassMethod();
        }

    }

    public abstract class MyGenericClassTest
    {
        public static void Test()
        {
            var tA = new MyGenericClass<RefinedClassA>();
            var tB = new MyGenericClass<RefinedClassB>();

            Assert.AreEqual(42, tA.GenericClassMethod());
            Assert.AreEqual(42, tB.GenericClassMethod());

            // #Type checks comparison
            var tAType = tA.GetType();
            var tBType = tB.GetType();
            Assert.IsFalse(tAType == tBType);

            var tAGenericTypeDefinition = tA.GetType().GetGenericTypeDefinition();
            var tBGenericTypeDefinition = tB.GetType().GetGenericTypeDefinition();
            Assert.IsTrue(tAGenericTypeDefinition == tBGenericTypeDefinition);

            // #generic type information
            var tATypeGenericTypeArgument = tAType.GenericTypeArguments[0];
            var refinedClassAType = typeof(RefinedClassA);
            Assert.IsTrue(tATypeGenericTypeArgument == refinedClassAType);

            var myGenericClassTypeName = typeof(MyGenericClass<>);
            Assert.IsTrue(tAGenericTypeDefinition == myGenericClassTypeName);


            //will not Compile, because int is not derived from MyGenericClass
            //var t3 = new MyGenericClass<int>();

            //create dynamic valid generic class instance
            Type t4 = typeof(MyGenericClass<RefinedClassA>);
            var t5 = Activator.CreateInstance(t4);
            Assert.IsNotNull(t5);
            Assert.AreEqual(42, ((MyGenericClass<RefinedClassA>)t5).GenericClassMethod());

            //create dynamic invalid generic class instance
            Assert.Throws<ArgumentException>(() => { Type t6 = typeof(MyGenericClass<>).MakeGenericType(typeof(int)); Assert.IsNotNull(t6); });

        }
    }

    #endregion
}
