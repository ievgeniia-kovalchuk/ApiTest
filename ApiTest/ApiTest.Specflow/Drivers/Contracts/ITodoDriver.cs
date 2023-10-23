using ApiTest.Specflow.Models.Todos;
using RestSharp;

namespace ApiTest.Specflow.Drivers.Contracts
{
    public interface ITodoDriver
    {
        Task<RestResponse> CreateTodoItem(Todo todoItem);
    }
}
