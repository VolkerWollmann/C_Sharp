namespace C_SharpExamplesLib.Language.IQueryable
{
    public static class MyQueryableExtension
    {
        private static readonly Func<int, int> SquareFunc = x => x * x;

        private static MySelectorEnumeratorQueryable<int, int> GetSquareQueryable(IEnumerator<int> enumerator)
        {
            var e = new MySelectorEnumerator<int, int>(enumerator, SquareFunc);
            return new MySelectorEnumeratorQueryable<int, int>(e);


        }

        public static IQueryable<int> Square(this IQueryable<int> myEnumeratorQueryable)
        {
            return GetSquareQueryable(myEnumeratorQueryable.GetEnumerator());
        }
    }
}
