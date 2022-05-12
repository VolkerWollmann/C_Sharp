using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp.Language
{
    public class MyIntegerRangeIQueryProvider : MyIntegerRange, IQueryProvider
    {
       
        public System.Linq.IQueryable CreateQuery(Expression expression)
        {
            MyIntegerRange copy = base.Copy();
            return copy;
        }

        public IQueryable<T> CreateQuery<T>(Expression expression)
        {
            MyIntegerRange copy = base.Copy();
            string x = typeof(T).ToString();
            return (IQueryable<T>)expression;
        }


        public object Execute(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Call)
            {
                MethodCallExpression methodCallExpression = (MethodCallExpression)expression;
                // private implementation of Enumerable.Any #Any
                if (methodCallExpression.Method.Name == "Any")
                {
                    if (methodCallExpression.Arguments.Count == 1)
                        return this.Any();

                    if (methodCallExpression.Arguments.Count == 2)
                    {
                        // complie lambda function as condition for any
                        UnaryExpression unaryExpression = (UnaryExpression)methodCallExpression.Arguments[1];
                        List<ParameterExpression> lp = new List<ParameterExpression> { Expression.Parameter(base.ElementType) };
                        InvocationExpression ie = Expression.Invoke(unaryExpression, lp);
                        var lambdaExpression = Expression.Lambda<Func<int, bool>>(ie, lp);
                        var anyFunction = lambdaExpression.Compile();

                        return base.Any(anyFunction);
                    }
                }

                if (methodCallExpression.Method.Name == "Sum")
                {
                    return base.Sum();
                }


            }

            var result = Expression.Lambda(expression).Compile().DynamicInvoke();
            return result;
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return (TResult)Execute(expression);
        }

        #region Constructor
        public MyIntegerRangeIQueryProvider(MyIntegerRange myIntegerRange): base(myIntegerRange)
        {
            
        }
        #endregion

    }

}
