using System.Text.Json.Serialization;

namespace Gobb.Clients.Contracts.Jira
{
    public class JiraCommentBlock
    {
        [JsonPropertyName("comments")]
        public List<JiraComment>? Comments { get; set; }

        [JsonPropertyName("self")]
        public string? Self { get; set; }

        [JsonPropertyName("maxResults")]
        public int MaxResults { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("startAt")]
        public int StartAt { get; set; }
    }
}
