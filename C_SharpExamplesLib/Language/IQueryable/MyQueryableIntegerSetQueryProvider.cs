﻿using System;
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

    internal class ExpressionTreeModifier2 : ExpressionVisitor
    {
        private readonly MyQueryableIntegerSet MyQueryableIntegerSet;
        private bool done=false;

        internal ExpressionTreeModifier2(MyQueryableIntegerSet myQueryableIntegerSet)
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

            return Visit(expression);
        }

    }

    internal class MyQueryableIntegerSetQueryContext
    {
        public MyIntegerSet MyIntegerSet;
        private bool IsQueryOverDataSource(Expression expression)
        {
            // If expression represents an unqueried IQueryable data source instance, 
            // expression is of type ConstantExpression, not MethodCallExpression. 
            return (expression is MethodCallExpression);
        }

        // Executes the expression tree that is passed to it. 
        internal object Execute(Expression expression, bool isEnumerable)
        {
            ConstantExpression constantExpression = expression as ConstantExpression;
            if (constantExpression != null) 
            {
                MyQueryableIntegerSet myQueryableIntegerSet = (MyQueryableIntegerSet)constantExpression.Value;
                if (myQueryableIntegerSet != null)
                {
                    return myQueryableIntegerSet.ToList();
                }
            }

            if (!IsQueryOverDataSource(expression))
                throw new InvalidProgramException("No query over the data source was specified.");

            // Find the call to Where() and get the lambda expression predicate.
            InnermostWhereFinder whereFinder = new InnermostWhereFinder();
            MethodCallExpression whereExpression = whereFinder.GetInnermostWhere(expression);
            if (whereExpression == null)
            {
                // is something, that we do not want to do on our own
                List<int> l = MyIntegerSet.ToList();
                IQueryable<int> queryableInts = l.AsQueryable();

                ExpressionTreeModifier treeCopier = new ExpressionTreeModifier(queryableInts);
                Expression newExpressionTree = treeCopier.Visit(expression);

                if (isEnumerable)
                    return queryableInts.Provider.CreateQuery(newExpressionTree);
                else
                    return queryableInts.Provider.Execute(newExpressionTree);
            }

            // apply lambda/where on the items and get a filtered MyIntegerSet
            LambdaExpression lambdaExpression = (LambdaExpression)((UnaryExpression)(whereExpression.Arguments[1])).Operand;
            var result = MyIntegerSet.GetFilteredSet(lambdaExpression);
            var result2 = new MyQueryableIntegerSet(result);
            // replace innermost where clause with calculated MyIntegerSet
            ExpressionTreeModifier2 expressionTreeModifier2 = new ExpressionTreeModifier2(result2);
            Expression newExpressionTree2 = expressionTreeModifier2.Visit(expression);

            MyQueryableIntegerSetQueryProvider myQueryableIntegerSetQueryProvider = new MyQueryableIntegerSetQueryProvider(result);

            if (isEnumerable)
                return myQueryableIntegerSetQueryProvider.CreateQuery(newExpressionTree2);
            else
                return myQueryableIntegerSetQueryProvider.Execute(newExpressionTree2);

            throw new NotImplementedException("Handle where clause");
        }

        internal MyQueryableIntegerSetQueryContext(MyIntegerSet myIntegerSet)
        {
            MyIntegerSet = myIntegerSet;
        }
    }

    public class MyQueryableIntegerSetQueryProvider : IQueryProvider
    {
        public readonly MyIntegerSet IntegerSet;

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
                var result = new MyQueryableIntegerSet(IntegerSet);

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
            return (IQueryable<TElement>)new MyQueryableIntegerSet(IntegerSet,this, expression);
        }

        public object Execute(Expression expression)
        {
            MyQueryableIntegerSetQueryContext myQueryableIntegerSetQueryContext =
                new MyQueryableIntegerSetQueryContext(IntegerSet);
            return myQueryableIntegerSetQueryContext.Execute(expression, false);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            bool isEnumerable = (typeof(TResult).Name == "IEnumerable`1");

            MyQueryableIntegerSetQueryContext myQueryableIntegerSetQueryContext =
                new MyQueryableIntegerSetQueryContext(IntegerSet);

            
            return (TResult)myQueryableIntegerSetQueryContext.Execute(expression, isEnumerable);
        }
        #endregion

        #region Construcutor

        public MyQueryableIntegerSetQueryProvider(MyIntegerSet integerSet)
        {
            IntegerSet = integerSet;
        }
        #endregion
    }
}
