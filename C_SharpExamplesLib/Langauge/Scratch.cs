using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp
{
	public class Scratch
	{

		public static void Test()
		{
			string qName = "/A/B/";

			List<string> qNameElems = qName.Split('/').Where(s => !string.IsNullOrEmpty(s)).ToList();
			List<string> qNameIterated = new List<string>();
			string nameSofar = "/";
			for(int i=0; i< qNameElems.Count; i++)
			{
				nameSofar = nameSofar + qNameElems[i] + "/";
				qNameIterated.Add(nameSofar);
			}
		}
	}
}
