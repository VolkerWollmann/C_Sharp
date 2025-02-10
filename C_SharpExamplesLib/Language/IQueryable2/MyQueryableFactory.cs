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

namespace C_Sharp.Language.IQueryable2
{
    public class MyQueryableFactory
    {
        public static IMyDisposeQueryable<int> GetMyQueryable(IMyIntegerSet myIntegerSet)
        {
            return new MyEnumeratorQueryable2<int>(myIntegerSet.GetEnumerator());
        }

        public static IMyDisposeQueryable<TType> GetMyConditionalEnumeratorQueryable2<TType>(
            IEnumerator<TType> enumerator, MethodCallExpression? whereExpression)
        {
	        if (enumerator is MyDatabaseStatementIntegerSetEnumerator)
	        {
                // Optimize
		        ;
	        }
            
            MyConditionalEnumeratorQueryable2<TType> x = new MyConditionalEnumeratorQueryable2<TType>(
                    enumerator, whereExpression);
            return x;
        }
        
    }
}
