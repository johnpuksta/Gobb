using System.Text.Json.Serialization;

namespace Gobb.Clients.Contracts.GitHub
{
    /// <summary>
    /// Represents a comment on a GitHub issue.
    /// </summary>
    public class GitHubComment
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("body")]
        public string? Body { get; set; }

        [JsonPropertyName("user")]
        public GitHubUser? User { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonPropertyName("reactions")]
        public Reactions? Reactions { get; set; }
    }
}
