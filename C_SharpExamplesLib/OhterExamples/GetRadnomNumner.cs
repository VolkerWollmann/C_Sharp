using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_Sharp.OhterExamples
{
    /// <summary>
    /// #random #http client
    /// </summary>
    public class GetRadnomNumner
    {
        private static int result = 0;
        private static async void DoTheRequest()
        {
            string url = "https://www.random.org/integers/?num=1&min=1&max=6&col=1&base=10&format=plain&rnd=new";
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    // Make a GET request
                    HttpResponseMessage response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        result = Convert.ToInt32(content);
                    }
                }
                finally { }
            }
        }
        public static void Test()
        {
            DoTheRequest();
            while (result == 0)
                System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Random number:" + result);
        }
    }
}
