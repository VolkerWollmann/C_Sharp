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
		internal readonly string TableName;
		internal const string TheIndex = "theIndex";
		internal const string TheValue = "theValue";

		
		internal readonly SqlConnection? _dataBaseConnection;

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
				statement += indexValuePair;
				if (i < set.Count)
					statement += ",";
				i++;
			}

			ExecuteNonQuery(statement);
		}

		private void DeleteTable()
		{
			string statement = $"drop table {TableName}";
			ExecuteNonQuery(statement);
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
		public IEnumerator<int> GetEnumerator()
        {
            return new MyDatabaseCursorIntegerSetEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MyDatabaseCursorIntegerSetEnumerator(this);
        }

		#endregion


		public SqlDataReader? GetReader()
        {
	        SqlDataReader? _reader = null;

	        SqlCommand command = new SqlCommand($"select {TheValue} from {TableName} order by {TheIndex}", _dataBaseConnection);
	        _reader = command.ExecuteReader();

	        return _reader;
        }

		#region Constructor

		public MyDatabaseCursorIntegerSet(string connectionString, List<int> set)
		{
			_dataBaseConnection = new SqlConnection(connectionString);
			_dataBaseConnection.Open();
			
			TableName = "MyDatabaseIntegerSet_" + Guid.NewGuid().ToString("N").ToUpper();

			// create table
			CreateTable();

			// insert values
			InsertValues(set);

		}

		public MyDatabaseCursorIntegerSet(MyDatabaseCursorIntegerSet origin)
		{
			_dataBaseConnection = origin._dataBaseConnection!;
			//_dataBaseConnection.Open();

			TableName = origin.TableName;
		}
		#endregion

	}

}