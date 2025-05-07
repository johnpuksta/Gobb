using System.Text.Json.Serialization;

namespace Gobb.Clients.Contracts.Jira
{
    public sealed class Mark
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
