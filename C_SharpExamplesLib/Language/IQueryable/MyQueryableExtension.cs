using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;

namespace C_SharpExamplesLib.Language.IQueryable
{
    public static class MyQueryableExtension
    {
	    private static readonly Func<int, int> SquareFunc = x => x * x;

	    private static MySelectorEnumeratorQueryable<int, int> GetSquareQueryable(IEnumerator<int> enumerator)
	    {
		    var e = new MySelectorEnumerator<int, int>(enumerator, SquareFunc);
			return new MySelectorEnumeratorQueryable<int, int>(e);


	    }

		public static MySelectorEnumeratorQueryable<int, int> Square(this IMyDisposeQueryable<int> myEnumeratorQueryable)
		{
			return GetSquareQueryable(myEnumeratorQueryable.GetEnumerator());
		}
	}
}
