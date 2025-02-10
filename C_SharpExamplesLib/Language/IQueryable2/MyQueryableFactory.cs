using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using C_Sharp.Language.IQueryable2;
using C_Sharp.Language.MyEnumerableIntegerRangeLibrary;
using C_SharpExamplesLib.Language.IQueryable2;
using MyEnumerableIntegerRangeLibrary;

namespace C_Sharp.Language.IQueryable2
{
    public class MyQueryableFactory
    {
        public static IMyDisposeQueryable<int> GetMyQueryable(IMyIntegerSet myIntegerSet)
        {
            return new MyEnumeratorQueryable2<int>(myIntegerSet.GetEnumerator());
        }

        public static IMyDisposeQueryable<TType> GetMyConditionalEnumeratorQueryable2<TType>(
            IEnumerator<TType> enumerator, MethodCallExpression? whereExpressionClaCallExpression)
        {
	        if (enumerator is MyDatabaseStatementIntegerSetEnumerator x2)
	        {
				// Optimize : Do the first where clause with where condition on database
				ExpressionCompileVisitor ecv = new ExpressionCompileVisitor(MyDatabaseStatementIntegerSet.TheValue);
                ecv.Visit(whereExpressionClaCallExpression.Arguments[1]);
				string whereClause = ecv.GetCondition();
				var r1 = new MyOptimizedDatabaseStatementIntegerSetEnumerator(x2, whereClause);
				var r2 = new MyEnumeratorQueryable2<int>(r1);
				return (IMyDisposeQueryable<TType>)r2;
	        }
            
            MyConditionalEnumeratorQueryable2<TType> x = new MyConditionalEnumeratorQueryable2<TType>(
                    enumerator, whereExpressionClaCallExpression);
            return x;
        }
        
    }
}
