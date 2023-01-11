
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace C_Sharp.Language.IQueryable
{
    internal class InnermostWhereFinder : ExpressionVisitor
    {
        private MethodCallExpression InnermostWhereExpression=null;

        public MethodCallExpression GetInnermostWhere(Expression expression)
        {
            Visit(expression);
            return InnermostWhereExpression;
        }

        protected override Expression VisitMethodCall(MethodCallExpression expression)
        {
            if (expression.Method.Name == "Where")
                InnermostWhereExpression = expression;

            Visit(expression.Arguments[0]);

            return expression;
        }
    }

    internal class ExpressionTreeModifier : ExpressionVisitor
    {
        private readonly IQueryable<int> QueryableIntegers;

        internal ExpressionTreeModifier(IQueryable<int> list)
        {
            QueryableIntegers = list;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            // #this is important: Replace the constant QueryableTerraServerData arg with the queryable Place collection. 
            if (c.Type == typeof(MyQueryableIntegerSet))
                return Expression.Constant(QueryableIntegers);
            else
                return c;
        }
    }

    internal class ExpressionTreeMyQueryableIntegerSetWhereClauseReplaceVisitor : ExpressionVisitor
    {
        private readonly MyQueryableIntegerSet MyQueryableIntegerSet;
        private bool done=false;

        internal ExpressionTreeMyQueryableIntegerSetWhereClauseReplaceVisitor(MyQueryableIntegerSet myQueryableIntegerSet)
        {
            MyQueryableIntegerSet = myQueryableIntegerSet;
        }
        protected override Expression VisitMethodCall(MethodCallExpression expression)
        {
            if (expression.Method.Name == "Where")
            {
                if (!done)
                {
                    ConstantExpression c = expression.Arguments[0] as ConstantExpression;
                    if (c != null)
                    {
                        done = true;
                        return Expression.Constant(MyQueryableIntegerSet);
                    }

                    var a0 = Visit(expression.Arguments[0]);
                    var a1 = Visit(expression.Arguments[1]);
                    return Expression.Call(expression.Method, new List<Expression>{a0, a1});
                }
            }

            List<Expression> arguments = new List<Expression>();
            foreach(Expression argument in expression.Arguments )
            {
                arguments.Add(Visit(argument));
            }
           
            return Expression.Call(expression.Object,expression.Method, arguments);
        }

    }

    internal class MyQueryableIntegerSetQueryContext
    {
        public MyQueryableIntegerSet MyQueryableIntegerSet;
        private bool IsQueryOverDataSource(Expression expression)
        {
            // If expression represents an unqueried IQueryable data source instance, 
            // expression is of type ConstantExpression, not MethodCallExpression. 
            return (expression is MethodCallExpression);
        }

        #region Expression evaluation

        private object EvaluateConstantExpression(ConstantExpression constantExpression)
        {
            MyQueryableIntegerSet myQueryableIntegerSet = (MyQueryableIntegerSet)constantExpression.Value;
            if (myQueryableIntegerSet != null)
            {
                return myQueryableIntegerSet.ToList();
            }

            throw new Exception("Cannot evaluate constant expression");
        }

        private object EvaluateNonWhereExpression(Expression expression, bool isEnumerable)
        {
            // is something, that we do not want to do on our own
            IEnumerable<int> l = MyQueryableIntegerSet.ToList();
            IQueryable<int> queryableIntegers = l.AsQueryable();

            ExpressionTreeModifier treeCopier = new ExpressionTreeModifier(queryableIntegers);
            Expression newExpressionTree = treeCopier.Visit(expression);

            if (isEnumerable)
                return queryableIntegers.Provider.CreateQuery(newExpressionTree);
            else
                return queryableIntegers.Provider.Execute(newExpressionTree);
        }

        private object EvaluateWhereExpression(Expression expression, MethodCallExpression whereExpression, bool isEnumerable)
        {
            // apply lambda/where on the items and get a filtered MyIntegerSet
            // get lambda expression
            LambdaExpression lambdaExpression = (LambdaExpression)((UnaryExpression)(whereExpression.Arguments[1])).Operand;

            // apply lambda expression to MyQueryableIntegerSet
            var filteredMyIntegerSet = MyQueryableIntegerSet.GetFilteredSet(lambdaExpression);

            // create filtered MyQueryableIntegerSet
            var filteredMyQueryableIntegerSet = new MyQueryableIntegerSet(filteredMyIntegerSet);

            // replace innermost where clause with calculated MyIntegerSet
            ExpressionTreeMyQueryableIntegerSetWhereClauseReplaceVisitor expressionTreeModifier2 = new ExpressionTreeMyQueryableIntegerSetWhereClauseReplaceVisitor(filteredMyQueryableIntegerSet);
            Expression newExpressionTree2 = expressionTreeModifier2.Visit(expression);

            MyQueryableIntegerSetQueryProvider myQueryableIntegerSetQueryProvider = new MyQueryableIntegerSetQueryProvider(filteredMyQueryableIntegerSet);

            if (isEnumerable)
                return myQueryableIntegerSetQueryProvider.CreateQuery(newExpressionTree2);
            else
                return myQueryableIntegerSetQueryProvider.Execute(newExpressionTree2);
        }

        #endregion
        // Executes the expression tree that is passed to it. 
        internal object Execute(Expression expression, bool isEnumerable)
        {
            ConstantExpression constantExpression = expression as ConstantExpression;
            if (constantExpression != null)
                return EvaluateConstantExpression(constantExpression);
           
            if (!IsQueryOverDataSource(expression))
                throw new InvalidProgramException("No query over the data source was specified.");

            // Find the call to Where() and get the lambda expression predicate.
            InnermostWhereFinder whereFinder = new InnermostWhereFinder();
            MethodCallExpression whereExpression = whereFinder.GetInnermostWhere(expression);
            if (whereExpression == null)
                return EvaluateNonWhereExpression(expression, isEnumerable);

            return EvaluateWhereExpression(expression, whereExpression, isEnumerable);
        }

        internal MyQueryableIntegerSetQueryContext(MyQueryableIntegerSet myQueryableIntegerSet)
        {
            MyQueryableIntegerSet = myQueryableIntegerSet;
        }
    }

    public class MyQueryableIntegerSetQueryProvider : IQueryProvider
    {
        public readonly MyQueryableIntegerSet MyQueryableIntegerSet;

        #region private Methods
        // Executes the expression tree that is passed to it. 
        internal static object Execute(Expression expression, bool isEnumerable)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IQueryProvider
        public System.Linq.IQueryable CreateQuery(Expression expression)
        {
            try
            {
                var result = MyQueryableIntegerSet.CreateMyQueryableIntegerSet(this, expression);

                return (System.Linq.IQueryable)result;
            }
            catch (System.Reflection.TargetInvocationException tie)
            {
                throw tie.InnerException;
            }
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            // TElement must be int
            if (typeof(TElement) != typeof(int)) 
                throw new NotImplementedException();
            return (IQueryable<TElement>)MyQueryableIntegerSet.CreateMyQueryableIntegerSet( this, expression);
        }

        public object Execute(Expression expression)
        {
            MyQueryableIntegerSetQueryContext myQueryableIntegerSetQueryContext =
                new MyQueryableIntegerSetQueryContext(MyQueryableIntegerSet);
            return myQueryableIntegerSetQueryContext.Execute(expression, false);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            bool isEnumerable = (typeof(TResult).Name == "IEnumerable`1");

            MyQueryableIntegerSetQueryContext myQueryableIntegerSetQueryContext =
                new MyQueryableIntegerSetQueryContext(MyQueryableIntegerSet);

            
            var result =  (TResult)myQueryableIntegerSetQueryContext.Execute(expression, isEnumerable);
            return result;
        }
        #endregion

        #region Construcutor

        public MyQueryableIntegerSetQueryProvider(MyQueryableIntegerSet myQueryableIntegerSet)
        {
            MyQueryableIntegerSet = myQueryableIntegerSet;
        }
        #endregion
    }
}
