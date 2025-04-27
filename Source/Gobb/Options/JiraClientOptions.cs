namespace Gobb.Options
{
    /// <summary>
    /// An options class to hold the Jira client configuration.
    /// </summary>
    public sealed class JiraClientOptions
    {
        /// <summary>
        /// The Jira url. Example: https://{your_domain}.atlassian.net
        /// </summary>
        public required string Url { get; set; }

        /// <summary>
        /// The email address of the user's account in Jira. Example: user@gmail.com
        /// </summary>
        public required string Email {get; set;}

        /// <summary>
        /// The API token of the user's account in Jira. Tokens can be created here: https://id.atlassian.com/manage-profile/security/api-tokens.
        /// </summary>
        public required string ApiToken {get; set;}
    }
}
