using System.Collections;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
using System.Transactions;

namespace C_Sharp.Language.MyEnumerableIntegerRangeLibrary
{
	/// <summary>
	/// Simulate a source, which is worth to be encapsulated for lazy linq queries.
	/// </summary>
	public class MyOptimizedDatabaseCursorIntegerSet(MyDatabaseCursorIntegerSet mdcis)
		: MyDatabaseCursorIntegerSet(mdcis)
	{
		
		public SqlDataReader? GetReader(string whereClause)
		{
			SqlDataReader? _reader = null;

			SqlCommand command = new SqlCommand($"select {TheValue} from {TableName} where {whereClause} order by {TheIndex}", _dataBaseConnection);
			_reader = command.ExecuteReader();

			return _reader;
		}

		#region Constructor

		#endregion

	}

}