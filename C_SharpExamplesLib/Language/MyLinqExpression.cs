using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace C_Sharp.Langauge
{
    internal class MyExpressionWriter1
    {
        int indent = 0;

        private string GetSpace()
        {
            return new String(' ', indent).ToString();
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
            indent = indent + 5;

            if (expression is ConstantExpression)
                Write((ConstantExpression)expression);
            else if (expression is Expression<Func<int>>)
                Write((Expression<Func<int>>)expression);
            else if (expression is Expression<Func<int, int>>)
                Write((Expression<Func<int, int>>)expression);
            else if (expression is Expression<Func<int, int, int>>)
                Write((Expression<Func<int, int,int >>)expression);
            else if (expression is BinaryExpression)
                Write((BinaryExpression)expression);
            else if (expression is Expression<Func<IEnumerable<int>, IEnumerable<int>>>)
                Write((Expression<Func<IEnumerable<int>, IEnumerable<int>>>)expression);
            else if (expression is MethodCallExpression)
                Write((MethodCallExpression)expression);
            else if (expression is ParameterExpression)
                Write((ParameterExpression)expression);
            else if (expression is Expression<Func<int, bool>>)
                Write((Expression<Func<int, bool>>)expression);
            else
                Console.WriteLine(GetSpace() + expression);
            
            indent = indent - 5;
        }
    }

    internal class MyExpressionWriter2
    {
        int indent = 0;

        private string GetSpace()
        {
            return new String(' ', indent).ToString();
        }

        private void WriteExpression(Expression expression)
        {
            Type constructed = expression.GetType();

            Console.WriteLine(GetSpace() + "NodeType:" + expression.NodeType.ToString());

            if (expression is LambdaExpression)
            {
                LambdaExpression lambdaExpression = expression as LambdaExpression;
                lambdaExpression.Parameters.ToList().ForEach(p => Write(p));
                Write(lambdaExpression.Body);         
            }
        }


        internal void Write(Expression expression)
        {
            indent = indent + 5;

            WriteExpression(expression);

            indent = indent - 5;
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
