﻿using MyEnumerableIntegerRangeLibrary.Properties;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEnumerableIntegerRangeLibrary;

namespace C_Sharp.Language.MyEnumerableIntegerRangeLibrary
{
    public class MyIntegerSetFactory
    {
        public enum DesiredDatabases
        {
            Simple = 1,
            Database = 2,
            DatabaseOptimized = 4
        }

        private bool DatabaseAvailable => (_dataBaseConnection != null);
        SqlConnection? _dataBaseConnection;

        private List<IMyIntegerSet> _myIntegerSets = new List<IMyIntegerSet>();

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
                Password = settings.DatabasePassword, // password
                ApplicationName = this.GetType().Name,  // MyIntegerSetFactory
                Encrypt = false,
                TrustServerCertificate = true
            };
            return builder.ConnectionString;

        }

        private bool TestDatabaseConnection()
        {
            if (_dataBaseConnection != null)
                return true;

            try
            {
                string connectionString = CreateConnectionString();
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
        }
        public MyIntegerSetFactory()
        {
            TestDatabaseConnection();
        }

        public List<IMyIntegerSet> GetIntegerSets(DesiredDatabases desiredDatabases)
        {
            List<IMyIntegerSet> result = new List<IMyIntegerSet>();
            if (( desiredDatabases & DesiredDatabases.Simple) == DesiredDatabases.Simple)
            {
                var myIntegerSet = new MyIntegerSet(new List<int> { 1, 2, 3 });
                _myIntegerSets.Add(myIntegerSet);
                result.Add(myIntegerSet);
                
            }

            if (!DatabaseAvailable || ( _dataBaseConnection == null ))
                return result;

            if ((desiredDatabases & DesiredDatabases.Database) == DesiredDatabases.Database)
            {
                var myDatabaseIntegerSet = new MyDatabaseIntegerSet(_dataBaseConnection, new List<int> { 1, 2, 3 });
                _myIntegerSets.Add(myDatabaseIntegerSet);
                result.Add(myDatabaseIntegerSet);
            }

            if ((desiredDatabases & DesiredDatabases.DatabaseOptimized) == DesiredDatabases.DatabaseOptimized)
            {
                var myOptimizedDatabaseIntegerSet = new MyOptimizedDatabaseIntegerSet(_dataBaseConnection, new List<int> { 1, 2, 3 });
                _myIntegerSets.Add(myOptimizedDatabaseIntegerSet);
                result.Add(myOptimizedDatabaseIntegerSet);
            }
           
            return result;
        }

        public List<IMyIntegerSet> GetIntegerSets()
        {
            return GetIntegerSets(DesiredDatabases.Simple | DesiredDatabases.Database | DesiredDatabases.DatabaseOptimized);
        }

        public bool DatabaseIntegerSetsAvailable()
        {
            return DatabaseAvailable;
        }

        public MyIntegerSet GetIntegerSet()
        {
            return new MyIntegerSet(new List<int> { 1, 2, 3 });
        }

        public MyDatabaseIntegerSet GetDatabaseIntegerSet()
        {
            if (DatabaseAvailable && (_dataBaseConnection != null))
            {
                var result = new MyDatabaseIntegerSet(_dataBaseConnection, [1, 2, 3]);
                _myIntegerSets.Add(result);
                return result;
            }

            throw new Exception("No database connection");
        }


        public MyOptimizedDatabaseIntegerSet GetOptimizedDatabaseIntegerSet()
        {
            if (DatabaseAvailable && (_dataBaseConnection != null))
            {
                var result = new MyOptimizedDatabaseIntegerSet(_dataBaseConnection, new List<int> {1, 2, 3});
                _myIntegerSets.Add(result);
                return result;
            }

            throw new Exception("No database connection");
        }

    }
}
