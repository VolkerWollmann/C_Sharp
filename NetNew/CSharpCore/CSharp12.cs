using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CSharpNew
{
    public abstract class CSharp12
    {
        private class Secret(string theSecret)
        {
            public string GetSecret() => theSecret;
        }
        // #primary constructor
        private class Animal(string name, string food) : Secret(name + " secret")
        {
            public string Name { get; } = name;
            public string Food { get; } = food;
        }

        public static void TestPrimaryConstructor()
        {
            var macchi = new Animal("Macchi", "Carrot");
            Assert.IsNotNull(macchi.GetSecret());
            Assert.AreEqual("Macchi", macchi.Name);
            Assert.AreEqual("Carrot", macchi.Food);
        }

        public static void Swap()
        {
            int a = 5, b = 10;
            // Swap using tuple deconstruction
            (a, b) = (b, a);

            Assert.AreEqual(10, a);
            Assert.AreEqual(5, b);
        }
    }
}
