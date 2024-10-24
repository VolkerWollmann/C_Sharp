using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace C_Sharp.OhterExamples
{
    /// <summary>
    /// #random #http client
    /// </summary>
    public class DoHttpRequests
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

        public static void TestHttpRequestSimple()
        {
            DoTheRequest();
            while (result == 0)
                System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Random number:" + result);
        }

        #region JSON Response

        internal class Post
        {
            public int UserId { get; set; }
           
            public int Id { get; set; }
            
            public string Title { get; set; } = string.Empty;
            
            public string Body { get; set; } = string.Empty;
        }

        private static async void DoRequestJSON()
        {
            result = 0;

            // Request posts by user 1
            string url = "https://jsonplaceholder.typicode.com/posts?userId=1";

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();

                        JsonSerializer js = JsonSerializer.Create();
                        JsonReader jr = new JsonTextReader( new StringReader(responseString));
                        var resultList = js.Deserialize<List<Post>>(jr);

                        Assert.IsTrue(resultList?.Any( e => e.Title == "eum et est occaecati"));
                        result = 2;
                    }

                }
                catch
                {
                    result = 1;
                }
                finally { }
            }
        }


        public static void TestHttpRequestJSON()
        {  
            DoRequestJSON();
            int i = 0;
            while(i<50)
            {
                System.Threading.Thread.Sleep(1000);
                if (result > 0)
                    break;
            }
              
            Assert.AreEqual(result, 2, "Did not reach web page");
        }

        #endregion
    }
}
