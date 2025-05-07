using System.Text.Json.Serialization;

namespace Gobb.Clients.Contracts.Jira
{
    public sealed class JiraIssueFields
    {
        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("description")]
        public DescriptionBlock Description { get; set; }
    }
}
