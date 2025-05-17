using System.Text.Json.Serialization;

namespace Gobb.Clients.Contracts.Jira
{
    public class JiraComment
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("body")]
        public DescriptionBlock? Body { get; set; }

        [JsonPropertyName("author")]
        public JiraUser? Author { get; set; }

        [JsonPropertyName("updateAuthor")]
        public JiraUser? UpdateAuthor { get; set; }

        [JsonPropertyName("created")]
        public string? Created { get; set; }

        [JsonPropertyName("updated")]
        public string? Updated { get; set; }

        [JsonPropertyName("jsdPublic")]
        public bool? JsdPublic { get; set; }
    }
}
