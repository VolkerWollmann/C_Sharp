using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace C_Sharp.Language
{
    internal class MyExpressionWriter1
    {
        int _indent;

        private string GetSpace()
        {
            return new String(' ', _indent).ToString();
        }

        private void Write(ConstantExpression constantExpression)
        {
            Console.WriteLine(GetSpace() + "Constant:" + constantExpression.Value );
        }
        private void Write(Expression<Func<int>> parameterLessIntegerFunction )
        {
            Console.WriteLine(GetSpace() + "f():");
            Write(parameterLessIntegerFunction.Body);
        }

        private void Write(System.Linq.Expressions.BinaryExpression binaryExpression)
        {
            Console.WriteLine(GetSpace() + "Binary Expression:" + binaryExpression.NodeType.ToString());
            Write(binaryExpression.Left);
            Write(binaryExpression.Right);
        }

        private void Write(Expression<Func<int,int>> unaryFunction)
        {
            Console.WriteLine(GetSpace() + "int f(int x):" + unaryFunction.NodeType.ToString());
            Write(unaryFunction.Body);
        }

        private void Write(Expression<Func<int, int, int>> binaryFunction)
        {
            Console.WriteLine(GetSpace() + "int f(int x, int y):" + binaryFunction.NodeType.ToString());
            Write(binaryFunction.Body);
        }

        private void Write(Expression<Func<int, bool>> unaryFunction)
        {
            Console.WriteLine(GetSpace() + "bool f(int x):" + unaryFunction.NodeType.ToString());
            Write(unaryFunction.Body);
        }

        private void Write(System.Linq.Expressions.MethodCallExpression methodCallExpression)
        {
            Console.WriteLine(GetSpace() + "MethodCall:" + methodCallExpression.NodeType.ToString());

            methodCallExpression.Arguments.ToList().ForEach(a => { Write(a);  });
        }

        private void Write(System.Linq.Expressions.ParameterExpression parameterExpression)
        {
            Console.WriteLine(GetSpace() + "Parameter Expression: " +  parameterExpression.Name);
        }

        private void Write(Expression<Func<IEnumerable<int>, IEnumerable<int>>> unaryFunction)
        {
            Console.WriteLine(GetSpace() + "IEnumerable<int> f(IEnumerable<int> l):" + unaryFunction.NodeType.ToString());
            Write(unaryFunction.Body);
        }

        internal void Write(Expression expression )
        {
            _indent = _indent + 5;

            if (expression is ConstantExpression constantExpression)
                Write(constantExpression);
            else if (expression is Expression<Func<int>> expression1)
                Write(expression1);
            else if (expression is Expression<Func<int, int>> expression2)
                Write(expression2);
            else if (expression is Expression<Func<int, int, int>> expression3)
                Write(expression3);
            else if (expression is BinaryExpression binaryExpression)
                Write(binaryExpression);
            else if (expression is Expression<Func<IEnumerable<int>, IEnumerable<int>>> expression4)
                Write(expression4);
            else if (expression is MethodCallExpression callExpression)
                Write(callExpression);
            else if (expression is ParameterExpression parameterExpression)
                Write(parameterExpression);
            else if (expression is Expression<Func<int, bool>> expression5)
                Write(expression5);
            else
                Console.WriteLine(GetSpace() + expression);
            
            _indent = _indent - 5;
        }
    }

    internal class MyExpressionWriter2
    {
        int _indent;

        private string GetSpace()
        {
            return new String(' ', _indent).ToString();
        }

        private void WriteExpression(Expression expression)
        {
            //Type constructed = expression.GetType();

            Console.WriteLine(GetSpace() + "NodeType:" + expression.NodeType.ToString());

            if (expression is LambdaExpression lambdaExpression)
            {
                lambdaExpression.Parameters.ToList().ForEach(p => Write(p));
                Write(lambdaExpression.Body);         
            }
        }


        internal void Write(Expression expression)
        {
            _indent = _indent + 5;

            WriteExpression(expression);

            _indent = _indent - 5;
        }
    }
    public class MyLinqExpression
    {
        private static List<Expression> GetExpressionList()
        {
            //#Expression #Linq
            List<Expression> expressions = new List<Expression>();

            ConstantExpression i42 = Expression.Constant(42, typeof(int));
            expressions.Add(i42);

            Expression<Func<int>> f42 = () => 42;
            expressions.Add(f42);

            Expression<Func<int, int>> square = x => x * x;
            expressions.Add(square);

            Expression<Func<int, int, int>> sum = (x, y) => x + y;
            expressions.Add(sum);

            Expression<Func<IEnumerable<int>, IEnumerable<int>>>
                numberLessThan42 = l => l.Where(i => i < 42);
            expressions.Add(numberLessThan42);

            Expression<Func<IEnumerable<int>, IEnumerable<int>>>
                numberLessThan42AndGreater5 =
                l => l.Where(i => (i < 42) && (i > 5));
            expressions.Add(numberLessThan42AndGreater5);

            Expression<Func<IEnumerable<int>, IEnumerable<int>>>
                numberLessThan42Greater5 =
                l => (l.Where(i => (i < 42)).Where(i => (i > 5)));
            expressions.Add(numberLessThan42Greater5);

            return expressions;
        }

        public static void WalkExpression1()
        {
            List<Expression> expressions = GetExpressionList();
            // write
            MyExpressionWriter1 myExpressionWriter1 = new MyExpressionWriter1();
            expressions.ForEach(expression =>
                {   Console.WriteLine(expression);
                    myExpressionWriter1.Write(expression);
                    Console.WriteLine("----");
                });
        }

        public static void WalkExpression2()
        {
            List<Expression> expressions = GetExpressionList();
            // write
            MyExpressionWriter2 myExpressionWriter2 = new MyExpressionWriter2();
            expressions.ForEach(expression =>
            {
                Console.WriteLine(expression);
                myExpressionWriter2.Write(expression);
                Console.WriteLine("----");
            });
        }
    }
}
