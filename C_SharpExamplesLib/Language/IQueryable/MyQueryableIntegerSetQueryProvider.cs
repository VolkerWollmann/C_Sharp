using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp.Language.IQueryable
{
    internal class InnermostWhereFinder : ExpressionVisitor
    {
        private MethodCallExpression _innermostWhereExpression=null;

        public MethodCallExpression GetInnermostWhere(Expression expression)
        {
            Visit(expression);
            return _innermostWhereExpression;
        }

        protected override Expression VisitMethodCall(MethodCallExpression expression)
        {
            if (expression.Method.Name == "Where")
                _innermostWhereExpression = expression;

            Visit(expression.Arguments[0]);

            return expression;
        }
    }

    internal class ExpressionTreeModifier : ExpressionVisitor
    {
        private readonly IQueryable<int> _queryableInts;

        internal ExpressionTreeModifier(IQueryable<int> list)
        {
            _queryableInts = list;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            // #this is important: Replace the constant QueryableTerraServerData arg with the queryable Place collection. 
            if (c.Type == typeof(MyQueryableIntegerSet))
                return Expression.Constant(_queryableInts);
            else
                return c;
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
            if (!IsQueryOverDataSource(expression))
                throw new InvalidProgramException("No query over the data source was specified.");

            // Find the call to Where() and get the lambda expression predicate.
            InnermostWhereFinder whereFinder = new InnermostWhereFinder();
            MethodCallExpression whereExpression = whereFinder.GetInnermostWhere(expression);
            if (whereExpression == null)
            {
                // is something, that we do not want to do on our own
                // Create a list of 
                List<int> l = MyIntegerSet.ToList();
                IQueryable<int> queryableInts = l.AsQueryable();

                ExpressionTreeModifier treeCopier = new ExpressionTreeModifier(queryableInts);
                Expression newExpressionTree = treeCopier.Visit(expression);

                if (isEnumerable)
                    return queryableInts.Provider.CreateQuery(newExpressionTree);
                else
                    return queryableInts.Provider.Execute(newExpressionTree);


            }

            throw new NotImplementedException("Handle where clause");
        }
    }

    public class MyQueryableIntegerSetQueryProvider : IQueryProvider
    {
        private MyIntegerSet _integerSet;

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
            throw new NotImplementedException();
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            // TElement must be int
            if (typeof(TElement) != typeof(int)) 
                throw new NotImplementedException();
            return (IQueryable<TElement>)new MyQueryableIntegerSet(this, expression);
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            bool isEnumerable = (typeof(TResult).Name == "IEnumerable`1");

            MyQueryableIntegerSetQueryContext myQueryableIntegerSetQueryContext =
                new MyQueryableIntegerSetQueryContext();

            myQueryableIntegerSetQueryContext.MyIntegerSet = _integerSet;

            return (TResult)myQueryableIntegerSetQueryContext.Execute(expression, isEnumerable);
        }
        #endregion

        #region Construcutor

        public MyQueryableIntegerSetQueryProvider(MyIntegerSet integerSet)
        {
            _integerSet = integerSet;
        }
        #endregion
    }
}
