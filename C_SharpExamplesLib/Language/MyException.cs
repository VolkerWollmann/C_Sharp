using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language
{
	internal class Sentence(string s)
	{
		public string Value { get; set; } = s;

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
				Assert.IsTrue(!ne.StackTrace?.Contains("InnerGet"));			//call stack does not contain InnerGet
				Assert.IsTrue(ne.StackTrace?.Contains("GetFirstCharacter2"));
				Assert.IsTrue(ne.StackTrace?.Contains("Test"));
			}
		}
	}
}
