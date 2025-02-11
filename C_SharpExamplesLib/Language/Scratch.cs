using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language
{
	public class Scratch
	{

		public static void Test()
		{
			string qName = "/A/B/";

			List<string> qNameElems = qName.Split('/').Where(s => !string.IsNullOrEmpty(s)).ToList();
			List<string> qNameIterated = [];
			string newQName = "/";
			foreach (var t in qNameElems)
            {
                newQName = newQName + t + "/";
                qNameIterated.Add(newQName);
            }

			Assert.IsNotNull(qNameIterated);
		}
	}
}
