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

        [Fact]
        public void SwitchWithCaseGuards()
        {
            CSharp8.CaseGuards();
        }
    }
    public class CSharp9UnitTest
    {
        private readonly ITestOutputHelper _Output;

        public CSharp9UnitTest(ITestOutputHelper output)
        {
            this._Output = output;
        }

        [Fact]
        public void TestRecord()
        {
            CSharp9.TestRecord();
        }

        [Fact]
        public void ContentEquality()
        {
            CSharp9.ContentEquality(this._Output);
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

    }
}
