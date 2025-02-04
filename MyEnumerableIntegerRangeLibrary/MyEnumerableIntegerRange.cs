using System.Collections;

namespace C_Sharp.Language.MyEnumerableIntegerRangeLibrary

{

    /// <summary>
    /// keeps the numbers 1, ...., 10
    /// Comment from th base branch

    ///
    /// see : https://putridparrot.com/blog/creating-a-custom-linq-provider/
    /// </summary>
    public class MyEnumerableIntegerRange : IEnumerable<int>
    {
        private static int _counter = 1;

        public string Name { get; }
        public int Start { get; }
        public int Range { get; }
        private readonly List<int> _range;

        internal int Count => _range.Count;

        internal int ValueAt(int i)
        {
            return _range[i];
        }

        #region IEnumerable<int>
        public IEnumerator<int> GetEnumerator()
        {
            return new MyEnumerableIntegerRangeEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MyEnumerableIntegerRangeEnumerator(this);
        }

        #endregion

        #region Constructor
        private MyEnumerableIntegerRange(string name)
        {
            Name = name;
            _range = new List<int>();
        }

        public MyEnumerableIntegerRange(int start, int range, string name) : this(name)
        {
            Start = start;
            Range = range;
            int j = Start;
            while (j <= Start + Range)
            {
                _range.Add(j++);
            }
        }

        public MyEnumerableIntegerRange(int start, int range) :
            this(start, range, "MyIntegerRange" + _counter++)
        {

        }

        #endregion

    }

    /// <summary>
    /// MyEnumerableIntegerRangeEnumerator
    /// Needed separately for e.g. with two or more concurrent IEnumerators needed
    /// </summary>
    public class MyEnumerableIntegerRangeEnumerator : IEnumerator<int>
    {
        private readonly MyEnumerableIntegerRange _myEnumerableIntegerRange;
        private int _i;
        #region IEnumerator<int>
        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            _i = _i + 1;
            return _i < _myEnumerableIntegerRange.Count;
        }

        public void Reset()
        {
            _i = -1;
        }

        public int Current => _myEnumerableIntegerRange.ValueAt(_i);

        object IEnumerator.Current => Current;

        #endregion

        #region Constructor

        public MyEnumerableIntegerRangeEnumerator(MyEnumerableIntegerRange range)
        {
            _myEnumerableIntegerRange = range;
            _i = -1;
        }
        #endregion


    }

}
