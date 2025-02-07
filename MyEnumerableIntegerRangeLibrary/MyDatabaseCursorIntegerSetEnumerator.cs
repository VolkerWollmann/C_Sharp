using Microsoft.Data.SqlClient;
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
	
	public class MyDatabaseCursorIntegerSetEnumerator : IEnumerator<int>
	{
		private readonly MyDatabaseCursorIntegerSet _myDatabaseCursorIntegerSet;
        private SqlDataReader? _reader = null;

        #region IEnumerator<int>
        public void Dispose()
		{
			_reader?.Close();

        }

		public bool MoveNext()
		{
			if (_reader != null)
			{
				var wo = _reader.Read();
				return wo;
			}

			return false;
        }

		public void Reset()
		{
			if (_reader != null)
				_reader.Close();

			_reader = _myDatabaseCursorIntegerSet.GetReader();
		
        }


		public int Current => (int)Current;

		object IEnumerator.Current
		{
			get
			{
                if (_reader != null) return (object)_reader.GetInt32(0);
                throw new InvalidOperationException("Reader is null or not open.");
            }
		}

		#endregion

		#region Constructor

		public MyDatabaseCursorIntegerSetEnumerator(MyDatabaseCursorIntegerSet set)
		{
			_myDatabaseCursorIntegerSet = set;
			Reset();
        }
		#endregion


	}
}