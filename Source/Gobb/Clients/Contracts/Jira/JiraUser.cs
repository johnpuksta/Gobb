using System.Text.Json.Serialization;

namespace Gobb.Clients.Contracts.Jira
{
    public class JiraUser
    {
        [JsonPropertyName("accountId")]
        public string? AccountId { get; set; }

        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }
    }
}
