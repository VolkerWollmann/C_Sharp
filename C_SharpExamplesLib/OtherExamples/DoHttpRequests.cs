﻿using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace C_SharpExamplesLib.OtherExamples
{
    /// <summary>
    /// #random #http client
    /// </summary>
    public class DoHttpRequests
    {
        private static int _result;
        private static async Task DoTheRequest()
        {
            _result = 0;
            string url = "https://www.random.org/integers/?num=1&min=1&max=6&col=1&base=10&format=plain&rnd=new";

            using HttpClient httpClient = new HttpClient();
            try
            {
	            // Make a GET request
	            HttpResponseMessage response = await httpClient.GetAsync(url);

	            if (response.IsSuccessStatusCode)
	            {
		            string content = await response.Content.ReadAsStringAsync();
		            _result = Convert.ToInt32(content);
	            }

	            _result = 2;
            }
            catch
            {
	            _result = 1;
            }
        }

        public static void TestHttpRequestSimple()
        {
            _ = DoTheRequest();
            int i = 0;
            while (i < 50)
            {
                Thread.Sleep(1000);
                if (_result > 0)
                    break;

                i++;
            }

            Assert.AreEqual(_result, 2, "Did not reach web page");
            
            Console.WriteLine("Random number:" + _result);

        }

        #region JSON Response

        internal class Post
        {
            public int UserId { get; set; }
           
            public int Id { get; set; }
            
            public string Title { get; set; } = string.Empty;
            
            public string Body { get; set; } = string.Empty;
        }

        private static async Task DoRequestJson()
        {
            _result = 0;

            // Request posts by user 1
            string url = "https://jsonplaceholder.typicode.com/posts?userId=1";

            using HttpClient httpClient = new HttpClient();
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
		            Assert.IsTrue(resultList?.Any( e => ( e.Id >= 0)));
		            Assert.IsTrue(resultList?.Any(e => (e.UserId >= 0)));
		            Assert.IsTrue(resultList?.Any(e => (!e.Body.IsNullOrEmpty())));
		            _result = 2;
	            }

            }
            catch
            {
	            _result = 1;
            }
        }


        public static void TestHttpRequestJson()
        {  
            _ = DoRequestJson();
            int i = 0;
            while(i<50)
            {
                Thread.Sleep(1000);
                if (_result > 0)
                    break;

                i++;
            }
              
            Assert.AreEqual(_result, 2, "Did not reach web page");
        }

        #endregion
    }
}
