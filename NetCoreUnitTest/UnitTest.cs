using C_Sharp.Language.Roslyn;
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

        [Fact]
        public void PatternMatching()
        {
            CSharp8.PatternMatching();
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

        [Fact]
        public void Roslyn()
        {
            MyRoslynNextCore.Test();
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
