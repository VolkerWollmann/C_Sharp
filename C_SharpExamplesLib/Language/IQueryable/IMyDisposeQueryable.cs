namespace C_SharpExamplesLib.Language.IQueryable
{
    public interface IMyDisposeQueryable<out TType> : IQueryable<TType>, IDisposable;
}
