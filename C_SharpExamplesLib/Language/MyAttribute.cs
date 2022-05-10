using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
    // #class attribute
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class AuthorAttribute : System.Attribute
    {
        private string name;
        public double version;

        public AuthorAttribute(string name)
        {
            this.name = name;
            version = 1.0;
        }

        public string GetName()
        {
            return name;
        }
    }

    [Author("Me", version = 1.1)]
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
                if (attr is AuthorAttribute)
                {
                    AuthorAttribute a = (AuthorAttribute) attr;
                    Assert.AreEqual(a.GetName(), "Me");
                    Assert.AreEqual(a.version, 1.1);
                }
            }
        }
    }
}
