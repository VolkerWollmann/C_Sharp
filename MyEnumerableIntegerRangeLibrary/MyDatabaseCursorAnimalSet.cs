using System.Collections;
using System.Transactions;
using Microsoft.Data.SqlClient;

namespace MyEnumerableIntegerRangeLibrary
{
    /// <summary>
    /// Animal table in the database, read with one open data reader (cursor).
    /// Counterpart of <see cref="MyDatabaseCursorIntegerSet"/>.
    /// </summary>
    public class MyDatabaseCursorAnimalSet : IMySet<MyAnimal>
    {
        internal readonly string TableName;
        internal const string TheIndex = MyDatabaseStatementAnimalSet.TheIndex;
        protected const string AllColumns = $"{nameof(MyAnimal.Nr)}, {nameof(MyAnimal.Name)}, {nameof(MyAnimal.Art)}, {nameof(MyAnimal.Futter)}";

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
            string statement = $"create table {TableName}({nameof(MyAnimal.Nr)} int, {nameof(MyAnimal.Name)} nvarchar(50), {nameof(MyAnimal.Art)} nvarchar(50), {nameof(MyAnimal.Futter)} nvarchar(50))";
            ExecuteNonQuery(statement);
        }

        private void InsertValues(List<MyAnimal> set)
        {
            string statement = $"insert into {TableName} ({AllColumns}) values ";
            statement += string.Join(",",
                set.Select(a => $"({a.Nr},{MyDatabaseStatementAnimalSet.Quote(a.Name)},{MyDatabaseStatementAnimalSet.Quote(a.Art)},{MyDatabaseStatementAnimalSet.Quote(a.Futter)})"));

            ExecuteNonQuery(statement);
        }

        private void DeleteTable()
        {
            string statement = $"drop table {TableName}";
            ExecuteNonQuery(statement);
        }

        #endregion

        #region IMySet
        public void Dispose()
        {
            DeleteTable();
            DataBaseConnection?.Close();
        }

        #endregion

        #region IEnumerable<MyAnimal>
        public IEnumerator<MyAnimal> GetEnumerator()
        {
            return new MyDatabaseCursorAnimalSetEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MyDatabaseCursorAnimalSetEnumerator(this);
        }

        #endregion

        public SqlDataReader? GetReader()
        {
            SqlCommand command = new SqlCommand($"select {AllColumns} from {TableName} order by {TheIndex}", DataBaseConnection);
            var reader = command.ExecuteReader();

            return reader;
        }

        #region Constructor

        public MyDatabaseCursorAnimalSet(string connectionString, List<MyAnimal> set)
        {
            DataBaseConnection = new SqlConnection(connectionString);
            DataBaseConnection.Open();

            TableName = "MyDatabaseCursorAnimalSet_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + Guid.NewGuid().ToString("N").ToUpper();

            // create table
            CreateTable();

            // insert values
            InsertValues(set);
        }

        protected MyDatabaseCursorAnimalSet(MyDatabaseCursorAnimalSet origin)
        {
            DataBaseConnection = origin.DataBaseConnection!;

            TableName = origin.TableName;
        }
        #endregion
    }
}
