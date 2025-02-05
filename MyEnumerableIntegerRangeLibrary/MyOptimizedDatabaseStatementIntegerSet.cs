using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace C_Sharp.Language.MyEnumerableIntegerRangeLibrary
{
    public class MyOptimizedDatabaseStatementIntegerSet : MyDatabaseStatementIntegerSet
    {
        #region primitive compiler

        /// <summary>
        /// very primitive compiler to retrieve the values form the database,
        /// which match the lambda expression
        /// only evaluates = 2, should evaluate [less|>|=] e.g. 3-4, ...
        /// </summary>
        public class ExpressionCompileVisitor : ExpressionVisitor
        {
            private string[] _results = new string[50];
            private int _index;

            protected override Expression VisitConstant(ConstantExpression node)
            {
                _results[_index++] = node.Value?.ToString() ?? "null";
                return node;
            }

            /// <summary>
            /// interpret something like i==2 or j==2 as theValue==2 for the table in the database
            /// </summary>
            /// <param name="node"></param>
            /// <returns></returns>
            protected override Expression VisitParameter(ParameterExpression node)
            {
                _results[_index++] = TheValue;
                return node;
            }

            protected override Expression VisitMethodCall(MethodCallExpression node)
            {
                throw new NotImplementedException("No function calls in optimized database integer set");
                //return base.VisitMethodCall(node);
            }

            protected override Expression VisitBinary(BinaryExpression node)
            {

                base.Visit(node.Left);
                base.Visit(node.Right);

                string operation;

                Dictionary<ExpressionType, string> knownOperations =
                    new Dictionary<ExpressionType, string>()
                    {
                        { ExpressionType.Equal, "=" },
                        { ExpressionType.GreaterThanOrEqual, ">=" },
                        { ExpressionType.GreaterThan, ">" },
                        { ExpressionType.LessThan, "<" },
                        { ExpressionType.LessThanOrEqual, "<=" },
                        { ExpressionType.NotEqual, "!=" },
                        { ExpressionType.Multiply, "*" },
                        { ExpressionType.Divide, "/" },
                        { ExpressionType.Modulo, "%" },
                        { ExpressionType.Add, "+" },
                        { ExpressionType.Subtract, "-" },
                    };
            
                if (knownOperations.ContainsKey(node.NodeType))
                    operation = knownOperations[node.NodeType];
                else
                    throw new Exception("Compile error");

                string s = _results[_index-2] + " " + operation + " " + _results[_index - 1];
                _index -= 2;
                _results[_index++] = s;

                return node;
            }

            internal string GetCondition()
            {
                return _results[0];
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

            SqlCommand cmd = new SqlCommand(statement, _dataBaseConnection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(reader.GetInt32(0));
            }
            reader.Close();
            
            return new MyMemoryIntegerSet(result);
        }

        public override int Sum()
        {
            int sum = ExecuteScalarQuery($"select Sum({TheValue}) from {TableName}");
            return sum;
        }

        public override bool Any(LambdaExpression lambdaExpression)
        {
            return GetFilteredSet(lambdaExpression).Any();        
        }

        #endregion

        #region Constructor
        public MyOptimizedDatabaseStatementIntegerSet(string connectionString, List<int> set) : 
            base(connectionString, set)
        {

        }
        #endregion
    }
}
