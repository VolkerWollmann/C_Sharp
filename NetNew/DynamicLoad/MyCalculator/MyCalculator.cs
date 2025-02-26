namespace MyCalculator
{
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once UnusedType.Global
    public class MyCalculator : IMyCalculatorInterface.IMyCalculator
    {
        int IMyCalculatorInterface.IMyCalculator.Add(int value1, int value2)
        {
            return value1 + value2;
        }

        public int MyCalculatorAdd(int value1, int value2)
        {
            return value1 + value2;
        }
    }
}