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
                Assert.AreNotEqual(ane, null);
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
			Assert.IsTrue(i < 2);
		}

		[Theory]
		[InlineData(new[] { 0, 1 })]
		public void TestTheoryExampleInLineData2(int[] i)
		{
			foreach (int i2 in i)
				Assert.IsTrue(i2 < 2);
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
			Assert.IsTrue(i < 2);

		}

		[Fact]
        public void TestDynamicLoad()
        {
            LoadingClass.Execute();
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

    public class CSharp11UnitTest
    {
	    [Fact]
	    public void FileClasses()
	    {
            CSharp11.TestFileClasses1();
            CSharp11.TestFileClasses2();
	    }
    }

    public class CSharp12UnitTest
    {
	    [Fact]
	    public void PrimaryConstructor()
	    {
		    CSharp12.TestPrimaryConstructor();
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
	    public void NaNUse()
	    {
		    double result = Math.Sqrt(-1);
		    Assert.IsTrue(Double.IsNaN(result));

		    double result2 = 1 / GetZero();
			Assert.IsTrue(Double.IsInfinity(result2));
	    }

	    [Fact]
	    public void TestFloatingPointNumericTypes()
	    {
		    CSharp.TestFloatingPointNumericTypes();
	    }
	}
}
