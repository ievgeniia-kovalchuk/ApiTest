using RestSharp;
using Xunit.Abstractions;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ApiTest.Common.Http
{
    public class ServiceClientBase
    {
        private readonly ITestOutputHelper testOutputHelper;
        private readonly RestClient restClient;

        protected ServiceClientBase(ITestOutputHelper testOutputHelper, RestClient restClient)
        {
            this.testOutputHelper = testOutputHelper;
            this.restClient = restClient;
        }

        public async Task<RestResponse> ExecuteGetAsync(string url)
        {
            testOutputHelper.WriteLine($"GET - {url}");

            var request = new RestRequest(url);

            var response = await restClient.GetAsync(request);

            return response;
        }

        public async Task<RestResponse<T>> ExecuteGetAsync<T>(string url)
        {
            testOutputHelper.WriteLine($"GET - {url}");

            var request = new RestRequest(url);

            var response = await restClient.ExecuteGetAsync<T>(request);

            return response;
        }

        public async Task<RestResponse> ExecutePostAsync(string url)
        {
            testOutputHelper.WriteLine($"POST - {url}");

            var request = new RestRequest(url);

            var response = await restClient.PostAsync(request);

            return response;
        }

        public async Task<RestResponse<T>> ExecutePostAsync<T>(string url, string json)
        {
            testOutputHelper.WriteLine($"POST - {url}");

            var stopWatch = new Stopwatch();

            var request = new RestRequest(url).AddJsonBody(json);

            stopWatch.Start();
            var response = await restClient.ExecutePostAsync<T>(request);
            stopWatch.Stop();

            LogRequest(request, response, stopWatch.ElapsedMilliseconds);

            return response;
        }

        private void LogRequest(RestRequest request, RestResponse? response, long durationMs)
        {
            var requestToLog = new
            {
                resource = request.Resource,
                parameters = request.Parameters.Select(parameter => new
                {
                    name = parameter.Name,
                    value = parameter.Value,
                    type = parameter.Type.ToString()
                }),
                method = request.Method.ToString(),
                uri = restClient.BuildUri(request),
            };

            var responseToLog = new
            {
                statusCode = response.StatusCode,
                content = response.Content,
                //headers = response.Headers,
                responseUri = response.ResponseUri,
                errorMessage = response.ErrorMessage,
            };

            testOutputHelper.WriteLine($"Request completed in {durationMs} ms");
            testOutputHelper.WriteLine($"Request:\n{JsonConvert.SerializeObject(requestToLog)}");
            testOutputHelper.WriteLine($"Response:\n{JsonConvert.SerializeObject(responseToLog)}");
        }
    }
}
