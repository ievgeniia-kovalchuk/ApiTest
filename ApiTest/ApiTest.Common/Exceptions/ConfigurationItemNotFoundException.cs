namespace ApiTest.Common.Exceptions
{
    public class ConfigurationItemNotFoundException : Exception
    {
        public ConfigurationItemNotFoundException() { }

        public ConfigurationItemNotFoundException(string item) : base($"Unable to locate configuration item: {item}") { }
    }
}