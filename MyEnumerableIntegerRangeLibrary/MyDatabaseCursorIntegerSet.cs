using System.Collections;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
using System.Transactions;

namespace C_Sharp.Language.MyEnumerableIntegerRangeLibrary
{
	/// <summary>
	/// Simulate a source, which is worth to be encapsulated for lazy linq queries.
	/// </summary>
	public class MyDatabaseCursorIntegerSet : IMyIntegerSet
	{
		private readonly string _tableName;
		private const string TheIndex = "theIndex";
		private const string TheValue = "theValue";

		
		private readonly SqlConnection? _dataBaseConnection;
		private SqlDataReader? _reader;

		#region IntegerRangeData

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

		private void CreateTable()
		{
			string statement = $"create table {_tableName}(theIndex int, theValue int)";
			ExecuteNonQuery(statement);
		}

		private void InsertValues(List<int> set)
		{
			string statement = $"insert into {_tableName} values ";
			int i = 1;
			foreach (int v in set)
			{
				string indexValuePair = $"({i},{v})";
				statement += indexValuePair;
				if (i < set.Count)
					statement += ",";
				i++;
			}

			ExecuteNonQuery(statement);
		}

		private void DeleteTable()
		{
			string statement = $"drop table {_tableName}";
			ExecuteNonQuery(statement);
		}

		#endregion

		public void Dispose()
		{
			_reader?.Close();
			DeleteTable();
			_dataBaseConnection?.Close();
		}

        #region IEnumerable<int>
        public IEnumerator<int> GetEnumerator()
        {
            return new MyDatabaseCursorIntegerSetEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MyDatabaseCursorIntegerSetEnumerator(this);
        }

        public SqlDataReader? GetReader()
        {
	        SqlDataReader? _reader = null;

	        SqlCommand command = new SqlCommand($"select {TheValue} from {_tableName} order by {TheIndex}", _dataBaseConnection);
	        _reader = command.ExecuteReader();

	        return _reader;
        }

		#endregion

		#region IMyIntegerSet

		public virtual IMyIntegerSet GetFilteredSet(LambdaExpression lambdaExpression)
        {
	        throw new NotImplementedException();
        }

		public virtual int Sum()
		{
			throw new NotImplementedException();
		}

		public virtual bool Any(LambdaExpression lambdaExpression)
		{
			throw new NotImplementedException();
		}

		public virtual bool Any()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<int> AsEnumerable()
		{
			return this;
		}

		#endregion

		#region Constructor

		public MyDatabaseCursorIntegerSet(string connectionString, List<int> set)
		{
			_dataBaseConnection = _dataBaseConnection = new SqlConnection(connectionString);
			_dataBaseConnection.Open();
			
			_tableName = "MyDatabaseIntegerSet_" + Guid.NewGuid().ToString("N").ToUpper();

			// create table
			CreateTable();

			// insert values
			InsertValues(set);

		}
		#endregion

	}

}