using C_Sharp.Language.MyEnumerableIntegerRangeLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEnumerableIntegerRangeLibrary
{
    public class MyOptimizedDatabaseIntegerSet : MyDatabaseIntegerSet
    {
        #region lambda expression visitor

        public class ExpressionTestVisitor : ExpressionVisitor
        {
            public override Expression Visit(Expression node)
            {
                ;
                return base.Visit(node);
            }
        }

        #endregion
        public override IMyIntegerSet GetFilteredSet(LambdaExpression lambdaExpression)
        {
            ExpressionTestVisitor visitor = new ExpressionTestVisitor();
            visitor.Visit(lambdaExpression);

            return base.GetFilteredSet(lambdaExpression);
        }

        public MyOptimizedDatabaseIntegerSet(SqlConnection dataBaseConnection, List<int> set) : 
            base(dataBaseConnection, set)
        {

        }
    }
}
