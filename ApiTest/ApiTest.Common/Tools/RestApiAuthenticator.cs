using ApiTest.Common.Tools.Models;
using RestSharp;
using RestSharp.Authenticators;

namespace ApiTest.Common.Tools
{
    public class RestApiAuthenticator : AuthenticatorBase
    {
        private readonly string baseUrl;
        private readonly string username;
        private readonly string password;

        public RestApiAuthenticator(string baseUrl, string username, string password) : base("")
        {
            this.baseUrl = baseUrl;
            this.username = username;
            this.password = password;
        }

        protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
            Token = string.IsNullOrEmpty(Token) ? await GetToken() : Token;
            return new HeaderParameter(KnownHeaders.Authorization, Token);
        }

        async Task<string> GetToken()
        {
            var options = new RestClientOptions(baseUrl)
            {
                Authenticator = new HttpBasicAuthenticator(username, password)
            };

            var client = new RestClient(options);

            var request = new RestRequest("auth");
            var response = await client.PostAsync<TokenResponse>(request);
            
            return $"{response!.Token}";
        }
    }
}
