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
using C_Sharp.Language.IQueryable;
using C_Sharp.Language.MyEnumerableIntegerRangeLibrary;
using C_SharpExamplesLib.Language.IQueryable;
using MyEnumerableIntegerRangeLibrary;

namespace C_Sharp.Language.IQueryable
{
    public class MyQueryableFactory
    {
        public static IMyDisposeQueryable<TType> GetMyConditionalEnumeratorQueryable2<TType>(
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
