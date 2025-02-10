namespace MyEnumerableIntegerRangeLibrary
{
    public class MyOptimizedDatabaseStatementIntegerSet : MyDatabaseStatementIntegerSet
    {
        #region Constructor
        public MyOptimizedDatabaseStatementIntegerSet(string connectionString, List<int> set) : 
            base(connectionString, set)
        {

        }

        public int GetNextIndex(int i, string whereClause)
        {
	        string statement = $"select min({TheIndex}) from {TableName} where {TheIndex} > {i} and {whereClause}";
	        return ExecuteScalarQuery(statement);
        }

		public MyOptimizedDatabaseStatementIntegerSet(MyDatabaseStatementIntegerSet myDsis) :
			base(myDsis)
        {
        }
		#endregion
	}
}
