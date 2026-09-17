namespace MyEnumerableIntegerRangeLibrary
{
    public class MyIntegerSetFactory
    {
        [Flags]
        public enum DesiredDatabases
        {
            Memory = 1,
            DatabaseCursor = 2,
            DatabaseStatement = 4,
            DatabaseOptimizedStatement = 8,
        }

        private readonly bool _databaseAvailable;
        private readonly string _connectionString;

        private readonly List<IMyIntegerSet> _myIntegerSets = [];

        public void Dispose()
        {
            _myIntegerSets.ForEach(integerSet => integerSet.Dispose());
        }

        public MyIntegerSetFactory()
        {
            _connectionString = MyDatabaseSettings.GetConnectionString(GetType().Name);
            _databaseAvailable = MyDatabaseSettings.TestDatabaseConnection(_connectionString);
        }

        public List<IMyIntegerSet> GetIntegerSets(DesiredDatabases desiredDatabases = DesiredDatabases.Memory |
                                                                                      DesiredDatabases.DatabaseCursor |
                                                                                      DesiredDatabases.DatabaseStatement |
                                                                                      DesiredDatabases.DatabaseOptimizedStatement)
        {
            List<int> initialValues = [1, 2, 3];
            List<IMyIntegerSet> result = [];

            if ((desiredDatabases & DesiredDatabases.Memory) == DesiredDatabases.Memory)
            {
                var myIntegerSet = new MyMemoryIntegerSet(initialValues);
                _myIntegerSets.Add(myIntegerSet);
                result.Add(myIntegerSet);

            }

            if (!DatabaseIntegerSetsAvailable())
                return result;

            if ((desiredDatabases & DesiredDatabases.DatabaseCursor) == DesiredDatabases.DatabaseCursor)
            {
                var myDatabaseIntegerSet = new MyDatabaseCursorIntegerSet(_connectionString, initialValues);
                _myIntegerSets.Add(myDatabaseIntegerSet);
                result.Add(myDatabaseIntegerSet);
            }

            if ((desiredDatabases & DesiredDatabases.DatabaseStatement) == DesiredDatabases.DatabaseStatement)
            {
                var myDatabaseIntegerSet = new MyDatabaseStatementIntegerSet(_connectionString, initialValues);
                _myIntegerSets.Add(myDatabaseIntegerSet);
                result.Add(myDatabaseIntegerSet);
            }

            if ((desiredDatabases & DesiredDatabases.DatabaseOptimizedStatement) == DesiredDatabases.DatabaseOptimizedStatement)
            {
                var myOptimizedDatabaseIntegerSet = new MyOptimizedDatabaseStatementIntegerSet(_connectionString,
                    initialValues);
                _myIntegerSets.Add(myOptimizedDatabaseIntegerSet);
                result.Add(myOptimizedDatabaseIntegerSet);
            }

            return result;
        }

        //public List<IMyIntegerSet> GetIntegerSets(DesiredDatabases desiredDatabases = DesiredDatabases.Memory |
        // DesiredDatabases.DatabaseCursor |
        // DesiredDatabases.DatabaseStatement |
        // DesiredDatabases.DatabaseOptimizedStatement)
        //{
        //}

        private bool DatabaseIntegerSetsAvailable()
        {
            return _databaseAvailable;
        }

        public MyMemoryIntegerSet GetMemoryIntegerSet()
        {
            return new MyMemoryIntegerSet([1, 2, 3]);
        }

    }
}
