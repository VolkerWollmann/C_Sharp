using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyEnumerableIntegerRangeLibrary.Properties;

namespace MyEnumerableIntegerRangeLibrary
{
    public class MyIntegerSetFactory
    {
        [Flags]
        public enum DesiredDatabases
        {
            Memory = 1,
            DatabaseCursor = 2,
            DatabaseStatement = 4,
            DatabaseOptimizedStatement = 8,
        }

        private readonly bool _databaseAvailable;
        private string _connectionString = "";
        SqlConnection? _dataBaseConnection;

        private readonly List<IMyIntegerSet> _myIntegerSets = [];

        private string GetConnectionString()
        {
            if (_connectionString == "")
            {
                Settings settings = new Settings();

                string databaseServer = settings.DatabaseServer;
                Assert.IsNotNull(databaseServer);

                var builder = new SqlConnectionStringBuilder
                {
                    DataSource = settings.DatabaseServer, // server address
                    InitialCatalog = settings.DatabaseName, // database name
                    IntegratedSecurity = false, // server auth(false)/win auth(true)
                    MultipleActiveResultSets = false, // activate/deactivate MARS
                    PersistSecurityInfo = true, // hide login credentials
                    UserID = settings.DatabaseUser, // user name
                    Password = settings.DatabasePassword, // password
                    ApplicationName = GetType().Name, // MyIntegerSetFactory
                    Encrypt = false,
                    TrustServerCertificate = true
                };

                _connectionString = builder.ConnectionString;
            }

            return _connectionString;
        }

        private bool TestDatabaseConnection()
        {
            if (_dataBaseConnection != null)
                return true;

            try
            {
                string connectionString = GetConnectionString();
                _dataBaseConnection = new SqlConnection(connectionString);
                _dataBaseConnection.Open();
                _dataBaseConnection.Close();
            }
            catch (Exception)
            {
                _dataBaseConnection = null;
                return false;
            }

            return true;
        }

        public void Dispose()
        {
            _myIntegerSets.ForEach(integerSet => integerSet.Dispose());
            _dataBaseConnection = null;
        }
        public MyIntegerSetFactory()
        {
            _databaseAvailable = TestDatabaseConnection();
        }

        public List<IMyIntegerSet> GetIntegerSets(DesiredDatabases desiredDatabases = DesiredDatabases.Memory |
                                                                                      DesiredDatabases.DatabaseCursor |
                                                                                      DesiredDatabases.DatabaseStatement)
        {
            List<IMyIntegerSet> result = [];
            if ((desiredDatabases & DesiredDatabases.Memory) == DesiredDatabases.Memory)
            {
                var myIntegerSet = new MyMemoryIntegerSet([1, 2, 3]);
                _myIntegerSets.Add(myIntegerSet);
                result.Add(myIntegerSet);

            }

            if (!DatabaseIntegerSetsAvailable())
                return result;

            if ((desiredDatabases & DesiredDatabases.DatabaseCursor) == DesiredDatabases.DatabaseCursor)
            {
                var myDatabaseIntegerSet = new MyDatabaseCursorIntegerSet(_connectionString, [1, 2, 3]);
                _myIntegerSets.Add(myDatabaseIntegerSet);
                result.Add(myDatabaseIntegerSet);
            }

            if ((desiredDatabases & DesiredDatabases.DatabaseStatement) == DesiredDatabases.DatabaseStatement)
            {
                var myDatabaseIntegerSet = new MyDatabaseStatementIntegerSet(_connectionString, [1, 2, 3]);
                _myIntegerSets.Add(myDatabaseIntegerSet);
                result.Add(myDatabaseIntegerSet);
            }

            if ((desiredDatabases & DesiredDatabases.DatabaseOptimizedStatement) == DesiredDatabases.DatabaseOptimizedStatement)
            {
                var myOptimizedDatabaseIntegerSet = new MyOptimizedDatabaseStatementIntegerSet(_connectionString,
                    [1, 2, 3]);
                _myIntegerSets.Add(myOptimizedDatabaseIntegerSet);
                result.Add(myOptimizedDatabaseIntegerSet);
            }

            return result;
        }

        //public List<IMyIntegerSet> GetIntegerSets(DesiredDatabases desiredDatabases = DesiredDatabases.Memory |
        // DesiredDatabases.DatabaseCursor |
        // DesiredDatabases.DatabaseStatement |
        // DesiredDatabases.DatabaseOptimizedStatement)
        //{
        //}

        private bool DatabaseIntegerSetsAvailable()
        {
            return _databaseAvailable;
        }

        public MyMemoryIntegerSet GetMemoryIntegerSet()
        {
            return new MyMemoryIntegerSet([1, 2, 3]);
        }

    }
}
