using System.Linq.Expressions;

namespace C_Sharp.Language.MyEnumerableIntegerRangeLibrary
{
    public interface IMyIntegerSet :  IEnumerable<int>
	{
        IMyIntegerSet GetFilteredSet(LambdaExpression lambdaExpression);

        int Sum();

        bool Any(LambdaExpression lambdaExpression);

        bool Any();

        void Dispose();

        IEnumerable<int> AsEnumerable();

	}
}
