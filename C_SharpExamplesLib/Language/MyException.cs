using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language
{
    internal class Sentence(string s)
    {
        private string Value { get; } = s;

        private char InnerGet()
        {
            return Value[0];
        }
        public char GetFirstCharacter()
        {
            try
            {
                return InnerGet();
            }
            catch (NullReferenceException ne)
            {
                Assert.IsTrue(ne.StackTrace?.Contains("InnerGet"));
                Assert.IsTrue(ne.StackTrace?.Contains("GetFirstCharacter"));
                Assert.IsTrue(!ne.StackTrace?.Contains("Test"));
                throw;
            }
        }

        public char GetFirstCharacter2()
        {
            try
            {
                return InnerGet();
            }
            catch (NullReferenceException ne)
            {
                Assert.IsTrue(ne.StackTrace?.Contains("InnerGet"));
                Assert.IsTrue(ne.StackTrace?.Contains("GetFirstCharacter2"));
                Assert.IsTrue(!ne.StackTrace?.Contains("Test"));

                throw new Exception("New Exception", ne);

            }
        }
    }

    internal class T1
    {
        public string Message  { get; set; } 
        public void Test()
        {
            
        }
    }

    internal class T2 : IDisposable
    {
        private readonly T1 _t1;
        private readonly string _messageFormT2;
        public T2(T1 t1, string messageFormT2)
        {
            _t1 = t1;
            _messageFormT2 = messageFormT2;
        }

        public void Crash()
        {
            throw  new Exception("Crash");
        }

        public void Dispose()
        {
            _t1.Message = _messageFormT2;
        }
    }

    public abstract class MyException
    {
        public static void Exception_Test()
        {
            try
            {
                var s = new Sentence("Hallo");
                Console.WriteLine($"The first character is {s.GetFirstCharacter()}");

                var t = new Sentence(null!);
                Console.WriteLine($"The first character is {t.GetFirstCharacter()}");
            }
            catch (Exception ne)
            {
                Assert.IsTrue(ne.StackTrace?.Contains("InnerGet"));
                Assert.IsTrue(ne.StackTrace?.Contains("GetFirstCharacter"));
                Assert.IsTrue(ne.StackTrace?.Contains("Test"));
            }

            try
            {
                var t = new Sentence(null!);
                Console.WriteLine($"The first character is {t.GetFirstCharacter2()}");
            }
            catch (Exception ne)
            {
                Assert.DoesNotContain(ne.StackTrace!,"InnerGet");            //call stack does not contain InnerGet
                Assert.IsTrue(ne.StackTrace?.Contains("GetFirstCharacter2"));
                Assert.IsTrue(ne.StackTrace?.Contains("Test"));
            }
        }

        public static void Exception_Dispose_Test()
        {
            T1 t1 = new T1();
            
            using (var t2 = new T2(t1, "Hello"))
            {
                try
                {
                    t2.Crash();  // if this throws, Dispose() is still called
                }
                catch
                {
                    // ignore the exception, we just want to see if Dispose() is called
                }
            }
            
            Assert.AreEqual("Hello",t1.Message);
        }
    }
}
