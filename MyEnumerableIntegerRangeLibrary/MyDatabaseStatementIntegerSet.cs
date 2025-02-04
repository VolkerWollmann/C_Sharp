using System.Collections;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
using System.Transactions;

namespace C_Sharp.Language.MyEnumerableIntegerRangeLibrary
{
    /// <summary>
    /// Simulate a source, which is worth to be encapsulated for lazy linq queries.
    /// </summary>
    public class MyDatabaseStatementIntegerSet : IMyIntegerSet
    {
        internal readonly string TableName;
        internal const string TheIndex = "theIndex";
        internal const string TheValue = "theValue";

        internal SqlConnection? _dataBaseConnection;

        #region IntegerRangeData
        private int _i;

        #endregion

        #region database operations

        private void ExecuteNonQuery(string statement)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                SqlCommand command = new SqlCommand(statement, _dataBaseConnection);
                command.ExecuteNonQuery();
                scope.Complete();               // enforces the commit
                
            }
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

        private int GetNextIndex(int i)
        {
            string statement = $"select min({TheIndex}) from {TableName} where {TheIndex} > {i}";
            return ExecuteScalarQuery(statement);
        }

        private int GetValueAtIndex(int i)
        {
            string statement = $"select {TheValue} from {TableName} where {TheIndex} = {i}";
            return ExecuteScalarQuery(statement);
        }

        #endregion

        #region IEnumerator<int> support
        public void Dispose()
        {
            DeleteTable();
            _dataBaseConnection?.Close();
        }

        /// <summary>
        /// Simulate time-consuming generation of next element
        /// </summary>
        /// <returns>next value</returns>
        public bool MoveNext()
        {
            _i = GetNextIndex(_i);
            return _i > 0;
        }

        public void Reset()
        {
            _i = -1;
        }

        public int Current => GetValueAtIndex(_i);

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

        #region IMyIntegerSet

        public virtual IMyIntegerSet GetFilteredSet(LambdaExpression lambdaExpression)
        {
            List<int> result = new List<int>();
            Func<int, bool> compiledExpression = (Func<int, bool>)lambdaExpression.Compile();

            Reset();
            while (MoveNext())
            {
                if (compiledExpression(Current))
                    result.Add(Current);
            }

            return new MyMemoryIntegerSet(result);
        }

        public virtual int Sum()
        {
            int sum=0;
            Reset();
            while (MoveNext())
            {
                sum = sum + Current;
            }

            return sum;
        }

        public virtual bool Any(LambdaExpression lambdaExpression)
        {
            Func<int, bool> compiledExpression = (Func<int, bool>)lambdaExpression.Compile();

            Reset();
            while (MoveNext())
            {
                if (compiledExpression(Current))
                    return true;
            }

            return false;
        }

        public virtual bool Any()
        {
            Reset();
            return MoveNext();
        }

		public IEnumerable<int> AsEnumerable()
		{
			return this;
		}


		#endregion

		#region Constructor

		public MyDatabaseStatementIntegerSet(string connectionString, List<int> set)
        {
            _dataBaseConnection = _dataBaseConnection = new SqlConnection(connectionString); 
            _dataBaseConnection.Open();
            
            TableName = "MyDatabaseIntegerSet" + Guid.NewGuid().ToString("N").ToUpper();

            // create table
            CreateTable();
            
            // insert values
            InsertValues(set);

            Reset();
        }
        #endregion

    }
}