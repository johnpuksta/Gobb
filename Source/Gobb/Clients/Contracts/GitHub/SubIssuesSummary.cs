using System.Text.Json.Serialization;

namespace Gobb.Clients.Contracts.GitHub
{
    public sealed class SubIssuesSummary
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("completed")]
        public int Completed { get; set; }

        [JsonPropertyName("percent_completed")]
        public int PercentCompleted { get; set; }
    }
}
