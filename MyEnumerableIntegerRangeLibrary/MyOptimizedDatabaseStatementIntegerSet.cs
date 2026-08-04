namespace MyEnumerableIntegerRangeLibrary
{
    public class MyOptimizedDatabaseStatementIntegerSet : MyDatabaseStatementIntegerSet
    {
        #region Constructor
        public MyOptimizedDatabaseStatementIntegerSet(string connectionString, List<int> set) :
            base(connectionString, set)
        {

        }

        public MyOptimizedDatabaseStatementIntegerSet(MyDatabaseStatementIntegerSet myDsis) :
            base(myDsis)
        {
        }
        #endregion

        public int GetNextIndex(int i, string whereClause)
        {
            string statement = $"select min({TheIndex}) from {TableName} where {TheIndex} > {i} and {whereClause}";
            return ExecuteScalarQuery(statement);
        }

        /// <summary>
        /// Counts on the database instead of enumerating all elements
        /// </summary>
        public int Count()
        {
            string statement = $"select count(*) from {TableName}";
            return ExecuteScalarQuery(statement);
        }

    }
}
