namespace MyEnumerableIntegerRangeLibrary
{
    /// <summary>
    /// Generic counterpart of <see cref="IMyIntegerSet"/> for arbitrary element types
    /// </summary>
    /// <typeparam name="TType">type of the elements</typeparam>
    public interface IMySet<out TType> : IEnumerable<TType>, IDisposable
    {
    }
}
