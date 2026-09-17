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
    /// as <see cref="IMySet{TType}"/> of <see cref="MyAnimal"/>
    /// in memory and, if the database is reachable, in the database.
    /// Counterpart of <see cref="MyIntegerSetFactory"/>.
    /// </summary>
    public class MyAnimalSetFactory : IDisposable
    {
        [Flags]
        public enum DesiredDatabases
        {
            Memory = 1,
            DatabaseCursor = 2,
            DatabaseStatement = 4,
            DatabaseOptimizedStatement = 8,
        }

        public static readonly MyAnimal Macci = new(1, "Macci", "Esel", "Möhre");
        public static readonly MyAnimal Amica = new(2, "Amica", "Hund", "Knochen");
        public static readonly MyAnimal Heidi = new(3, "Heidi", "Ziege", "Gräser");
        public static readonly MyAnimal Fridolin = new(4, "Fridolin", "Möwe", "Fisch");

        public static List<MyAnimal> InitialValues => [Macci, Amica, Heidi, Fridolin];

        private readonly bool _databaseAvailable;
        private readonly string _connectionString;

        private readonly List<IMySet<MyAnimal>> _myAnimalSets = [];

        public MyAnimalSetFactory()
        {
            _connectionString = MyDatabaseSettings.GetConnectionString(GetType().Name);
            _databaseAvailable = MyDatabaseSettings.TestDatabaseConnection(_connectionString);
        }

        public bool DatabaseAnimalSetsAvailable()
        {
            return _databaseAvailable;
        }

        public void Dispose()
        {
            _myAnimalSets.ForEach(animalSet => animalSet.Dispose());
            _myAnimalSets.Clear();
        }

        public List<IMySet<MyAnimal>> GetAnimalSets(DesiredDatabases desiredDatabases = DesiredDatabases.Memory |
                                                                                        DesiredDatabases.DatabaseCursor |
                                                                                        DesiredDatabases.DatabaseStatement |
                                                                                        DesiredDatabases.DatabaseOptimizedStatement)
        {
            List<IMySet<MyAnimal>> result = [];

            if (desiredDatabases.HasFlag(DesiredDatabases.Memory))
                result.Add(Register(new MyMemorySet<MyAnimal>(InitialValues)));

            if (!DatabaseAnimalSetsAvailable())
                return result;

            if (desiredDatabases.HasFlag(DesiredDatabases.DatabaseCursor))
                result.Add(Register(new MyDatabaseCursorAnimalSet(_connectionString, InitialValues)));

            if (desiredDatabases.HasFlag(DesiredDatabases.DatabaseStatement))
                result.Add(Register(new MyDatabaseStatementAnimalSet(_connectionString, InitialValues)));

            if (desiredDatabases.HasFlag(DesiredDatabases.DatabaseOptimizedStatement))
                result.Add(Register(new MyOptimizedDatabaseStatementAnimalSet(_connectionString, InitialValues)));

            return result;
        }

        private IMySet<MyAnimal> Register(IMySet<MyAnimal> animalSet)
        {
            _myAnimalSets.Add(animalSet);
            return animalSet;
        }

        public MyMemorySet<MyAnimal> GetMemoryAnimalSet()
        {
            return new MyMemorySet<MyAnimal>(InitialValues);
        }
    }
}
