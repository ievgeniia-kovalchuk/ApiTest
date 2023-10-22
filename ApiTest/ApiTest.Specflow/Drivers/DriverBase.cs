using ApiTest.Common.Tools;
using RestSharp;
using Xunit.Abstractions;

namespace ApiTest.Specflow.Drivers
{
    public class DriverBase
    {
        private readonly ITestOutputHelper testOutputHelper;
        private readonly string baseUrl;
        private readonly string username;
        private readonly string password;

        public RestClient restClient;

        public DriverBase(ITestOutputHelper testOutputHelper, string baseUrl,string username, string password)
        {
            this.testOutputHelper = testOutputHelper;
            this.baseUrl = baseUrl;
            this.username = username;
            this.password = password;

            var options = new RestClientOptions(baseUrl)
            {
                Authenticator = new RestApiAuthenticator(baseUrl, username, password)
            };
            restClient = new RestClient(options);
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
    }
}
