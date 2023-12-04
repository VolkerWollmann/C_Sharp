using System;
using System.Net.Http;
using System.Text;
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
            result = 0;
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

        private static async void DoRequest2()
        {
            result = 0;
            string url = "https://jsonplaceholder.typicode.com/posts?userId=1";
            //string jsonParameter =
            //    @"userId=1";
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    //var content = new StringContent(jsonParameter, Encoding.UTF8, "application/json");

                    // Make a GET request
                    HttpResponseMessage response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        
                        Assert.IsTrue(responseString.Contains("\"id\": 1"));
                        result = 1;
                    }
                }
                finally { }
            }
        }

        public static void TestHttpRequestSimple()
        {
            DoTheRequest();
            while (result == 0)
                System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Random number:" + result);
        }

        public static void TestHttpRequest2()
        {
            DoRequest2();
            while (result == 0)
                System.Threading.Thread.Sleep(1000);
        }
    }
}
