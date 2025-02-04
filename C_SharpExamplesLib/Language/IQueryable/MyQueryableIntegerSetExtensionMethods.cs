using System.Linq.Expressions;

namespace C_Sharp.Language.IQueryable
{
    public static class MyQueryableIntegerSetExtensionMethods
    {
        #region Extension methods
        public static int Sum(this MyQueryableIntegerSet<int> myQueryableIntegerSet)
        {
            return myQueryableIntegerSet.SumImplementation();
        }

        public static bool Any(this MyQueryableIntegerSet<int> myQueryableIntegerSet, LambdaExpression lambdaExpression)
        {
            return myQueryableIntegerSet.AnyImplementation(lambdaExpression);
        }

        public static bool Any(this MyQueryableIntegerSet<int> myQueryableIntegerSet, Expression<Func<int, bool>> predicate)
        {
            return myQueryableIntegerSet.AnyImplementation(predicate);
        }

        #endregion
    }
}
