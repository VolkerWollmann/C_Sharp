﻿using System.Linq.Expressions;
using C_Sharp.Language.IQueryable;
using C_Sharp.Language.IQueryable2;
using C_Sharp.Language.MyEnumerableIntegerRangeLibrary;

namespace C_SharpExamplesLib.Language.IQueryable2
{
    public class MySelectorEnumeratorQueryProvider2<TResultType, TBaseType> : IQueryProvider
    {
        private MySelectorEnumeratorQueryable2<TResultType, TBaseType> _mySelectorEnumerator;

        public MySelectorEnumeratorQueryProvider2(MySelectorEnumeratorQueryable2<TResultType, TBaseType> selectorEnumerator)
        {
            _mySelectorEnumerator = selectorEnumerator;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
            
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            InnermostExpressionFinder whereFinder = new InnermostExpressionFinder("Where");
            MethodCallExpression? whereExpression = whereFinder.GetInnermostExpression(expression);

            if (whereExpression != null)
            {
                var newQueryableEnumerator = new MyConditionalEnumeratorQueryable2<TResultType>(
                    _mySelectorEnumerator.GetEnumerator(), whereExpression);

                return (IQueryable<TElement>)newQueryableEnumerator;
            }

            InnermostExpressionFinder selectFinder = new InnermostExpressionFinder("Select");
            MethodCallExpression? selectExpression = selectFinder.GetInnermostExpression(expression);
            if (selectExpression != null)
            {
                IEnumerator<TResultType> enumerator = _mySelectorEnumerator.GetEnumerator();

                var selectorEnumerator = new MySelectorEnumerator<TElement, TResultType>(enumerator, selectExpression);

                var newQueryableEnumerator = new MySelectorEnumeratorQueryable2<TElement, TResultType>(selectorEnumerator);

                return newQueryableEnumerator;
            }

            throw new NotImplementedException("CreateQuery");
        }

        public object? Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            throw new NotImplementedException();
        }
    }
}
