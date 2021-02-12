using System;
using Xunit;
using Xunit.Abstractions;
using CSharpCore;
using CSharpNaming;

namespace NetCoreUnitTest
{
    public class CSharp8UnitTest
    {
        [Fact]
        public void RangeOperators()
        {
            CSharp8.RangeOperators();
        }
    }
    public class CSharp9UnitTest
    {
        private readonly ITestOutputHelper output;

        public CSharp9UnitTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestRecord()
        {
            CSharp9.TestRecord();
        }

        [Fact]
        public void ContentEquality()
        {
            CSharp9.ContentEquality(this.output);
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
    }
}
