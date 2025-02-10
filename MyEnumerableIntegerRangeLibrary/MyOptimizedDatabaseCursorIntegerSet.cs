using Microsoft.Data.SqlClient;

namespace MyEnumerableIntegerRangeLibrary
{
	/// <summary>
	/// Simulate a source, which is worth to be encapsulated for lazy linq queries.
	/// </summary>
	public class MyOptimizedDatabaseCursorIntegerSet(MyDatabaseCursorIntegerSet mdcis)
		: MyDatabaseCursorIntegerSet(mdcis)
	{
		
		public SqlDataReader? GetReader(string whereClause)
		{
			SqlCommand command = new SqlCommand($"select {TheValue} from {TableName} where {whereClause} order by {TheIndex}", DataBaseConnection);
			var reader = command.ExecuteReader();

			return reader;
		}
	}

}