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

        public int GetNextIndex(int i, string _whereClause)
        {
	        string statement = $"select min({TheIndex}) from {TableName} where {TheIndex} > {i} and {_whereClause}";
	        return ExecuteScalarQuery(statement);
        }

		public MyOptimizedDatabaseStatementIntegerSet(MyDatabaseStatementIntegerSet myDSIS) :
			base(myDSIS)
        {
        }
		#endregion
	}
}
