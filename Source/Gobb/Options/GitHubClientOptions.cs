namespace Gobb.Options
{
    public class GitHubClientOptions
    {
        public required string RepositoryOwner { get; set; } = string.Empty;
        public required string RepositoryName { get; set; } = string.Empty;
    }
}