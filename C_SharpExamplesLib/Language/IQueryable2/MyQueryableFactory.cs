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
