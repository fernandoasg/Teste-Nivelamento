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
            BaseAddress = new Uri("https://jsonmock.hackerrank.com/api/football_matches");
        }

        public async Task<string> GetAsync(string urlParams)
        {
            HttpPonseMessage response = await httpClient.GetAsync(urlParams);

            response.EnsureSuccessStatusCode().WriteRequestToConsole();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return jsonResponse;
        }
    }
}