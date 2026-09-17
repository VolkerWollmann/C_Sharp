using System.Collections;
using System.Transactions;
using Microsoft.Data.SqlClient;

namespace MyEnumerableIntegerRangeLibrary
{
    /// <summary>
    /// Animal table in the database, read row by row with single statements.
    /// Counterpart of <see cref="MyDatabaseStatementIntegerSet"/>.
    /// The column <see cref="TheIndex"/> (Nr) is used as index.
    /// </summary>
    public class MyDatabaseStatementAnimalSet : IMySet<MyAnimal>
    {
        internal readonly string TableName;
        public const string TheIndex = nameof(MyAnimal.Nr);
        protected const string AllColumns = $"{nameof(MyAnimal.Nr)}, {nameof(MyAnimal.Name)}, {nameof(MyAnimal.Art)}, {nameof(MyAnimal.Futter)}";

        private readonly SqlConnection? _dataBaseConnection;

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
            if (reader.Read() && (!reader.IsDBNull(0)))
                result = reader.GetInt32(0);
            reader.Close();

            return result;
        }

        /// <summary>
        /// Reads one animal, the statement must select <see cref="AllColumns"/>
        /// </summary>
        /// <returns>null, if not found, otherwise the animal</returns>
        internal MyAnimal? ExecuteAnimalQuery(string statement)
        {
            MyAnimal? result = null;
            SqlCommand command = new SqlCommand(statement, _dataBaseConnection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
                result = ReadAnimal(reader);
            reader.Close();

            return result;
        }

        /// <summary>
        /// Maps the current row of a reader, which selected <see cref="AllColumns"/>, to an animal
        /// </summary>
        internal static MyAnimal ReadAnimal(SqlDataReader reader)
        {
            return new MyAnimal(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
        }

        /// <summary>
        /// Unicode string literal for SQL Server
        /// </summary>
        internal static string Quote(string value)
        {
            return "N'" + value.Replace("'", "''") + "'";
        }

        private void CreateTable()
        {
            string statement = $"create table {TableName}({nameof(MyAnimal.Nr)} int, {nameof(MyAnimal.Name)} nvarchar(50), {nameof(MyAnimal.Art)} nvarchar(50), {nameof(MyAnimal.Futter)} nvarchar(50))";
            ExecuteNonQuery(statement);
        }

        private void InsertValues(List<MyAnimal> set)
        {
            string statement = $"insert into {TableName} ({AllColumns}) values ";
            statement += string.Join(",",
                set.Select(a => $"({a.Nr},{Quote(a.Name)},{Quote(a.Art)},{Quote(a.Futter)})"));

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

        public MyAnimal GetValueAtIndex(int i)
        {
            string statement = $"select {AllColumns} from {TableName} where {TheIndex} = {i}";
            return ExecuteAnimalQuery(statement) ?? throw new InvalidOperationException($"No animal with {TheIndex} = {i}");
        }

        #endregion

        #region IMySet
        public void Dispose()
        {
            DeleteTable();
            _dataBaseConnection?.Close();
        }

        #endregion

        #region IEnumerable<MyAnimal>
        // bad implementation because only one iterator possible
        public IEnumerator<MyAnimal> GetEnumerator()
        {
            return new MyDatabaseStatementAnimalSetEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MyDatabaseStatementAnimalSetEnumerator(this);
        }
        #endregion

        #region Constructor

        public MyDatabaseStatementAnimalSet(string connectionString, List<MyAnimal> set)
        {
            _dataBaseConnection = new SqlConnection(connectionString);
            _dataBaseConnection.Open();

            TableName = "MyDatabaseStatementAnimalSet_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + Guid.NewGuid().ToString("N").ToUpper();

            // create table
            CreateTable();

            // insert values
            InsertValues(set);
        }

        protected MyDatabaseStatementAnimalSet(MyDatabaseStatementAnimalSet origin)
        {
            _dataBaseConnection = origin._dataBaseConnection!;

            TableName = origin.TableName;
        }
        #endregion
    }
}
