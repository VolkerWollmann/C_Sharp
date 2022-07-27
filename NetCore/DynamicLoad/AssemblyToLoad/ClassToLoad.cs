namespace AssemblyToLoad
{
    // ReSharper disable once UnusedMember.Global
    public class ClassToLoad : DynamicLoadInterface.IDynamicLoadInterface
    {
        int DynamicLoadInterface.IDynamicLoadInterface.Add(int value1, int value2)
        {
            return value1 + value2;
        }
    }
}