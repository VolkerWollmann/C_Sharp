using System;
using Xunit;
using Xunit.Abstractions;
using CSharpCore;
using CSharpCore.Roslyn;
using CSharpNaming;
using CSharpNew.ProcessCommunication;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using LoadingAssembly;

namespace NetCoreUnitTest
{
    public class CSharp8UnitTest
    {
        [Fact]
        public void RangeOperators()
        {
            CSharp8.RangeOperators();
        }

        [Fact]
        public void SwitchWithCaseGuards()
        {
            CSharp8.CaseGuards();
        }

        [Fact]
        public void PropertyPatternMatching()
        {
            CSharp8.PropertyPatternMatching();
        }

        [Fact]
        public void PositionalPatternMatching()
        {
            CSharp8.PositionalPatternMatching();
        }

        [Fact]
        public void TuplePatternMatching()
        {
            CSharp8.TuplePatternMatching();
        }

        // seems to not work, see unit 
        [Fact]
        //[ExpectedException(typeof(ArgumentNullException), "Expected Argument null exception")]
        public void NullForgivingOperator()
        {
            try
            {
                CSharp8.NullNameShouldThrowTest();
            }
            catch (ArgumentNullException ane)
            {
                Assert.AreNotEqual(ane, null);
                //throw ane;
            }
           
        }

        [Fact]
        public void NullCoalescingExample()
        {
            CSharp8.NullCoalescingExample();
        }
    }
    public class CSharp9UnitTest
    {
        private readonly ITestOutputHelper _output;

        public CSharp9UnitTest(ITestOutputHelper output)
        {
            this._output = output;
        }

        [Fact]
        public void TestRecord()
        {
            CSharp9.TestRecord();
        }

        [Fact]
        public void ContentEquality()
        {
            CSharp9.ContentEquality(this._output);
        }

        [Fact]
        public void TestInit()
        {
            CSharp9.TestInit();
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

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void TestTheoryExample(int i)
        {
            Assert.IsTrue(i< 2);
        }

        [Fact]
        public void TestDynamicLoad()
        {
            LoadingClass.Execute();
        }

        [Fact]
        public void StreamTestUnconventionalUsage()
        {
            CSharp.StreamTestUnconventionalUsage();
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
            CSharp8.Test_IDisposable();
        }

        [Fact]
        public void TestFloatingPointNumericTypes()
        {
	        CSharp.TestFloatingPointNumericTypes();
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

    public class HTTPListnerTest
    {
        [Fact]
        public void SelfHostestTest()
        {
           // ??? 
        }
    }

    public class ProcessCommunication
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
}
