using System;
using Xunit;
using CSharpCore;

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
        [Fact]
        public void TestRecord()
        {
            CSharp9.TestRecord();
        }
    }
}
