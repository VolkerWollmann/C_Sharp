using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Langauge
{
	// #Operator ?. ??
	public class MyOperator
	{
		public string Esel = "Esel";
		private static void QuestionMarkOperator()
		{
			string st = null;
			var t = st ?? "Hase";
			Assert.IsTrue(t == "Hase");
			st = "Igel";
			var u = st ?? "Esel";
			Assert.IsTrue(u == "Igel");

			MyOperator myOperator11 = null;
			string e1 = myOperator11?.Esel;
			Assert.IsTrue(e1 == null);

			myOperator11 = new MyOperator();
			string e2 = myOperator11?.Esel;
			Assert.IsTrue(e2 == myOperator11.Esel);

			string e3 = e1 + e2;

			string s = (0 < 1) ? "immer" : "nie";
			Assert.IsTrue(s == "immer");
		}

		public static void Operator_Test()
		{
			QuestionMarkOperator();
		}
	}
}
