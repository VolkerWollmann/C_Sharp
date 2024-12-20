﻿
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
    /// <summary>
    /// used to find an expression that could be evaluated by MyQueryableIntegerSet and MyQueryableIntegerSetQueryProvider
    /// </summary>
    internal class InnermostExpressionFinder : ExpressionVisitor
    {
        private string _innerMostExpressionName = "Where";
        private MethodCallExpression? _innermostExpression=null;

        public MethodCallExpression? GetInnermostExpression(Expression expression)
        {
            Visit(expression);
            return _innermostExpression;
        }

        protected override Expression VisitMethodCall(MethodCallExpression expression)
        {
            if (expression.Method.Name == "Where" &&
                expression.Arguments[0].Type.Name.StartsWith("MyQueryableIntegerSet"))
                _innermostExpression = expression;

            Visit(expression.Arguments[0]);

            return expression;
        }

        public InnermostExpressionFinder(string expressionName)
        {
            _innerMostExpressionName = expressionName;
        }
    }

    internal class ExpressionTreeModifier : ExpressionVisitor
    {
        private readonly IQueryable<int> _queryableIntegers;

        internal ExpressionTreeModifier(IQueryable<int> list)
        {
            _queryableIntegers = list;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            if (!c.Type.IsGenericType)
                return c;

            // #this is important: Replace the constant MyQueryableIntegerSet arg with the IQueryable<TBaseType> collection. 
            var cGenericTypeDefinition = c.Type.GetGenericTypeDefinition();
            if (cGenericTypeDefinition.Equals(typeof(MyQueryableIntegerSet<>)))
                return Expression.Constant(_queryableIntegers);
            else
                return c;
        }
    }

    internal class ExpressionTreeMyQueryableIntegerSetWhereClauseReplaceVisitor<TOutputType> : ExpressionVisitor
    {
        private readonly MyQueryableIntegerSet<TOutputType> _myQueryableIntegerSet;
        private bool done=false;

        internal ExpressionTreeMyQueryableIntegerSetWhereClauseReplaceVisitor(MyQueryableIntegerSet<TOutputType> myQueryableIntegerSet)
        {
            _myQueryableIntegerSet = myQueryableIntegerSet;
        }
        protected override Expression VisitMethodCall(MethodCallExpression expression)
        {
            if (expression.Method.Name == "Where")
            {
                if (!done)
                {
                    ConstantExpression? c = expression.Arguments[0] as ConstantExpression;
                    if (c != null)
                    {
                        done = true;
                        return Expression.Constant(_myQueryableIntegerSet);
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

    internal class MyQueryableIntegerSetQueryContext<TOutputType>
    {
        public MyQueryableIntegerSet<TOutputType> MyQueryableIntegerSet;
        private bool IsQueryOverDataSource(Expression expression)
        {
            // If expression represents an unqueried IQueryable data source instance, 
            // expression is of type ConstantExpression, not MethodCallExpression. 
            return (expression is MethodCallExpression);
        }

        #region Expression evaluation

        private object EvaluateConstantExpression(ConstantExpression constantExpression)
        {
            if (constantExpression.Value is MyQueryableIntegerSet<TOutputType> myQueryableIntegerSet)
            {
                return myQueryableIntegerSet.ToList();
            }

            throw new Exception("Cannot evaluate constant expression");
        }

        private object ExecuteNonWhereExpression(Expression expression, bool enumerableNeeded)
        {
            // is something, that we do not want to do on our own
            // so we provide an expression with explicit integer set within instead of MyQueryAbleIntegerSet  
            IEnumerable<int> l = MyQueryableIntegerSet.ToIntegerList();
            IQueryable<int> queryableIntegers = l.AsQueryable();

            ExpressionTreeModifier treeCopier = new ExpressionTreeModifier(queryableIntegers);
            Expression newExpressionTree = treeCopier.Visit(expression);

            if (enumerableNeeded)
            {
	            return queryableIntegers.Provider.CreateQuery(newExpressionTree);
            }
            else
                return queryableIntegers.Provider.Execute(newExpressionTree);
        }

        private object ExecuteWhereExpression(Expression expression, MethodCallExpression whereExpression)
        {
            // apply lambda/where on the items and get a filtered MyIntegerSet
            // get lambda expression
            LambdaExpression lambdaExpression = (LambdaExpression)((UnaryExpression)(whereExpression.Arguments[1])).Operand;

            // apply lambda expression to MyQueryableIntegerSet
            var filteredMyIntegerSet = MyQueryableIntegerSet.GetFilteredSet(lambdaExpression);

            // create filtered MyQueryableIntegerSet
            var filteredMyQueryableIntegerSet = new MyQueryableIntegerSet<TOutputType>(filteredMyIntegerSet);

            // Lesson : Create an List<int> instead of MyQueryableIntegerSet and use that for replacement
            //          MyQueryableIntegerSet is used, so that still MoveNext in an 'integer list' could be observed 

            // replace innermost where clause with calculated MyQueryableIntegerSet
            ExpressionTreeMyQueryableIntegerSetWhereClauseReplaceVisitor<TOutputType> expressionTreeModifier2 = 
                new ExpressionTreeMyQueryableIntegerSetWhereClauseReplaceVisitor<TOutputType>(filteredMyQueryableIntegerSet);
            Expression newExpressionTree2 = expressionTreeModifier2.Visit(expression);

            MyQueryableIntegerSetQueryProvider<TOutputType> myQueryableIntegerSetQueryProvider = 
                new MyQueryableIntegerSetQueryProvider<TOutputType>(filteredMyQueryableIntegerSet);
            
            return myQueryableIntegerSetQueryProvider.CreateQuery(newExpressionTree2);
            
        }

        #endregion
        // Executes the expression tree that is passed to it. 
        internal object Execute(Expression expression, bool enumerableNeeded)
        {
            ConstantExpression? constantExpression = expression as ConstantExpression;
            if (constantExpression != null)
                return EvaluateConstantExpression(constantExpression);
           
            if (!IsQueryOverDataSource(expression))
                throw new InvalidProgramException("No query over the data source was specified.");

            // Find the call to Where() and get the lambda expression predicate.
            InnermostExpressionFinder whereFinder = new InnermostExpressionFinder( "Where");
            MethodCallExpression? whereExpression = whereFinder.GetInnermostExpression(expression);
            if (whereExpression != null)
                return ExecuteWhereExpression(expression, whereExpression);

            return ExecuteNonWhereExpression(expression, enumerableNeeded);
        }

        internal MyQueryableIntegerSetQueryContext(MyQueryableIntegerSet<TOutputType> myQueryableIntegerSet)
        {
            MyQueryableIntegerSet = myQueryableIntegerSet;
        }
    }

    public class MyQueryableIntegerSetQueryProvider<TOutputType> : IQueryProvider
    {
        public readonly MyQueryableIntegerSet<TOutputType> MyQueryableIntegerSet;

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
                throw tie.InnerException ?? tie;
            }
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            // TElement must be int
            if (typeof(TElement) != typeof(int))
            {
                MyQueryableIntegerSet<TElement> myQueryableIntegerSet = MyQueryableIntegerSet.CastToNewType<TElement>();
                MyQueryableIntegerSetQueryProvider<TElement> myQueryableIntegerSetQueryProvider =
                    new MyQueryableIntegerSetQueryProvider<TElement>(myQueryableIntegerSet);

                var result =  (IQueryable<TElement>)myQueryableIntegerSet.CreateMyQueryableIntegerSet<TElement>(
                    myQueryableIntegerSetQueryProvider, expression);

                return result;
            }

            return (IQueryable<TElement>)MyQueryableIntegerSet.CreateMyQueryableIntegerSet( this, expression);
        }

        public object Execute(Expression expression)
        {
            MyQueryableIntegerSetQueryContext<TOutputType> myQueryableIntegerSetQueryContext =
                new MyQueryableIntegerSetQueryContext<TOutputType>(MyQueryableIntegerSet);
            return myQueryableIntegerSetQueryContext.Execute(expression, false);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            bool enumerableNeeded = (typeof(TResult).Name == "IEnumerable`1");

            MyQueryableIntegerSetQueryContext<TOutputType> myQueryableIntegerSetQueryContext =
                new MyQueryableIntegerSetQueryContext<TOutputType>(MyQueryableIntegerSet);

            
            var result =  (TResult)myQueryableIntegerSetQueryContext.Execute(expression, enumerableNeeded);
            return result;
        }
        #endregion

        #region Construcutor

        public MyQueryableIntegerSetQueryProvider(MyQueryableIntegerSet<TOutputType> myQueryableIntegerSet)
        {
            MyQueryableIntegerSet = myQueryableIntegerSet;
        }
        #endregion
    }
}
