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
using C_Sharp.Language.MyEnumerableIntegerRangeLibrary;

namespace C_Sharp.Language.IQueryable
{
    public static class MyQueryableIntegerSetExtensionMethods
    {
        #region Extension methods
        public static int Sum(this MyQueryableIntegerSet<int> myQueryableIntegerSet)
        {
            return myQueryableIntegerSet.SumImplementation();
        }
        #endregion
    }
}
