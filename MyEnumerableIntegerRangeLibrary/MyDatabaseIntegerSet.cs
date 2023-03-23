using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace C_Sharp.Language.MyEnumerableIntegerRangeLibrary
{
    /// <summary>
    /// Simulate a source, which is worth to be encapsulated for lazy linq queries.
    /// </summary>
    public class MyDatabaseIntegerSet : IMyIntegerSet
    {
        private string _TableName;
        private SqlConnection _dataBaseConnection;

        #region IntegerRangeData

        private readonly List<int> _set;
        private int _i;

        #endregion

        #region database operations

        private void ExecuteNonQuery(string statement)
        {
            _dataBaseConnection.Open();

            using (TransactionScope scope = new TransactionScope())
            {
                SqlCommand command = new SqlCommand(statement, _dataBaseConnection);
                command.ExecuteNonQuery();
                scope.Complete();
                
            }

            _dataBaseConnection.Close();
        }

        /// <summary>
        /// Reads one integer value, NULL will be interpreted as -1.
        /// Expected values are > 0
        /// </summary>
        /// <returns>-1, if not found, otherwise the value</returns>
        private int ExecuteScalarQuery(string statement)
        {
            int result = -1;
            _dataBaseConnection.Open();
            SqlCommand command = new SqlCommand(statement, _dataBaseConnection);
            SqlDataReader reader = command.ExecuteReader();
            if ( reader.Read() && (!reader.IsDBNull(0)))
                result = reader.GetInt32(0);
            reader.Close();
            _dataBaseConnection.Close();

            return result;
        }

        private void CreateTable()
        {
            string statement = $"create table {_TableName}(theIndex int, theValue int)";
            ExecuteNonQuery(statement);
        }

        private void InsertValues(List<int> set)
        {
            string statement = $"insert into {_TableName} values ";
            int i = 1;
            foreach (int v in set)
            {
                string indexValuePair = $"({i},{v})";
                statement = statement + indexValuePair;
                if (i < set.Count)
                    statement = statement + ",";
                i++;
            }

            ExecuteNonQuery(statement);
        }

        private void DeleteTable()
        {
            string statement = $"drop table {_TableName}";
            ExecuteNonQuery(statement);
        }

        private int GetNextIndex(int i)
        {
            string statement = $"select min(theIndex) from {_TableName} where theIndex > {i}";
            return ExecuteScalarQuery(statement);
        }

        private int GetValueAtIndex(int i)
        {
            string statement = $"select theValue from {_TableName} where theIndex = {i}";
            return ExecuteScalarQuery(statement);
        }

        #endregion

        #region IEnumerator<int>
        public void Dispose()
        {
            DeleteTable();
        }

        /// <summary>
        /// Simulate time consuming generation of next element
        /// </summary>
        /// <returns>next value</returns>
        public bool MoveNext()
        {
            _i = GetNextIndex(_i);
            return _i > 0;
        }

        public void Reset()
        {
            _i = -1;
        }

        public int Current => GetValueAtIndex(_i);

        object IEnumerator.Current => ((IEnumerator<int>)this).Current;
        #endregion

        #region Helper methods

        public virtual IMyIntegerSet GetFilteredSet(LambdaExpression lambdaExpression)
        {
            List<int> result = new List<int>();
            Func<int, bool> compiledExpression = (Func<int, bool>)lambdaExpression.Compile();

            Reset();
            while (MoveNext())
            {
                if (compiledExpression(Current))
                    result.Add(Current);
            }

            return new MyIntegerSet(result);
        }

        #endregion

        #region Constructor

        public MyDatabaseIntegerSet(SqlConnection dataBaseConnection, List<int> set)
        {
            _dataBaseConnection = dataBaseConnection;
            _TableName = "MyDatabaseIntegerSet" + Guid.NewGuid().ToString("N").ToUpper();

            // create table
            CreateTable();
            
            // insert values
            InsertValues(set);

            Reset();
        }
        #endregion

    }
}