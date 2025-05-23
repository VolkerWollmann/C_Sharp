﻿using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language
{
    internal class MyExpressionWriter1
    {
        int _indent;

        private string GetSpace()
        {
            return new string(' ', _indent);
        }

        private void Write(ConstantExpression constantExpression)
        {
            var cr = typeof(EnumerableQuery<int>);
            
            if (constantExpression.Type.IsAssignableFrom(cr))
            {
                Console.WriteLine(GetSpace() + "Enumerable Query:" + constantExpression.Value);
            }
            else
                Console.WriteLine(GetSpace() + "Constant:" + constantExpression.Value );
        }

        private void Write(UnaryExpression unaryExpression)
        {
            Console.WriteLine(GetSpace() + "Unary Expression:");
            Write(unaryExpression.Operand);
        }
        private void Write(Expression<Func<int>> parameterLessIntegerFunction )
        {
            Console.WriteLine(GetSpace() + "f():");
            Write(parameterLessIntegerFunction.Body);
        }

        private void Write(BinaryExpression binaryExpression)
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

        private void Write(MethodCallExpression methodCallExpression)
        {
            Console.WriteLine(GetSpace() + "MethodCall:" + methodCallExpression.NodeType.ToString());

            foreach (var a in methodCallExpression.Arguments.ToList())
            {
                Write(a);
            }
        }

        private void Write(ParameterExpression parameterExpression)
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
            else if (expression is UnaryExpression unaryExpression)
                Write(unaryExpression);
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
            return new String(' ', _indent);
        }

        private void WriteExpression(Expression expression)
        {
            //Type constructed = expression.GetType();

            Console.WriteLine(GetSpace() + "NodeType:" + expression.NodeType.ToString());

            if (expression is LambdaExpression lambdaExpression)
            {
                lambdaExpression.Parameters.ToList().ForEach(Write);

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
    public abstract class MyLinqExpression
    {
        private static List<Expression> GetExpressionList()
        {
            //#Expression #Linq
            List<Expression> expressions = [];

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

            var numberList = Enumerable.Range(1, 10).AsQueryable();
            IQueryable<int> filteredNumberList = numberList.Where(i => i >= 3 && i <= 6).AsQueryable();
            expressions.Add(filteredNumberList.Expression);

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

        public static void CompileLambda()
        {
            // Define a parameter for the lambda expression (x)
            ParameterExpression parameter = Expression.Parameter(typeof(int), "x");
            
            // Define the body of the lambda expression (x * x)
            Expression body = Expression.Multiply(parameter, parameter);
            
            // Create the lambda expression (x => x * x)
            LambdaExpression lambda = Expression.Lambda(body, parameter);
            
            // Compile the lambda expression into a delegate
            var compiledLambda = (Func<int, int>)lambda.Compile();
            
            // Use the compiled lambda expression
            int result = compiledLambda(5);
            
            Assert.AreEqual(25,result);
        }
    }
}
