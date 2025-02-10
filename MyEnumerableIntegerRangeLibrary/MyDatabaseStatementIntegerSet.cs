using System.Collections;
using System.Transactions;
using Microsoft.Data.SqlClient;

namespace MyEnumerableIntegerRangeLibrary
{
    /// <summary>
    /// Simulate a source, which is worth to be encapsulated for lazy linq queries.
    /// </summary>
    public class MyDatabaseStatementIntegerSet : IMyIntegerSet
    {
        internal readonly string TableName;
        protected const string TheIndex = "theIndex";
        public const string TheValue = "theValue";

        private readonly SqlConnection? _dataBaseConnection;

        #region IntegerRangeData

        #endregion

        #region database operations

        private void ExecuteNonQuery(string statement)
        {
	        using var scope = new TransactionScope();
	        SqlCommand command = new SqlCommand(statement, _dataBaseConnection);
	        command.ExecuteNonQuery();
	        scope.Complete();               // enforces the commit
        }

        /// <summary>
        /// Reads one integer value, NULL will be interpreted as -1.
        /// Expected values are > 0
        /// </summary>
        /// <returns>-1, if not found, otherwise the value</returns>
        internal int ExecuteScalarQuery(string statement)
        {
            int result = -1;
            SqlCommand command = new SqlCommand(statement, _dataBaseConnection);
            SqlDataReader reader = command.ExecuteReader();
            if ( reader.Read() && (!reader.IsDBNull(0)))
                result = reader.GetInt32(0);
            reader.Close();
            
            return result;
        }

        private void CreateTable()
        {
            string statement = $"create table {TableName}(theIndex int, theValue int)";
            ExecuteNonQuery(statement);
        }

        private void InsertValues(List<int> set)
        {
            string statement = $"insert into {TableName} values ";
            int i = 1;
            foreach (int v in set)
            {
                string indexValuePair = $"({i},{v})";
                statement = statement + indexValuePair;
                if (i < set.Count)
                    statement = statement + ",";
                i++;
            }

            ExecuteNonQuery(statement);
        }

        private void DeleteTable()
        {
            string statement = $"drop table {TableName}";
            ExecuteNonQuery(statement);
        }

        public int GetNextIndex(int i)
        {
            string statement = $"select min({TheIndex}) from {TableName} where {TheIndex} > {i}";
            return ExecuteScalarQuery(statement);
        }

        public int GetValueAtIndex(int i)
        {
            string statement = $"select {TheValue} from {TableName} where {TheIndex} = {i}";
            return ExecuteScalarQuery(statement);
        }

		#endregion

		#region IMyIntegerSet IEnumerator<int> support
		public void Dispose()
        {
            DeleteTable();
            _dataBaseConnection?.Close();
        }

		#endregion

		#region IEnumerable<int>
		// bad implementation because only one iterator possible
		public IEnumerator<int> GetEnumerator()
        {
            return new MyDatabaseStatementIntegerSetEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MyDatabaseStatementIntegerSetEnumerator(this);
        }
        #endregion

        
		#region Constructor

		public MyDatabaseStatementIntegerSet(string connectionString, List<int> set)
        {
            _dataBaseConnection = _dataBaseConnection = new SqlConnection(connectionString); 
            _dataBaseConnection.Open();
            
            TableName = "MyDatabaseIntegerSet_" + Guid.NewGuid().ToString("N").ToUpper();

            // create table
            CreateTable();
            
            // insert values
            InsertValues(set);

        }

        protected MyDatabaseStatementIntegerSet(MyDatabaseStatementIntegerSet origin )
        {
	        _dataBaseConnection = origin._dataBaseConnection!;
	        //_dataBaseConnection.Open();

	        TableName = origin.TableName;
        }
		#endregion

	}
}