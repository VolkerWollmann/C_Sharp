using Microsoft.Data.SqlClient;

namespace MyEnumerableIntegerRangeLibrary
{
    /// <summary>
    /// Counterpart of <see cref="MyOptimizedDatabaseCursorIntegerSet"/>:
    /// the first where clause is evaluated by the database
    /// </summary>
    public class MyOptimizedDatabaseCursorAnimalSet(MyDatabaseCursorAnimalSet origin)
        : MyDatabaseCursorAnimalSet(origin)
    {
        public SqlDataReader? GetReader(string whereClause)
        {
            SqlCommand command = new SqlCommand($"select {AllColumns} from {TableName} where {whereClause} order by {TheIndex}", DataBaseConnection);
            var reader = command.ExecuteReader();

            return reader;
        }
    }
}
