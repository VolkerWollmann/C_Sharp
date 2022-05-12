using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp.Language.IQueryable
{
    public static class MyIntegerRangeExentsion
    {
        public static bool Any(this MyIntegerRange myIntegerRange)
        {
            return myIntegerRange.xAny();
        }
    }
}
