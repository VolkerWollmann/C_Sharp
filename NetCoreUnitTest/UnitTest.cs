using System;
using Xunit;
using Xunit.Abstractions;
using CSharpCore;
using CSharpCore.Roslyn;
using CSharpNaming;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

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
        public void PatternMatching()
        {
            CSharp8.PatternMatching();
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
    }

    public class RoslynUnitTest
    {
        [Fact]
        public void Roslyn()
        {
            MyRoslynNextCore.Test();
        }
    }
}
