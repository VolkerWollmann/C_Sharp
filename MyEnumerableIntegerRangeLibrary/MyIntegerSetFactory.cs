using MyEnumerableIntegerRangeLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp.Language.MyEnumerableIntegerRangeLibrary
{
    public class MyIntegerSetFactory
    {
        private bool _databaseAvailable=false;
        SqlConnection _dataBaseConnection=null;

        private MyDatabaseIntegerSet _myDatabaseIntegerSet = null;

        private string CreateConnectionString()
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
                Password = settings.DatabasePassword // password
            };
            return builder.ConnectionString;

        }

        private bool TestDatabaseConnection()
        {
            try
            {
                string connectionString = CreateConnectionString();
                _dataBaseConnection = new SqlConnection(connectionString);
                _dataBaseConnection.Open();
                _dataBaseConnection.Close();

            }
            catch (Exception e)
            {
                return false;
            }
            
            return true;
        }

        public MyIntegerSetFactory()
        {
            _databaseAvailable = TestDatabaseConnection();
        }

        public List<IMyIntegerSet> GetTestData()
        {
            List<IMyIntegerSet> data = new List<IMyIntegerSet>() {
                new MyIntegerSet(new List<int> {1, 2, 3}),
            };
            if (_myDatabaseIntegerSet != null)
                data.Add(_myDatabaseIntegerSet);

            return data;
        }

    }
}
