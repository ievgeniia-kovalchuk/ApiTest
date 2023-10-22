using System.Text.Json.Serialization;

namespace ApiTest.Common.Tools.Models
{
    record TokenResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; init; }
    }
}
