using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Questao2
{
    public static class HttpClientService
    {
        private static HttpClient httpClient = new()
        {
            BaseAddress = new Uri("https://jsonmock.hackerrank.com/api/football_matches")
        };

        public static async Task<string> GetRequest(string urlParams)
        {
            string url = httpClient.BaseAddress + urlParams;

            HttpResponseMessage response = await httpClient.GetAsync(url);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return jsonResponse;
        }
    }
}