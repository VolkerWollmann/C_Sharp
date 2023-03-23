using C_Sharp.Language.MyEnumerableIntegerRangeLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEnumerableIntegerRangeLibrary
{
    internal class MyOptimizedDatabaseIntegerSet : MyDatabaseIntegerSet
    {
        public override IMyIntegerSet GetFilteredSet(LambdaExpression lambdaExpression)
        {
            return base.GetFilteredSet(lambdaExpression);
        }

        public MyOptimizedDatabaseIntegerSet(SqlConnection dataBaseConnection, List<int> set) : 
            base(dataBaseConnection, set)
        {

        }
    }
}
