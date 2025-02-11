using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language.DataTypes
{
    public class MyHashSet
    {
        public static void Test()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            HashSet<int> integerHashSet = new HashSet<int>
            {
                1,
                2
            };

            integerHashSet.Add(1);

            Assert.AreEqual(2, integerHashSet.Count);
            
            Assert.IsTrue(integerHashSet.Contains(1));
        }

    }
}
