using ApiTest.Common.Http;
using ApiTest.Specflow.Drivers.Contracts;
using ApiTest.Specflow.Models.Todos;
using Newtonsoft.Json;
using RestSharp;
using Xunit.Abstractions;

namespace ApiTest.Specflow.Drivers
{
    public class TodoDriver : ITodoDriver
    {
        private readonly string baseUrl;
        private readonly ServiceClient serviceClient;
        
        public TodoDriver(ITestOutputHelper testOutputHelper, string baseUrl, string username, string password)
        {
            this.baseUrl = baseUrl;
            var factory = new ServiceClientFactory(testOutputHelper, baseUrl, username, password);

            serviceClient = factory.CreateApiClient();
        }

        /// <summary>
        /// Create new todo item
        /// POST - {TestApi}/todos
        /// </summary>
        public async Task<RestResponse> CreateTodoItem(Todo todoItem)
        {
            var url = $"{baseUrl}/todos";

            var json = JsonConvert.SerializeObject(todoItem);

            var response = await serviceClient.ExecutePostAsync<Todo>(url, json);

            return response;
        }
    }
}
