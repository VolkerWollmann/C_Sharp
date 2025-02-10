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
        private int Start { get; }
        private int Range { get; }
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
            _range = [];
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

}
