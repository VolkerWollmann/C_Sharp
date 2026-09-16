using System.Collections;
using System.Threading;

namespace MyEnumerableIntegerRangeLibrary
{
    /// <summary>
    /// Generic memory variant of <see cref="MyMemoryIntegerSet"/>.
    /// Simulate a source, which is worth to be encapsulated for lazy linq queries.
    /// </summary>
    /// <typeparam name="TType">type of the elements</typeparam>
    public class MyMemorySet<TType>(List<TType> set) : IMySet<TType>
    {
        #region IMySet
        public void Dispose()
        {

        }

        #endregion

        public int GetNextIndex(int i)
        {
            // simulate time-consuming operation
            Thread.Sleep(100);

            int result = i + 1;
            if (result < set.Count)
                return result;

            return -1;
        }

        public TType GetValueAtIndex(int i)
        {
            return set[i];
        }

        #region IEnumerable<TType>
        // bad implementation because only one iterator possible
        public IEnumerator<TType> GetEnumerator()
        {
            return new MyMemorySetEnumerator<TType>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MyMemorySetEnumerator<TType>(this);
        }
        #endregion

    }
}
