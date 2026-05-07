using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace jellyfin2link.RESTClient
{
    public class RESTClient : IDisposable
    {
        private IServiceProvider ServiceProvider;
        internal IConfiguration Configuration { get; private set; }
        internal ILogger Logger;
        internal RestSharp.RestClient RestClient;



        public string jellyfinServer => Configuration["PublicJellyfinServer"];
        public string localJellyfinServer => Configuration["LocalJellyfinServer"];
        public string apiKey => Configuration["JellyfinApiKey"];

        private string Token;

        public RESTClient(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Logger = ServiceProvider.GetRequiredService<ILogger<RESTClient>>();
            Configuration = ServiceProvider.GetRequiredService<IConfiguration>();
            RestClient = new RestSharp.RestClient(new RestClientOptions(localJellyfinServer) {  });

            if (string.IsNullOrEmpty(apiKey) == false)
                RestClient.AddDefaultHeader("X-Emby-Token", apiKey);

            Token = apiKey;
        }

        public async Task<T> GetAndHandleExceptionAsync<T>(RestRequest request)
        {
            var response = await ExecuteAsyncAndHandleException(request, Method.Get);

            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public async Task<T> GetAndHandleExceptionAsyncSystemJson<T>(RestRequest request)
        {
            var response = await ExecuteAsyncAndHandleException(request, Method.Get);

            var options = new JsonSerializerOptions
            {
                //PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = { new JsonStringEnumConverter() },
                IncludeFields = true
            };

            return System.Text.Json.JsonSerializer.Deserialize<T>(response.Content, options);
        }

        public async Task<T> PostAndHandleExceptionAsync<T>(RestRequest request)
        {
            var response = await ExecuteAsyncAndHandleException(request, Method.Post);

            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public async Task<T> DeleteAndHandleExceptionAsync<T>(RestRequest request)
        {
            var response = await ExecuteAsyncAndHandleException(request, Method.Delete);

            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public async Task<RestResponse> ExecuteAsyncAndHandleException(RestRequest request, Method httpMethod)
        {
            request.Method = httpMethod;
            var response = await RestClient.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                string exceptionMessage = null;
                if (!string.IsNullOrEmpty(response.Content))
                {
                    exceptionMessage = response.Content;
                }
                else
                {
                    exceptionMessage = "The API returned an error: " + response.StatusCode.ToString() + " " + response.ResponseStatus + " " + response.ErrorMessage;
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException(exceptionMessage);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    throw new UnauthorizedAccessException(exceptionMessage);
                }
                else
                {
                    throw new Exception(exceptionMessage);
                }

            }
            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            return response;
        }

        public void Dispose()
        {
            RestClient.Dispose();
        }
    }
}
