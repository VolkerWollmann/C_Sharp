using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp
{
	public class MyOperators
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

			MyOperators myOperators11 = null;
			string e1 = myOperators11?.Esel;
			Assert.IsTrue(e1 == null);

			myOperators11 = new MyOperators();
			string e2 = myOperators11?.Esel;
			Assert.IsTrue(e2 == myOperators11.Esel);

			string e3 = e1 + e2;

			string s = (0 < 1) ? "immer" : "nie";
			Assert.IsTrue(s == "immer");
		}

		public static void Test()
		{
			QuestionMarkOperator();
		}
	}
}
