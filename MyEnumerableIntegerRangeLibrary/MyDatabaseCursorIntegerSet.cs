using System.Collections;
using System.Transactions;
using Microsoft.Data.SqlClient;

namespace MyEnumerableIntegerRangeLibrary
{
	/// <summary>
	/// Simulate a source, which is worth to be encapsulated for lazy linq queries.
	/// </summary>
	public class MyDatabaseCursorIntegerSet : IMyIntegerSet
	{
		internal readonly string TableName;
		internal const string TheIndex = "theIndex";
		internal const string TheValue = "theValue";

		
		internal readonly SqlConnection? DataBaseConnection;

		#region database operations

		private void ExecuteNonQuery(string statement)
		{
			using var scope = new TransactionScope();
			SqlCommand command = new SqlCommand(statement, DataBaseConnection);
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
			DataBaseConnection?.Close();
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
	        SqlCommand command = new SqlCommand($"select {TheValue} from {TableName} order by {TheIndex}", DataBaseConnection);
	        var reader = command.ExecuteReader();

	        return reader;
        }

		#region Constructor

		public MyDatabaseCursorIntegerSet(string connectionString, List<int> set)
		{
			DataBaseConnection = new SqlConnection(connectionString);
			DataBaseConnection.Open();
			
			TableName = "MyDatabaseCursorIntegerSet_" + Guid.NewGuid().ToString("N").ToUpper();

			// create table
			CreateTable();

			// insert values
			InsertValues(set);

		}

		protected MyDatabaseCursorIntegerSet(MyDatabaseCursorIntegerSet origin)
		{
			DataBaseConnection = origin.DataBaseConnection!;
			//_dataBaseConnection.Open();

			TableName = origin.TableName;
		}
		#endregion

	}

}