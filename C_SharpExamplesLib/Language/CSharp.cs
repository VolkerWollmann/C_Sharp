using System.Collections;
using System.Net.Http.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language
{
    public abstract class CSharp
    {
        /// <summary>
        /// test
        /// #local method #local function
        /// </summary>
        public static void LocalFunction()
        {
            var one = GetOne();

            Assert.AreEqual(1, one);
            return;

            // ReSharper disable once LocalFunctionHidesMethod
            int GetOne()
            {
                return 1;
            }
        }

        public static void MultiLineStringConstant()
        {
            string animals = @"donkey
                               dog
                               cat";

            Assert.IsNotNull(animals);
        }

        #region Implict Operator
        public readonly struct Digit
        {
            private readonly byte _digit;

            public Digit(byte digit)
            {
                if (digit > 9)
                {
                    throw new ArgumentOutOfRangeException(nameof(digit), "Digit cannot be greater than nine.");
                }
                _digit = digit;
            }

            public static implicit operator byte(Digit d) => d._digit;
            public static explicit operator Digit(byte b) => new(b);

            public override string ToString() => $"{_digit}";
        }

        public static void ImplicitExplicitOperator()
        {
            var d = new Digit(7);

            // #implicit operator : statement would raise a compilation error without implicit cast operator
            byte number = d;
            Assert.AreEqual(7, number);

            //  #explicit operator : : statement would raise a compilation error without explicit cast operator
            Digit digit = (Digit)number;
            Assert.AreEqual(7, digit);
        }

        #endregion

        #region Named Parameter

        // #named #parameter #named parameter
        public static void NamedParameters()
        {
            int Sum(int parameter1, int parameter2)
            {
                return parameter1 + parameter2;
            }

            int result = Sum(parameter2: 5, parameter1: 3);

            Assert.AreEqual(8, result);
        }

        #endregion

        #region Optional Parameter
        // #optional parameter
        public static void OptionalParameters()
        {
            int Sum(int parameter1, int parameter2 = 3)
            {
                return parameter1 + parameter2;
            }

            int result = Sum(2);
            Assert.AreEqual(5, result);

            int result2 = Sum(1, 2);
            Assert.AreEqual(3, result2);

        }
        #endregion

        #region class initializer code
        private class ClassToBeInitialized
        {
            public int P;

        }
        public static void TestClassInitializer()
        {
            ClassToBeInitialized xxx = new ClassToBeInitialized() { P = 5 };
            Assert.AreEqual(5, xxx.P);
        }

        #endregion

        #region CallerMember
        // #CompilerServices #CallerMemberName #GetCurrentMethod
        public static void ShowCompilerServices()
        {
            TraceMessage(System.Reflection.MethodBase.GetCurrentMethod()?.Name ?? "Nothing to trace");
        }

        private static void TraceMessage(string message,
                [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            Assert.AreEqual("ShowCompilerServices", message);
            Assert.AreEqual("ShowCompilerServices", memberName);
            Assert.EndsWith("CSharp.cs", sourceFilePath);
            Assert.IsGreaterThan(0, sourceLineNumber);
        }

        #endregion

        #region Lazy
        // #lazy
        private class LazyClass
        {
            internal readonly int[] Array = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
            internal int Length => Array.Length;

            public LazyClass()
            {
                // For debbuging purposes
                Thread.Sleep(100);
            }
        }

        public static void LazyClassTest()
        {
            // Create Lazy.
            Lazy<LazyClass> lazy = new Lazy<LazyClass>();

            // Show that IsValueCreated is false.
            Assert.IsFalse(lazy.IsValueCreated);

            // Get the Value
            // ... This creates the instance of LazyClass.
            LazyClass test = lazy.Value;

            // Show the IsValueCreated is true.
            Assert.IsTrue(lazy.IsValueCreated);

            // The object can be used.
            Assert.IsGreaterThan(0, test.Length);
            Assert.AreEqual(1, test.Array[0]);
        }

        #endregion

        #region ToString

        private enum MyNumbers
        {
            FourtyTwo = 42,
        }
        /// #ToString()
        public static void ToStringExamples()
        {
            string iString = 1.ToString();
            Assert.AreEqual("1", iString);

            //Does not compile
            //string nullString = null.ToString();

            string constString = MyNumbers.FourtyTwo.ToString();
            Assert.AreEqual("FourtyTwo", constString);

        }
        #endregion

        #region #Recursive #Class

        public class MyRecursiveCLass
        {
            private static readonly MyRecursiveCLass Anchor = new();
            private static int _counter = 1;
            private readonly int _myCounter;

            private MyRecursiveCLass()
            {
                _myCounter = _counter++;
            }

            private void WriteData()
            {
                Console.WriteLine("MyCounter:" + _myCounter);

                // Don't comment this out
                if (Anchor == this)
                    return;

                // Cycle
                Anchor.WriteData();
            }

            /// <summary>
            /// Structure is not recursive, but you can step down infinitely
            /// </summary>
            public static void Test()
            {
                MyRecursiveCLass myRecursiveCLass = new MyRecursiveCLass();
                myRecursiveCLass.WriteData();
            }
        }
        #endregion

        #region Goto

        public static void GotoTest()
        {
            Random random = new Random();

            if (random.Next(0, 100) < 50)
                goto Label1;
            else
                goto Label2;

            // ReSharper disable once HeuristicUnreachableCode
#pragma warning disable CS0162 // Unreachable code detected
            throw new Exception("That must not happen");

        Label1:
            Console.WriteLine("Successful Test with Label 1.");
            goto Label3;

        Label2:
            Console.WriteLine("Successful Test with Label 2.");
            goto Label3;

            // ReSharper disable once HeuristicUnreachableCode
            throw new Exception("That must not happen");
#pragma warning restore CS0162 // Unreachable code detected

        Label3:
            Console.WriteLine("Test end.");

        }
        #endregion

        #region params

        /// #params : list of optional parameters of same type

        private static int ParamSumSimple(int[] values)
        {
            int sum = 0;
            values.ToList().ForEach(v => sum += v);

            return sum;
        }
        private static int ParamsSum(params int[] values)
        {
            int sum = 0;
            values.ToList().ForEach(v => sum += v);

            return sum;
        }

        public static void ParamsTest()
        {
            Assert.AreEqual(12, ParamSumSimple([2, 4, 6]));

            // will not compile
            //Assert.AreEqual(9, ParamSumSimple(1, 3, 5));

            Assert.AreEqual(12, ParamsSum(2, 4, 6));
            Assert.AreEqual(9, ParamsSum(1, 3, 5));

        }
        #endregion

        private static int _one;

        private static int GetOne()
        {
            return _one = 1;
        }

        // #return #assignment #expression
        public static void ReturnAssignment()
        {
            var lOne = GetOne();
            Assert.AreEqual(1, lOne);

            var lOne2 = _one;
            Assert.AreEqual(1, lOne2);
        }

        // string operations
        // #string #join #list of integers to a string
        public static void StringJoin()
        {
            List<int> ids1 = [1, 2, 3];

            string commaSeparatedIds1 = string.Join(", ", ids1);

            Assert.AreEqual("1, 2, 3", commaSeparatedIds1);

            List<int> ids2 = [1];

            string commaSeparatedIds2 = string.Join(", ", ids2);

            Assert.AreEqual("1", commaSeparatedIds2);
        }


        #region backing field
        private static int TheValue
        {
            set { TheValue = value;
                  TheValue = TheValue + 1;
            }
            get;
        }

        private static int _theValue2;

        private static int TheValue2
        {
            set
            {
                _theValue2 = value;
                _theValue2 = _theValue2 + 1;
            }

            get { return _theValue2; }
        }

        public static void PropertyWithBackingField()
        {
            TheValue2 = 5;
            Assert.AreEqual(6, TheValue2);

            // Will cause stack overflow exception, because the property is calling itself recursively without a backing field
            // This cannot be caught.
            //Assert.Throws<StackOverflowException>( ()=>
            //{ 
            //    TheValue = 5;
            //});
        }
        #endregion

        #region byte bitarray

        /// <summary>
        /// print the value
        /// </summary>
        /// <param name="myList">the bits</param>
        /// <param name="myWidth">bits per line</param>
        private static void PrintBitArrayValues(BitArray myList, int myWidth)
        {
            Console.WriteLine("   Values:");
            int i = myWidth;
            foreach (bool bit in myList)
            {
                if (i <= 0)
                {
                    i = myWidth;
                    Console.WriteLine();
                }
                i--;
                Console.Write("{0,1}", ((bit == false) ? 0 : 1));
            }
            Console.WriteLine();
        }

        public static void ShowBitArray()
        {
            byte[] myBytes = [1, 2, 3, 4, 5];
            BitArray myBitArray = new BitArray(myBytes);    // Internal representation is in integer

            Assert.HasCount(40, myBitArray);
            Assert.HasCount(40, myBitArray);
            PrintBitArrayValues(myBitArray, 32);

            int[] integers = [-1];
            BitArray myBitArray2 = new BitArray(integers);
            Assert.HasCount(32, myBitArray2);
            Assert.HasCount(32, myBitArray2);
            PrintBitArrayValues(myBitArray2, 32);

        }

        public static void DynamicType()
        {
            dynamic myDynamic = 1;
            Assert.AreEqual(1, myDynamic);

            myDynamic = "String";
            Assert.AreEqual("String", myDynamic);

            //will not compile
            //var myDynamic2 = 1;
            //Assert.AreEqual(1, myDynamic2);

            //myDynamic2 = "String";
            //Assert.AreEqual("String", myDynamic2);
        }

        #endregion
    }



}
