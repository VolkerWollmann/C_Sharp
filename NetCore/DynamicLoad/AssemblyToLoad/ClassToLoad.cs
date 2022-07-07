namespace AssemblyToLoad
{
    public class ClassToLoad : DynamicLoadInterface.DynamicLoadInterface
    {
        public ClassToLoad()
        {

        }

        int DynamicLoadInterface.DynamicLoadInterface.Add(int value1, int value2)
        {
            return value1 + value2;
        }
    }
}