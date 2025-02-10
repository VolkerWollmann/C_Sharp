using System.Linq.Expressions;
using MyEnumerableIntegerRangeLibrary;

namespace C_SharpExamplesLib.Language.IQueryable
{
    public abstract class MyQueryableFactory
    {
        public static IMyDisposeQueryable<TType> GetMyConditionalEnumeratorQueryable<TType>(
            IEnumerator<TType> enumerator, MethodCallExpression whereExpressionClaCallExpression)
        {
	        if (enumerator is MyDatabaseStatementIntegerSetEnumerator x2)
	        {
				// Optimize : Do the first where clause with where condition on database
				ExpressionCompileVisitor ecv = new ExpressionCompileVisitor(MyDatabaseStatementIntegerSet.TheValue);
                ecv.Visit(whereExpressionClaCallExpression.Arguments[1]);
				string whereClause = ecv.GetCondition();
				var r1 = new MyOptimizedDatabaseStatementIntegerSetEnumerator(x2, whereClause);
				var r2 = new MyEnumeratorQueryable<int>(r1);
				return (IMyDisposeQueryable<TType>)r2;
	        }

			if (enumerator is MyDatabaseCursorIntegerSetEnumerator x3)
			{
				// Optimize : Do the first where clause with where condition on database
				ExpressionCompileVisitor ecv = new ExpressionCompileVisitor(MyDatabaseStatementIntegerSet.TheValue);
				ecv.Visit(whereExpressionClaCallExpression.Arguments[1]);
				string whereClause = ecv.GetCondition();
				var r1 = new MyOptimizedDatabaseCursorIntegerSetEnumerator(x3, whereClause);
				var r2 = new MyEnumeratorQueryable<int>(r1);
				return (IMyDisposeQueryable<TType>)r2;
			}

			MyConditionalEnumeratorQueryable<TType> x = new MyConditionalEnumeratorQueryable<TType>(
                    enumerator, whereExpressionClaCallExpression);
            return x;
        }
        
    }
}
