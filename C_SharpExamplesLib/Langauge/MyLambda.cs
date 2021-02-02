using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp
{
    internal class MyExpressionWriter
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
        private void Write(Expression<Func<int>> paremeterLessIntergerFunction )
        {
            Console.WriteLine(GetSpace() + "f():");
            Write(paremeterLessIntergerFunction.Body);
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
            Console.WriteLine(GetSpace() + "Parmeter Expression: " +  parameterExpression.Name);
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

    public class MyLinqExpression
    {
        public static void ExpressionTest()
        {

            //#Expression #Linq
            List<Expression> expressions = new List<Expression>();

            ConstantExpression i42 = Expression.Constant(42, typeof(int));
            expressions.Add(i42);

            Expression<Func<int>> f42 = () => 42;
            expressions.Add(f42);
            
            Expression<Func<int, int>> square = x => x * x;
            expressions.Add(square);

            Expression<Func<IEnumerable<int>, IEnumerable<int>>> 
                numberLessThan42 = l => l.Where( i => i < 42 );
            expressions.Add(numberLessThan42);

            Expression<Func<IEnumerable<int>, IEnumerable<int>>>
                numberLessThan42AndGreater5 = 
                l =>  l.Where(i => (i < 42) && (i > 5));
            expressions.Add(numberLessThan42AndGreater5);

            Expression<Func<IEnumerable<int>, IEnumerable<int>>>
                numberLessThan42Greater5 =
                l => (l.Where(i => (i < 42)).Where( i=> (i > 5)));
            expressions.Add(numberLessThan42Greater5);

            // write
            MyExpressionWriter myExpressionWriter = new MyExpressionWriter();
            expressions.ForEach(expression =>
                {   Console.WriteLine(expression);
                    myExpressionWriter.Write(expression);
                    Console.WriteLine("----");
                });
        }
    }
}
