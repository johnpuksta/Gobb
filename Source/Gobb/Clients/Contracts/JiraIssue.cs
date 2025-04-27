using System.Text.Json.Serialization;

namespace Gobb.Clients.Contracts
{
    public class JiraIssue
    {
        [JsonPropertyName("fields")]
        public JiraIssueFields Fields { get; set; }
    }
}
