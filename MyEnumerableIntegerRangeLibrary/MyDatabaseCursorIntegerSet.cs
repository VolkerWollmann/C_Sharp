using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace C_Sharp.Language.MyEnumerableIntegerRangeLibrary
{
	/// <summary>
	/// Simulate a source, which is worth to be encapsulated for lazy linq queries.
	/// </summary>
	public class MyDatabaseCursorIntegerSet : IMyIntegerSet
	{
		private readonly string _tableName;
		private const string TheIndex = "theIndex";
		private const string TheValue = "theValue";

		
		private readonly SqlConnection? _dataBaseConnection;
		private SqlDataReader? _reader = null;

		#region IntegerRangeData

		#endregion

		#region database operations

		private void ExecuteNonQuery(string statement)
		{
			using (TransactionScope scope = new TransactionScope())
			{
				SqlCommand command = new SqlCommand(statement, _dataBaseConnection);
				command.ExecuteNonQuery();
				scope.Complete();               // enforces the commit

			}

		}

		private void CreateTable()
		{
			string statement = $"create table {_tableName}(theIndex int, theValue int)";
			ExecuteNonQuery(statement);
		}

		private void InsertValues(List<int> set)
		{
			string statement = $"insert into {_tableName} values ";
			int i = 1;
			foreach (int v in set)
			{
				string indexValuePair = $"({i},{v})";
				statement += indexValuePair;
				if (i < set.Count)
					statement += ",";
				i++;
			}

			ExecuteNonQuery(statement);
		}

		private void DeleteTable()
		{
			string statement = $"drop table {_tableName}";
			ExecuteNonQuery(statement);
		}

		#endregion

		#region IEnumerator<int> support
		public void Dispose()
		{
			_reader?.Close();
			DeleteTable();
			_dataBaseConnection?.Close();
		}

		/// <summary>
		/// Simulate time-consuming generation of next element
		/// </summary>
		/// <returns>next value</returns>
		public bool MoveNext()
		{
			return _reader != null && _reader.Read();
		}

		public void Reset()
		{
			if (_reader != null ) 
				_reader.Close();
			
			SqlCommand command = new SqlCommand($"select {TheValue} from {_tableName} order by {TheIndex}", _dataBaseConnection);
			_reader = command.ExecuteReader();
		}

		public int Current
		{
			get
			{
				if (_reader != null) return _reader.GetInt32(0);
				throw new InvalidOperationException("Reader is null or not open.");
			}
		}

		object IEnumerator.Current => ((IEnumerator<int>)this).Current;
        #endregion

        #region IEnumerable<int>
        public IEnumerator<int> GetEnumerator()
        {
            return new MyDatabaseCursorIntegerSetEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MyDatabaseCursorIntegerSetEnumerator(this);
        }

        #endregion

        #region IMyIntegerSet

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

			return new MyMemoryIntegerSet(result);
		}

		public virtual int Sum()
		{
			int sum = 0;
			Reset();
			while (MoveNext())
			{
				sum = sum + Current;
			}

			return sum;
		}

		public virtual bool Any(LambdaExpression lambdaExpression)
		{
			Func<int, bool> compiledExpression = (Func<int, bool>)lambdaExpression.Compile();

			Reset();
			while (MoveNext())
			{
				if (compiledExpression(Current))
					return true;
			}

			return false;
		}

		public virtual bool Any()
		{
			Reset();
			return MoveNext();
		}

		#endregion

		#region Constructor

		public MyDatabaseCursorIntegerSet(string connectionString, List<int> set)
		{
			_dataBaseConnection = _dataBaseConnection = new SqlConnection(connectionString);
			_dataBaseConnection.Open();
			
			_tableName = "MyDatabaseIntegerSet" + Guid.NewGuid().ToString("N").ToUpper();

			// create table
			CreateTable();

			// insert values
			InsertValues(set);

			Reset();
		}
		#endregion

	}

    public class MyDatabaseCursorIntegerSetEnumerator : IEnumerator<int>
    {
        private readonly MyDatabaseCursorIntegerSet _myDatabaseCursorIntegerSet;
        private int _i = -1;
        #region IEnumerator<int>
        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            return _myDatabaseCursorIntegerSet.MoveNext();
        }

        public void Reset()
        {
            _i = -1;
        }

        public int Current => _myDatabaseCursorIntegerSet.Current;

        object IEnumerator.Current => Current;

        #endregion

        #region Constructor

        public MyDatabaseCursorIntegerSetEnumerator(MyDatabaseCursorIntegerSet set)
        {
            _myDatabaseCursorIntegerSet = set;
            _i = -1;
        }
        #endregion


    }
}