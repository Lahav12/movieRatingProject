using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Model;
using System.Net.Http.Headers;

namespace movieRatingProject
{
    class ServiceApi
    {
        public static string ScoreChange(int num)
        {
            HttpClient client = new HttpClient();
            //this url was copied from swagger of my project
            string url = @"https://localhost:44312/api/Values/" + num;
        
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = client.GetAsync("").Result;
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return "";
            }
        }

        public static List<ImdbMovie> GetTopMovies()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/"),
                Headers =
            {
                { "x-rapidapi-key", "c7f979aae1mshb356b8dbe5cef10p16f2f0jsn07dde4064b83" },
                { "x-rapidapi-host", "imdb-top-100-movies.p.rapidapi.com" },
            },
            };
            using (var response = client.SendAsync(request).Result)
            {
                response.EnsureSuccessStatusCode();
                var body = response.Content.ReadAsAsync<List<ImdbMovie>>().Result;
                return body;
            }

            return null;

        }
    }
}
