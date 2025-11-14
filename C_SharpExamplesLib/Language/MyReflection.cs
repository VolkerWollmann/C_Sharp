using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language
{
    // #reflection #GetMethod #GetConstructors #Invoke
    internal class Ship(string name)
    {
        public string Name { get; } = name;
        public int Speed { private set; get; }

        public bool SetSpeed(int speed)
        {
            if (speed < -5 || speed > 20)
                return false;

            Speed = speed;

            return true;
        }
    }

    public abstract class MyReflection
    {
        public static void Test()
        {
            Type t = typeof(Ship);

            ConstructorInfo[] ci = t.GetConstructors();

            object ship = ci[0].Invoke(["HMS Victory"]);

            MethodInfo mi = t.GetMethods().First(m => m.Name == "SetSpeed");

            bool speedSet1 = ((Ship)ship).SetSpeed(4);
            Assert.IsTrue(speedSet1);

            bool speedSet2 = (bool)(mi.Invoke(ship, [5]) ?? false);
            Assert.AreEqual(5, ((Ship)ship).Speed);
            Assert.IsTrue(speedSet2);

            var properties = t.GetProperties();

            var name = properties.First(p => p.Name == "Name").GetValue(ship);

            Assert.AreEqual(((Ship)ship).Name, name);
        }

    }
}
