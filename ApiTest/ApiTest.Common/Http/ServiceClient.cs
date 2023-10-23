using RestSharp;
using Xunit.Abstractions;

namespace ApiTest.Common.Http
{
    public class ServiceClient : ServiceClientBase
    {
        public ServiceClient(ITestOutputHelper testOutputHelper, RestClient restClient) : base(testOutputHelper, restClient)
        {
        }
    }
}
