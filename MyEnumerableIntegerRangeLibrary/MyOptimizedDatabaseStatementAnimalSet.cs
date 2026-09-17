namespace MyEnumerableIntegerRangeLibrary
{
    /// <summary>
    /// Counterpart of <see cref="MyOptimizedDatabaseStatementIntegerSet"/>:
    /// the first where clause is evaluated by the database
    /// </summary>
    public class MyOptimizedDatabaseStatementAnimalSet : MyDatabaseStatementAnimalSet
    {
        #region Constructor
        public MyOptimizedDatabaseStatementAnimalSet(string connectionString, List<MyAnimal> set) :
            base(connectionString, set)
        {
        }

        public MyOptimizedDatabaseStatementAnimalSet(MyDatabaseStatementAnimalSet origin) :
            base(origin)
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
