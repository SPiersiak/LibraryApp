using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Mobile.Services.Request
{
    public class RequestService : IRequestService
    {
        private HttpClient CreateHttpClient()
        {
            var client = new HttpClient(GetInsecureHandler());

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
        // This method must be in a class in a platform project, even if
        // the HttpClient object is constructed in a shared project.
        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain,
           errors) =>
            {
                return true;
            };
            return handler;
        }
        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                //if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                //{

                //}    

                throw new Exception(content);
            }
        }
        public async Task<TResult> GetAsync<TResult>(string uri)
        {
            HttpClient client = CreateHttpClient();

            var response = await client.GetAsync(uri);

            await HandleResponse(response);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResult>(content);
        }
        public Task<TResult> PostAsync<TResult>(string uri, TResult data)
        {
            return PostAsync<TResult, TResult>(uri, data);
        }

        public async Task<TResult> PostAsync<TRequest, TResult>(string uri, TRequest data)
        {
            HttpClient client = CreateHttpClient();

            var serializedData = JsonConvert.SerializeObject(data);

            var response = await client.PostAsync(uri, new StringContent(serializedData, Encoding.UTF8, "application/json"));

            await HandleResponse(response);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResult>(content);
        }

        public Task<TResult> PutAsync<TResult>(string uri, TResult data)
        {
            return PutAsync<TResult, TResult>(uri, data);
        }

        public async Task<TResult> PutAsync<TRequest, TResult>(string uri, TRequest data)
        {
            HttpClient client = CreateHttpClient();

            var serializedData = JsonConvert.SerializeObject(data);

            var response = await client.PutAsync(uri, new StringContent(serializedData, Encoding.UTF8, "application/json"));

            await HandleResponse(response);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResult>(content);
        }
    }
}
