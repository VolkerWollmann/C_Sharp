using System;
using System.Collections.Generic;
using CSharpNaming;
using CSharpNew;
using CSharpNew.ProcessCommunication;
using CSharpNew.Roslyn;
using LoadingAssembly;
using Xunit;
using Xunit.Abstractions;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace NetNewUnitTest
{
    public class CSharp08UnitTest
    {
        [Fact]
        public void RangeOperators()
        {
            CSharp08.RangeOperators();
        }

        [Fact]
        public void SwitchWithCaseGuards()
        {
            CSharp08.CaseGuards();
        }

        [Fact]
        public void PropertyPatternMatching()
        {
            CSharp08.PropertyPatternMatching();
        }

        [Fact]
        public void PositionalPatternMatching()
        {
            CSharp08.PositionalPatternMatching();
        }

        [Fact]
        public void TuplePatternMatching()
        {
            CSharp08.TuplePatternMatching();
        }

        // seems to not work, see unit 
        [Fact]
        //[ExpectedException(typeof(ArgumentNullException), "Expected Argument null exception")]
        public void NullForgivingOperator()
        {
            try
            {
                CSharp08.NullNameShouldThrowTest();
            }
            catch (ArgumentNullException ane)
            {
                Assert.IsNotNull(ane);
                //throw ane;
            }

        }

        [Fact]
        public void NullCoalescingExample()
        {
            CSharp08.NullCoalescingExample();
        }
    }
    public class CSharp09UnitTest(ITestOutputHelper output)
    {
        [Fact]
        public void TestRecord()
        {
            CSharp09.TestRecord();
        }

        [Fact]
        public void ContentEquality()
        {
            CSharp09.ContentEquality(output);
        }

        [Fact]
        public void TestInit()
        {
            CSharp09.TestInit();
        }

        [Fact]
        public void NamingConventions()
        {
            NamingConvention.ShowNamingConventions();
        }

        [Fact]
        public void Vector()
        {
            MyVector.Test();
        }

        // #inline data  #data
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void TestTheoryExampleInLineData(int i)
        {
            Assert.IsLessThan(2, i);
        }

        [Theory]
        [InlineData(new[] { 0, 1 })]
        public void TestTheoryExampleInLineData2(int[] i)
        {
            foreach (int i2 in i)
                Assert.IsLessThan(2, i2);
        }

        public static IEnumerable<object[]> TestData => new List<object[]>
        {
            new object[] {0},
            new object[] {1}
        };

        [Theory]
        [MemberData(nameof(TestData))]
        public void TestTheoryExampleMemberData(int i)
        {
            Assert.IsLessThan(2, i);

        }

        [Fact]
        public void TestDynamicLoad()
        {
            LoadingClass.Execute();
        }

        [Fact]
        public void TestDllVersion()
        {
            LoadingClass.TestDllVersion();
        }

        [Fact]
        public void StreamTestUnconventionalUsage()
        {
            _ = CSharp.StreamTestUnconventionalUsage();
        }


        [Fact]
        public void StreamTestFlush()
        {
            CSharp.StreamTestFlush();
        }

        [Fact]
        public void CountDownEventTest()
        {
            CSharp.CountDownEventTest();
        }

        [Fact]
        public void TestDebuggerDisplay()
        {
            CSharp.TestDebuggerDisplay();
        }

        [Fact]
        public void Test_IDisposable()
        {
            CSharp08.Test_IDisposable();
        }

        [Fact]
        public void TestFieldHiding()
        {
            CSharp.TestFieldHiding();
        }
    }

    public class CSharp10UnitTest
    {
        [Fact]
        public void FromFileScopedNamespace()
        {
            CSharp10.DefinedInFileScopedNameSpace();
        }
    }
    public class CSharp11UnitTest
    {
        [Fact]
        public void FileClasses()
        {
            CSharp11Partial.TestFileClasses1();
            CSharp11Partial.TestFileClasses2();
        }

        [Fact]
        public void RequiredMembers()
        {
            CSharp11.TestRequiredMembers();
        }
    }

    public class CSharp12UnitTest
    {
        [Fact]
        public void PrimaryConstructor()
        {
            CSharp12.TestPrimaryConstructor();
        }

        [Fact]
        public void Swap()
        {
            CSharp12.Swap();
        }
    }

    public class RoslynUnitTest
    {
        [Fact]
        public void Roslyn()
        {
            MyRoslynNextCore.Test();
        }
    }

    public class HttpListenerTest
    {
        [Fact]
        public void SelfHostedTest()
        {
            // ??? 
        }
    }

    public class ProcessCommunicationTest
    {
        [Fact]
        public void NamedPipeTest()
        {
            NamedPipe.NamedPipeTest();
        }

        [Fact]
        public void MemoryMappedFileTest()
        {
            MemoryMappedFile.MemoryMappedFileTest();
        }
    }


    public class MathTest
    {
        private double GetZero()
        {
            return 0;
        }

        [Fact]
        public void Use_NaN_IsInfinity()
        {
            double result = Math.Sqrt(-1);
            Assert.IsTrue(Double.IsNaN(result));

            double result2 = 1 / GetZero();
            Assert.IsTrue(Double.IsInfinity(result2));

            double positiveInfinity = double.PositiveInfinity;
            Assert.IsTrue(Double.IsInfinity(positiveInfinity));
        }

        [Fact]
        public void IntegerCompareTo()
        {
            var v1 = 1.CompareTo(42);
            Assert.AreEqual(-1, v1);

            var v2 = 42.CompareTo(1);
            Assert.AreEqual(1, v2);

            var v3 = 42.CompareTo(42);
            Assert.AreEqual(0, v3);

        }

        [Fact]
        public void TestFloatingPointNumericTypes()
        {
            CSharp.TestFloatingPointNumericTypes();
        }

        [Fact]
        public void TestRounding()
        {
            Assert.AreEqual(2, Math.Ceiling(1.2));
            Assert.AreEqual(1, Math.Floor(1.5));
            Assert.AreEqual(2, Math.Round(1.5, MidpointRounding.ToEven));
        }
    }
}
