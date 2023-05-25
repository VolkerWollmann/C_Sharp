using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
    // #class attribute
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class AuthorAttribute : System.Attribute
    {
        private readonly string _name;
        public double Version;

        public AuthorAttribute(string name)
        {
            this._name = name;
            Version = 1.0;
        }

        public string GetName()
        {
            return _name;
        }
    }

    [Author("Me", Version = 1.1)]
    public class MyAttributedClass
    {

    }

    public class MyAttributedClassTest
    {
        public static void Test()
        {
            System.Attribute[] attrs = System.Attribute.GetCustomAttributes(typeof(MyAttributedClass)); // Reflection.  

            // Displaying output
            foreach (System.Attribute attr in attrs)
            {
                if (attr is AuthorAttribute attribute)
                {
                    Assert.AreEqual("Me", attribute.GetName());
                    Assert.AreEqual(1.1, attribute.Version);
                }
            }
        }
    }
}
