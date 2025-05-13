namespace Gobb.Options
{
    /// <summary>
    /// An options class to hold the Jira client configuration.
    /// </summary>
    public class GitHubClientOptions
    {
        /// <summary>
        /// The repository's owner. Example: johnpuksta
        /// </summary>
        public required string RepositoryOwner { get; set; } = string.Empty;

        /// <summary>
        /// The repository's name. Example: Gobb
        /// </summary>
        public required string RepositoryName { get; set; } = string.Empty;
    }
}