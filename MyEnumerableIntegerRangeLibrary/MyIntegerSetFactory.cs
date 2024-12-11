using MyEnumerableIntegerRangeLibrary.Properties;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEnumerableIntegerRangeLibrary;
using System.Data.Common;

namespace C_Sharp.Language.MyEnumerableIntegerRangeLibrary
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

        private readonly bool _databaseAvailable = false;
        private string _connectionString = "";
        SqlConnection? _dataBaseConnection;

        private List<IMyIntegerSet> _myIntegerSets = new List<IMyIntegerSet>();

        private string GetConnectionString()
        {
	        if (_connectionString == "")
	        {
		        Settings settings = new Settings();

		        string databaseServer = settings.DatabaseServer;

		        var builder = new SqlConnectionStringBuilder
		        {
			        DataSource = settings.DatabaseServer, // server address
			        InitialCatalog = settings.DatabaseName, // database name
			        IntegratedSecurity = false, // server auth(false)/win auth(true)
			        MultipleActiveResultSets = false, // activate/deactivate MARS
			        PersistSecurityInfo = true, // hide login credentials
			        UserID = settings.DatabaseUser, // user name
			        Password = settings.DatabasePassword, // password
			        ApplicationName = this.GetType().Name, // MyIntegerSetFactory
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
            _myIntegerSets.ForEach( mdis => mdis.Dispose() );
            _dataBaseConnection = null;
        }
        public MyIntegerSetFactory()
        {
            _databaseAvailable = TestDatabaseConnection();
        }

        public List<IMyIntegerSet> GetIntegerSets(DesiredDatabases desiredDatabases = DesiredDatabases.Memory |
                                                                                      DesiredDatabases.DatabaseCursor | 
                                                                                      DesiredDatabases.DatabaseStatement |
                                                                                      DesiredDatabases.DatabaseOptimizedStatement)
        {
            List<IMyIntegerSet> result = new List<IMyIntegerSet>();
            if (( desiredDatabases & DesiredDatabases.Memory) == DesiredDatabases.Memory)
            {
                var myIntegerSet = new MyMemoryIntegerSet([1, 2, 3]);
                _myIntegerSets.Add(myIntegerSet);
                result.Add(myIntegerSet);
                
            }

            if (!_databaseAvailable) 
                return result;

			if ((desiredDatabases & DesiredDatabases.DatabaseCursor) == DesiredDatabases.DatabaseCursor)
			{
                var myDatabaseIntegerSet = new MyDatabaseCursorIntegerSet( _connectionString, [1, 2, 3]);
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
                var myOptimizedDatabaseIntegerSet = new MyOptimizedDatabaseStatementIntegerSet(_connectionString, new List<int> { 1, 2, 3 });
                _myIntegerSets.Add(myOptimizedDatabaseIntegerSet);
                result.Add(myOptimizedDatabaseIntegerSet);
            }
           
            return result;
        }

        public bool DatabaseIntegerSetsAvailable()
        {
            return _databaseAvailable;
        }

        public MyMemoryIntegerSet GetMemoryIntegerSet()
        {
            return new MyMemoryIntegerSet([1, 2, 3]);
        }

        public MyOptimizedDatabaseStatementIntegerSet GetOptimizedDatabaseIntegerSet()
        {
            if (_databaseAvailable)
            {
                var result = new MyOptimizedDatabaseStatementIntegerSet(_connectionString, [1, 2, 3]);
                _myIntegerSets.Add(result);
                return result;
            }

            throw new Exception("No database connection");
        }

    }
}
