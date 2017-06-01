using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Amezquita.ControlTiempos.Mobile.Infrastructure
{
    public class HttpService : IHttpService
    {
        private const string BASE_URL = "http://amez-control-tiempos.azurewebsites.net";

        private enum HttpMethod
        {
            GET = 1,
            POST = 2
        }

        private async Task<TResult> HttpCallAsync<TResult>(HttpMethod httpMethod, string url, ByteArrayContent body, AuthenticationHeaderValue auth = null)
        {
            using (var client = new HttpClient())
            {
                if (auth != null)
                    client.DefaultRequestHeaders.Authorization = auth;

                var requestUrl = $"{BASE_URL}{url}";

                HttpResponseMessage response;

                if (httpMethod == HttpMethod.POST)
                    response = await client.PostAsync(requestUrl, body);
                else
                    response = await client.GetAsync(requestUrl);
                
                    using (response)
                    {
                    
                           using (var content = response.Content)
                            {
                                var result = await content.ReadAsStringAsync();
                                var a = JsonConvert.DeserializeObject<TResult>(result);
                                return a;
                            } 
                   
                    }
            }
        }

        public Task<TResult> PostAsync<TResult>(string url, ByteArrayContent body, AuthenticationHeaderValue auth = null) => HttpCallAsync<TResult>(HttpMethod.POST, url, body, auth);

        public Task<TResult> GetAsync<TResult>(string url, AuthenticationHeaderValue auth = null) => HttpCallAsync<TResult>(HttpMethod.GET, url, null, auth);
    }
}