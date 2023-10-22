using BoDi;
using ApiTest.Specflow.Configuration;

namespace ApiTest.Specflow.StepDefinitions
{
    [Binding]
    public class BeforeSteps : Steps, IDisposable
    {
        private readonly IObjectContainer objectContainer;

        public BeforeSteps(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        [BeforeScenario()]
        public void AddHttpClients()
        {
            objectContainer
                .AddConfiguration()
                .AddApiClients();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
