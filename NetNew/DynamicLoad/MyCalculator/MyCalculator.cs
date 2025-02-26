namespace AssemblyToLoad
{
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once UnusedType.Global
    public class MyCalculator : DynamicLoadInterface.IMyCalculator
    {
        int DynamicLoadInterface.IMyCalculator.Add(int value1, int value2)
        {
            return value1 + value2;
        }

        public int ClassToLoadAdd(int value1, int value2)
        {
            return value1 + value2;
        }
    }
}