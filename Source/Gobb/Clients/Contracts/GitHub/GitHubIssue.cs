using System.Text.Json.Serialization;

namespace Gobb.Clients.Contracts.GitHub
{
    public sealed class GitHubIssue
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("repository_url")]
        public string RepositoryUrl { get; set; }

        [JsonPropertyName("labels_url")]
        public string LabelsUrl { get; set; }

        [JsonPropertyName("comments_url")]
        public string CommentsUrl { get; set; }

        [JsonPropertyName("events_url")]
        public string EventsUrl { get; set; }

        [JsonPropertyName("html_url")]
        public string HtmlUrl { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("node_id")]
        public string NodeId { get; set; }

        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("user")]
        public GitHubUser User { get; set; }

        [JsonPropertyName("labels")]
        public List<GitHubLabel> Labels { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("locked")]
        public bool Locked { get; set; }

        [JsonPropertyName("assignee")]
        public GitHubUser Assignee { get; set; }

        [JsonPropertyName("assignees")]
        public List<GitHubUser> Assignees { get; set; }

        [JsonPropertyName("milestone")]
        public object Milestone { get; set; }

        [JsonPropertyName("comments")]
        public int Comments { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("closed_at")]
        public DateTime? ClosedAt { get; set; }

        [JsonPropertyName("author_association")]
        public string AuthorAssociation { get; set; }

        [JsonPropertyName("active_lock_reason")]
        public string ActiveLockReason { get; set; }

        [JsonPropertyName("sub_issues_summary")]
        public SubIssuesSummary SubIssuesSummary { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }

        [JsonPropertyName("closed_by")]
        public object ClosedBy { get; set; }

        [JsonPropertyName("reactions")]
        public Reactions Reactions { get; set; }

        [JsonPropertyName("timeline_url")]
        public string TimelineUrl { get; set; }

        [JsonPropertyName("performed_via_github_app")]
        public object PerformedViaGithubApp { get; set; }

        [JsonPropertyName("state_reason")]
        public object StateReason { get; set; }
    }
}
