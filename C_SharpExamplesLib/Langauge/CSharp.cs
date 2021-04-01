using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp
{
    public class CSharp
    {
        /// <summary>
        /// test
        /// #local method #local function
        /// </summary>
        public static void LocalFunction()
        {
            int GetOne()
            {
                return 1;
            }

            var one = GetOne();
        }

        public static void MultiLineStringConstant()
        {
            string animals = @"donkey
                               dog
                               cat";

            Assert.IsNotNull(animals);
        }
    }
}
