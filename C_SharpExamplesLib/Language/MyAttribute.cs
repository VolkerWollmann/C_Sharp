using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language
{
    // #class attribute
    [AttributeUsage(AttributeTargets.Class)]
    public class AuthorAttribute(string name) : Attribute
    {
	    public double Version = 1.0;

        public string GetName()
        {
            return name;
        }
    }

    [Author("Me", Version = 1.1)]
    public class MyAttributedClass;

    public abstract class MyAttributedClassTest
    {
        public static void Test()
        {
            Attribute[] attrs = Attribute.GetCustomAttributes(typeof(MyAttributedClass)); // Reflection.  

            // Displaying output
            foreach (Attribute attr in attrs)
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
