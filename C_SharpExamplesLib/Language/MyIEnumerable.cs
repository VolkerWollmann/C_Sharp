using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
	/// <summary>
	/// <![CDATA[ #IEnumerable<int> #IEnumerator<int> #IQueryable<int> #IQueryProvider ]]>
	/// returns the number 1, ...., 10
	///
	/// see : https://putridparrot.com/blog/creating-a-custom-linq-provider/
	/// </summary>
	public class MyIntegerRange : IEnumerator<int>, IQueryable<int>, IQueryProvider // IQueryable<int> includes IEnumerable<int>
	{
		private readonly List<int> _Range;
		private int _I;

		#region IEnumerator<int>
		int IEnumerator<int>.Current => _Range[_I];

        object IEnumerator.Current => ((IEnumerator<int>)this).Current;

        // required by Dispose
		protected virtual void Dispose(bool b) { }

		public void Dispose()
		{
			Dispose(true);
		}

		public bool MoveNext()
		{
			_I = _I + 1;
			return _I < _Range.Count;
		}

		public void Reset()
		{
			_I = 0;
		}
		#endregion

		#region IEnumerable<int>
		public IEnumerator<int> GetEnumerator()
		{
			return this;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}
		#endregion

		#region IQueryable<int>

		// The expression as EnumerableQuery<int>
		// #Expression
		public Expression Expression
		{
			get
            {
                var z = Expression.Constant(this, typeof(IQueryable<int>));

				var x = _Range.AsQueryable();
                var y = x.Expression;

				return z;
			}
		}

		// determines linq types
		public Type ElementType => typeof(int);

		public IQueryProvider Provider =>  this;

		#endregion

		#region IQueryProvider

		public IQueryable CreateQuery(Expression expression)
		{
			return new EnumerableQuery<int>(expression);
		}

		public IQueryable<T> CreateQuery<T>(Expression expression)
		{
			return new EnumerableQuery<T>(expression);
		}

		public object Execute(Expression expression)
		{
			var result =  Expression.Lambda(expression).Compile().DynamicInvoke();
			return result;
		}

		public TResult Execute<TResult>(Expression expression)
		{
			return (TResult)Execute(expression);
		}

		#endregion

		#region Constructor
		private MyIntegerRange()
		{
			_Range = new List<int>();
			_I = 0;
		}
		private MyIntegerRange(int start, int range) : this()
		{
			int j = start;
			while( j <= start + range)
			{
				_Range.Add(j++);
			}
		}

		#endregion

		public static void Test()
		{
			// uses public IEnumerator<int> GetEnumerator()
			// uses public bool MoveNext()
			// uses int IEnumerator<int>.Current
			MyIntegerRange myIntegerRange = new MyIntegerRange(1, 10);
			foreach( int i in myIntegerRange)
			{
				if (i > 5)
					break;
			}

			// myIntegerRange stands at 6
			// uses int IEnumerator<int>.Current
			var a = ((IEnumerator<int>)myIntegerRange).Current;
			Assert.AreEqual(a,6);

			// uses object IEnumerator.Current
			var b = ((IEnumerator)myIntegerRange).Current;
			Assert.IsNotNull(b);

			// does work
			// uses public IEnumerator<int> GetEnumerator()
			var d = myIntegerRange.ToList();
			Assert.IsNotNull(d);

			//does work
			// uses public Expression Expression
			// uses public IQueryable<T> CreateQuery<T>(Expression expression)
			var e = myIntegerRange.Where(i => (i < 5)).ToList();
			Assert.IsNotNull(e);

			// does work 
			// uses public Expression Expression
			// uses public TResult Execute<TResult>(Expression expression)
			var f = myIntegerRange.Sum();
			Assert.IsTrue(f>0);

		}
	}
}
