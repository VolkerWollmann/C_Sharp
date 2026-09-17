using System.Globalization;
using System.Linq.Expressions;


namespace MyEnumerableIntegerRangeLibrary
{
    #region primitive compiler

    /// <summary>
    /// very primitive compiler to retrieve the values form the database,
    /// which match the lambda expression
    /// Supported:
    /// - a bare parameter (i => i == 2) is mapped to the column <paramref name="theValue"/>
    /// - a member of the parameter (a => a.Name == "Macci") is mapped to the column with the member's name
    /// - constants: numbers as they are, strings as N'...' literals
    /// - captured variables (closures) are evaluated and treated as constants
    /// - comparison, arithmetic and the logical operators &amp;&amp; and ||
    /// </summary>
    /// <param name="theValue">column for a bare parameter, null if the element type has no single value column</param>
    public class ExpressionCompileVisitor(string? theValue) : ExpressionVisitor
    {
        private readonly string[] _results = new string[50];
        private int _index;

        private static string ToSqlLiteral(object? value)
        {
            return value switch
            {
                null => "null",
                string s => "N'" + s.Replace("'", "''") + "'",
                bool b => b ? "1" : "0",
                IFormattable f => f.ToString(null, CultureInfo.InvariantCulture),
                _ => value.ToString() ?? "null"
            };
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            _results[_index++] = ToSqlLiteral(node.Value);
            return node;
        }

        /// <summary>
        /// only the body is compiled, the parameter declaration itself is no part of the condition
        /// </summary>
        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            Visit(node.Body);
            return node;
        }

        /// <summary>
        /// interpret something like i==2 or j==2 as theValue==2 for the table in the database
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            _results[_index++] = theValue ??
                                 throw new NotSupportedException(
                                     $"Parameter '{node.Name}' of type {node.Type.Name} has no value column, use a member access instead.");
            return node;
        }

        /// <summary>
        /// a.Name is mapped to the column Name;
        /// members of captured variables (closures) are evaluated to a constant
        /// </summary>
        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression is ParameterExpression)
            {
                _results[_index++] = node.Member.Name;
                return node;
            }

            object? value = Expression.Lambda(node).Compile().DynamicInvoke();
            _results[_index++] = ToSqlLiteral(value);
            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            throw new NotImplementedException("No function calls in optimized database integer set");
            //return base.VisitMethodCall(node);
        }

        private static readonly Dictionary<ExpressionType, string> KnownOperations =
            new()
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
                { ExpressionType.AndAlso, "and" },
                { ExpressionType.OrElse, "or" },
            };

        protected override Expression VisitBinary(BinaryExpression node)
        {
            base.Visit(node.Left);
            base.Visit(node.Right);

            if (!KnownOperations.TryGetValue(node.NodeType, out var operation))
                throw new Exception("Compile error");

            string s = _results[_index - 2] + " " + operation + " " + _results[_index - 1];

            // keep the precedence of the lambda expression for logical operators
            if (node.NodeType is ExpressionType.AndAlso or ExpressionType.OrElse)
                s = "(" + s + ")";

            _index -= 2;
            _results[_index++] = s;

            return node;
        }

        public string GetCondition()
        {
            return _results[0];
        }

    }

    #endregion
}
