using System.Linq.Expressions;
using C_Sharp.Language.MyEnumerableIntegerRangeLibrary;

namespace C_Sharp.Language.IQueryable2
{
    public class MyQueryableFactory
    {
        public static IQueryable<int> GetMyQueryable(IMyIntegerSet myIntegerSet)
        {
            return new MyIntegerSetQueryable2(myIntegerSet);
        }

        public static IQueryable<TType> GetMyConditionalEnumeratorQueryable2<TType>(
            IEnumerator<TType> enumerator, MethodCallExpression? whereExpression)
        {
            MyConditonalEnumeratorQueryable2<TType> x = new MyConditonalEnumeratorQueryable2<TType>(
                    enumerator, whereExpression);
            return x;
        }
    }
}
