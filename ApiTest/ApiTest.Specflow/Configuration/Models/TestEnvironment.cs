namespace ApiTest.Specflow.Configuration.Models
{
    public class TestEnvironment
    {
        public string Value { get; set; }

        public TestEnvironment(string value)
        {
            Value = value;
        }
    }
}
