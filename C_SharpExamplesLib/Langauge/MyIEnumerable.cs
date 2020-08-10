using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace C_Sharp
{
	/// <summary>
	/// #IEnumrable<int> #IEnumerator<int> #IQueryable<int>
	/// returns the number 1, ...., 10
	/// </summary>
	public class MyIntegerRange : IEnumerable<int>, IEnumerator<int>, IQueryable<int>
	{
		private List<int> Range;
		private int i;

		#region IEnumerator<int>
		public int Current => Range[i];

		object IEnumerator.Current => this;

		// required by Dispose
		protected virtual void Dispose(bool b) { }

		public void Dispose()
		{
			Dispose(true);
		}

		public bool MoveNext()
		{
			i = i + 1;
			return i < Range.Count();
		}

		public void Reset()
		{
			i = 0;
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
		public Expression Expression
		{
			get
			{
				var x = Range.AsQueryable<int>();
				var y = x.Expression;

				List<ElementInit> le = new List<ElementInit>();
				foreach( int i in Range)
				{
					System.Reflection.MethodInfo addMethod = typeof(List<int>).GetMethod("Add");
					System.Linq.Expressions.ElementInit ei =
						System.Linq.Expressions.Expression.ElementInit(addMethod, System.Linq.Expressions.Expression.Constant(i));

					le.Add(ei);
				}

				NewExpression newListExpression = Expression.New(typeof(List<int>));
				ListInitExpression listInitExpression = Expression.ListInit(newListExpression, le);

				EnumerableQuery<int> eq = new EnumerableQuery<int>(listInitExpression);

				return Expression.Constant(eq);
			}
		}
		public Type ElementType => typeof(int);

		public IQueryProvider Provider =>  (IQueryProvider)this.Range.AsQueryable<int>();

		#endregion

		#region Constructor
		private MyIntegerRange()
		{
			Range = new List<int>();
			i = 0;
		}
		private MyIntegerRange(int start, int range) : this()
		{
			int j = start;
			while( j <= start + range)
			{
				Range.Add(j++);
			}
		}

		#endregion

		public static void Test()
		{
			MyIntegerRange myIntegerRange = new MyIntegerRange(1, 10);
			foreach( int i in myIntegerRange)
			{
				if (i > 5)
					break;
			}

			// does work;
			var l = myIntegerRange.ToList();

			//does not work
			var x = myIntegerRange.Where(i => (i < 5)).ToList();
		}
	}
}
