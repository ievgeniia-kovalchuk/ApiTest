using RestSharp;

namespace ApiTest.Common.Exceptions
{
    public class MissingHeaderException : Exception
    {
        public MissingHeaderException() { }

        public MissingHeaderException(string headerName, string resource) : base($"No header '{headerName}' was found for resource '{resource}'") { }
    }
}