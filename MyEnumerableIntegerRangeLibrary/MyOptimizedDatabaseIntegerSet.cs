using C_Sharp.Language.MyEnumerableIntegerRangeLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp.Language.MyEnumerableIntegerRangeLibrary
{
    public class MyOptimizedDatabaseIntegerSet : MyDatabaseIntegerSet
    {
        #region primitive compiler

        /// <summary>
        /// very very primitive compiler to retrieve the values form the database,
        /// which match the lambda expression
        /// </summary>
        public class ExpressionCompileVisitor : ExpressionVisitor
        {
            private string[] results = new string[50];
            private int index = 0;

            protected override Expression VisitConstant(ConstantExpression node)
            {
                results[index++] = node.Value.ToString();
                return node;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                results[index++] = TheValue;
                return node;
            }

            protected override Expression VisitBinary(BinaryExpression node)
            {
                
                base.Visit(node.Left);
                base.Visit(node.Right);

                string s = results[index-2] + "=" + results[index - 1];
                index -= 2;
                results[index++] = s;

                return node;
            }

            internal string GetCondition()
            {
                return results[0];
            }
        }

        #endregion

        #region IMyIntegerSet
        public override IMyIntegerSet GetFilteredSet(LambdaExpression lambdaExpression)
        {
            ExpressionCompileVisitor visitor = new ExpressionCompileVisitor();
            visitor.Visit(lambdaExpression);

            string statement = $"select {TheValue} from {TableName} where " + visitor.GetCondition();

            List<int> result = new List<int>();
            _dataBaseConnection.Open();

            SqlCommand cmd = new SqlCommand(statement, _dataBaseConnection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(reader.GetInt32(0));
            }
            reader.Close();
            _dataBaseConnection.Close();

            return new MyIntegerSet(result);
        }

        public override int Sum()
        {
            int sum = base.ExecuteScalarQuery($"select Sum({TheValue}) from {TableName}");
            return sum;
        }

        public override bool Any(LambdaExpression lambdaExpression)
        {
            // understand expression and compile it to a sql server expression
            return base.Any(lambdaExpression) == true;
        }

        #endregion

        #region Constructor
        public MyOptimizedDatabaseIntegerSet(SqlConnection dataBaseConnection, List<int> set) : 
            base(dataBaseConnection, set)
        {

        }
        #endregion
    }
}
