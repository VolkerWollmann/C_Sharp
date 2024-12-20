﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.Language
{
	// #Operator ?. (Null-Conditional Operator) ??
	[SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
    [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
    [SuppressMessage("ReSharper", "ConstantConditionalAccessQualifier")]
    public class MyOperator
	{
		public string Donkey = "Donkey";
		private static void QuestionMarkOperator()
		{
			string? st = null;
			var t = st ?? "Rabbit";
			Assert.IsTrue(t == "Rabbit");
			st = "Hedgehog";
			var u = st ?? "Donkey";
			Assert.IsTrue(u == "Hedgehog");

			MyOperator? myOperator11 = null;
			string? e1 = myOperator11?.Donkey;
			Assert.IsTrue(e1 == null);

			myOperator11 = new MyOperator();
			string? e2 = myOperator11?.Donkey;
			Assert.IsTrue(e2 == myOperator11?.Donkey);

			string e3 = e1 + e2;
			Assert.AreEqual("Donkey", e3);

            // ReSharper disable once HeuristicUnreachableCode
            string s = (0 < 1) ? "always" : "never";
			Assert.IsTrue(s == "always");
		}

		public static void Operator_Test()
		{
			QuestionMarkOperator();
		}
	}
}
