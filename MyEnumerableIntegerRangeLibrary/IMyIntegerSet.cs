using System.Linq.Expressions;

namespace C_Sharp.Language.MyEnumerableIntegerRangeLibrary
{
    public interface IMyIntegerSet :  IEnumerable<int>
	{
        void Dispose();

	}
}
