using System.Collections.Generic;
using System.Linq;

namespace C_Sharp.Language
{
	public class Scratch
	{

		public static void Test()
		{
			string qName = "/A/B/";

			List<string> qNameElems = qName.Split('/').Where(s => !string.IsNullOrEmpty(s)).ToList();
			List<string> qNameIterated = new List<string>();
			string newQName = "/";
			for(int i=0; i< qNameElems.Count; i++)
			{
				newQName = newQName + qNameElems[i] + "/";
				qNameIterated.Add(newQName);
			}
		}
	}
}
