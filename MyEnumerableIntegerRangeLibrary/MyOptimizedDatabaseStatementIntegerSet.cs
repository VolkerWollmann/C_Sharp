using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace C_Sharp.Language.MyEnumerableIntegerRangeLibrary
{
    public class MyOptimizedDatabaseStatementIntegerSet : MyDatabaseStatementIntegerSet
    {
        #region Constructor
        public MyOptimizedDatabaseStatementIntegerSet(string connectionString, List<int> set) : 
            base(connectionString, set)
        {

        }
        #endregion
    }
}
