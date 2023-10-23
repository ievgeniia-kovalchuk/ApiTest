using ApiTest.Specflow.Drivers.Contracts;
using ApiTest.Specflow.Models.Todos;
using RestSharp;
using TechTalk.SpecFlow.Assist;

namespace ApiTest.Specflow.StepDefinitions
{
    [Binding]
    public sealed class TodoSteps : Steps
    {
        private readonly ITodoDriver todoDriver;

        private RestResponse restResponse;

        public TodoSteps(ITodoDriver todoDriver)
        {
            this.todoDriver = todoDriver;
            restResponse = null!;
        }

        [When(@"I create new todo item:")]
        public async Task GivenIGetBookingId(Table table)
        {
            var todoItem = table.CreateInstance<Todo>();

            restResponse = await todoDriver.CreateTodoItem(todoItem);
            restResponse.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}