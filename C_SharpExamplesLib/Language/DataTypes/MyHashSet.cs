using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language.DataTypes
{
    public abstract class MyHashSet
    {
        public static void Test()
        {
            HashSet<int> integerHashSet =
            [
	            1,
	            2
            ];

            Assert.IsFalse(integerHashSet.Add(1));

            Assert.AreEqual(2, integerHashSet.Count);
            
            Assert.IsTrue(integerHashSet.Contains(1));
        }

    }
}
