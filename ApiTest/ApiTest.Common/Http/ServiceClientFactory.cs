using ApiTest.Common.Exceptions;
using RestSharp;
using RestSharp.Authenticators;
using Xunit.Abstractions;

namespace ApiTest.Common.Http
{
    public class ServiceClientFactory
    {
        private readonly ITestOutputHelper testOutputHelper;
        private readonly string baseUrl;
        private readonly string username;
        private readonly string password;

        public ServiceClientFactory(ITestOutputHelper testOutputHelper, string baseUrl, string username, string password)
        {
            this.testOutputHelper = testOutputHelper;
            this.baseUrl = baseUrl;
            this.username = username;
            this.password = password;
        }

        public ServiceClient CreateApiClient()
        {
            var task = Task.Run(async () => await GetDefaultHeadersAsync());
            task.Wait();
            var headers = task.Result;

            var restClient = new RestClient();
            restClient.AddDefaultHeaders(headers);

            return new ServiceClient(testOutputHelper, restClient);
        }

        private async Task<Dictionary<string, string>> GetDefaultHeadersAsync()
        {
            var resource = "secret/token";
            var headerName = "X-Auth-Token";

            testOutputHelper.WriteLine($"POST - {baseUrl}/{resource}");

            var challengerHeader = await GetChallengerHeaderAsync();
            var options = new RestClientOptions(baseUrl)
            {
                Authenticator = new HttpBasicAuthenticator(username, password)
            };

            var client = new RestClient(options);
            var request = new RestRequest(resource);
            request.AddHeader(challengerHeader.Name, challengerHeader.Value.ToString());

            var response = await client.PostAsync(request);

            var secretTokenHeader = GetHeader(response, resource, headerName);

            var headers = new Dictionary<string, string>
            {
                { challengerHeader.Name, challengerHeader.Value.ToString() },
                { secretTokenHeader.Name, secretTokenHeader.Value.ToString() }
            };

            return headers;
        }

        private async Task<HeaderParameter> GetChallengerHeaderAsync()
        {
            var resource = "challenger";
            var headerName = "X-Challenger";

            testOutputHelper.WriteLine($"POST - {baseUrl}/{resource}");

            var options = new RestClientOptions(baseUrl);
            var client = new RestClient(options);

            var request = new RestRequest(resource);
            var response = await client.PostAsync(request);

            var headerParameter = GetHeader(response, resource, headerName);

            return headerParameter;
        }

        private static HeaderParameter? GetHeader(RestResponse response, string resource, string headerName)
        {
            if (response.Headers is null)
            {
                throw new MissingHeadersException(resource);
            }

            var headerParameter = response.Headers.FirstOrDefault(h => h.Name.Equals(headerName));

            if (headerParameter is null)
            {
                throw new MissingHeaderException(headerName, resource);
            }

            if (headerParameter.Value is null)
            {
                throw new MissingHeaderParameterException(nameof(headerParameter.Value));
            }

            return headerParameter;
        }
    }
}
