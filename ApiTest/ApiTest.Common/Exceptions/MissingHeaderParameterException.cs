using RestSharp;

namespace ApiTest.Common.Exceptions
{
    public class MissingHeaderParameterException : Exception
    {
        public MissingHeaderParameterException() { }

        public MissingHeaderParameterException(string parameterName) : base($"Header is missing a parameter '{parameterName}'") { }
    }
}