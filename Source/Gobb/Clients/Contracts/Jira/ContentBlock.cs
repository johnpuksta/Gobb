using System.Text.Json.Serialization;

namespace Gobb.Clients.Contracts.Jira
{
    public sealed class ContentBlock
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("content")]
        public List<ContentBlock> Content { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("marks")]
        public List<Mark> Marks { get; set; }
    }
}
