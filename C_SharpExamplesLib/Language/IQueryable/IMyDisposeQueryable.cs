using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_SharpExamplesLib.Language.IQueryable
{
	public interface IMyDisposeQueryable<out TType> : IQueryable<TType>, IDisposable 
	{
	}
}
