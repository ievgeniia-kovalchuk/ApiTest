using RestSharp;

namespace ApiTest.Common.Exceptions
{
    public class MissingHeadersException : Exception
    {
        public MissingHeadersException() { }

        public MissingHeadersException(string resource) : base($"No headers are found for resource '{resource}'") { }
    }
}