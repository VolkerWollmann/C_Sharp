namespace MyEnumerableIntegerRangeLibrary
{
    /// <summary>
    /// Provides the animal table
    /// <code>
    /// Nr  Name      Art    Futter
    /// 1   Macci     Esel   Möhre
    /// 2   Amica     Hund   Knochen
    /// 3   Heidi     Ziege  Gräser
    /// 4   Fridolin  Möwe   Fisch
    /// </code>
    /// as <see cref="IMySet{TType}"/> of <see cref="MyAnimal"/>.
    /// Currently only the memory variant is available.
    /// </summary>
    public class MyAnimalSetFactory : IDisposable
    {
        public static readonly MyAnimal Macci = new(1, "Macci", "Esel", "Möhre");
        public static readonly MyAnimal Amica = new(2, "Amica", "Hund", "Knochen");
        public static readonly MyAnimal Heidi = new(3, "Heidi", "Ziege", "Gräser");
        public static readonly MyAnimal Fridolin = new(4, "Fridolin", "Möwe", "Fisch");

        public static List<MyAnimal> InitialValues => [Macci, Amica, Heidi, Fridolin];

        private readonly List<IMySet<MyAnimal>> _myAnimalSets = [];

        public void Dispose()
        {
            _myAnimalSets.ForEach(animalSet => animalSet.Dispose());
            _myAnimalSets.Clear();
        }

        public List<IMySet<MyAnimal>> GetAnimalSets()
        {
            List<IMySet<MyAnimal>> result = [];

            var myAnimalSet = new MyMemorySet<MyAnimal>(InitialValues);
            _myAnimalSets.Add(myAnimalSet);
            result.Add(myAnimalSet);

            return result;
        }

        public MyMemorySet<MyAnimal> GetMemoryAnimalSet()
        {
            return new MyMemorySet<MyAnimal>(InitialValues);
        }
    }
}
