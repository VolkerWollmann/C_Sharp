using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp.Language.MyEnumerableIntegerRangeLibrary
{
    public interface IMyIntegerSet : IEnumerator<int>
    {
        IMyIntegerSet GetFilteredSet(LambdaExpression lambdaExpression);

        int Sum();

        bool Any(LambdaExpression lambdaExpression);
    }
}
