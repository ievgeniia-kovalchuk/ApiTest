using System.Text.Json.Serialization;

namespace ApiTest.Specflow.Models.Todos
{
    public record Todo
    {
        [JsonPropertyName("id")] public int Id { get; init; }

        [JsonPropertyName("title")] public string Title { get; init; }

        [JsonPropertyName("doneStatus")] public bool DoneStatus { get; init; }

        [JsonPropertyName("description")] public string Description { get; init; }

    }
}
