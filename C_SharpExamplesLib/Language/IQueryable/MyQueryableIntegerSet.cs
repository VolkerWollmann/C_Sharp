using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using C_Sharp.Language.MyEnumerableIntegerRangeLibrary;

namespace C_Sharp.Language.IQueryable
{
    
    public class MyQueryableIntegerSet<TOutputType> : IQueryable<TOutputType>
    {
        private IMyIntegerSet _myIntegerSet;

        public IEnumerator<TOutputType> GetEnumerator()
        {
            return (Provider.Execute<IEnumerable<TOutputType>>(Expression)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Expression Expression { get; }
        public Type ElementType => typeof(TOutputType);
        public IQueryProvider Provider { get; }

        public MyQueryableIntegerSet<TBaseType2> CastToNewType<TBaseType2>()
        {
            MyQueryableIntegerSet<TBaseType2> result = new MyQueryableIntegerSet<TBaseType2>(_myIntegerSet);
            return result;
        }

        #region IList<int>

        public List<TOutputType> ToList()
        {
            List<TOutputType> result = new List<TOutputType>();

            _myIntegerSet.Reset();
            while (_myIntegerSet.MoveNext())
            {
                int current = _myIntegerSet.Current;
                TOutputType castCurrent = (TOutputType)Convert.ChangeType(current, typeof(TOutputType));
                result.Add(castCurrent);
            }

            return result;
        }

        public List<int> ToIntegerList()
        {
            List<int> result = new List<int>();

            _myIntegerSet.Reset();
            while (_myIntegerSet.MoveNext())
            {
                result.Add(_myIntegerSet.Current);
            }

            return result;
        }

        #endregion

        public IMyIntegerSet GetFilteredSet(LambdaExpression lambdaExpression)
        {
            return _myIntegerSet.GetFilteredSet(lambdaExpression);
        }

        public int SumImplementation()
        {
            return _myIntegerSet.Sum();
        }

        public bool AnyImplementation(LambdaExpression lambdaExpression)
        {
            return _myIntegerSet.Any(lambdaExpression);
        }

        bool Any(Expression<Func<int, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        #region Constructors

        public MyQueryableIntegerSet(IMyIntegerSet myIntegerSet)
        {
            _myIntegerSet = myIntegerSet;
            Provider = new MyQueryableIntegerSetQueryProvider<TOutputType>(this);
            Expression = Expression.Constant(this);
        }

        /// <summary> 
        /// This constructor is called by Provider.CreateQuery(). 
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="expression"></param>
        /// <param name="integerSet"></param>
        private MyQueryableIntegerSet(IMyIntegerSet integerSet, MyQueryableIntegerSetQueryProvider<TOutputType> provider,
            Expression expression) :
            this(integerSet)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            if (!typeof(IQueryable<TOutputType>).IsAssignableFrom(expression.Type))
            {
                throw new ArgumentOutOfRangeException(nameof(expression));
            }


            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            Expression = expression;
        }

        public MyQueryableIntegerSet<TBaseType2> CreateMyQueryableIntegerSet<TBaseType2>(MyQueryableIntegerSetQueryProvider<TBaseType2> provider,
            Expression expression)
        {
            return new MyQueryableIntegerSet<TBaseType2>(this._myIntegerSet, provider, expression);
        }

        #endregion
    }
}
