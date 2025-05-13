using Gobb.Clients.Contracts.GitHub;
using Gobb.Data;
using Gobb.Options;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Gobb.Clients
{
    /// <summary>
    /// An http client for interacting with the GitHub REST API.
    /// </summary>
    public class GitHubClient : ITicketClient
    {
        private readonly string _repositoryOwner;
        private readonly string _repositoryName;
        private readonly HttpClient _httpClient;
        private readonly ILogger<GitHubClient> _logger;

        //TODO: These should be handled better to map with the csproj's version / app name.
        private const string _productName = "GobbApp";
        private const string _productVersion = "1.0";

        /// <summary>
        /// Constructor for <see cref="GitHubClient"/> that initializes the internal <see cref="HttpClient"/>.
        /// </summary>
        /// <param name="options">The <see cref="GitHubClientOptions"/> used for initialization</param>
        public GitHubClient(IOptions<GitHubClientOptions> options, ILogger<GitHubClient> logger)
        {
            _repositoryOwner = options.Value.RepositoryOwner;
            _repositoryName = options.Value.RepositoryName;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(_productName, _productVersion)); //IMPORTANT! Github needs the user-agent header to verify the request.
            _logger = logger;

            _logger.LogInformation("GitHubClient initialized with repository owner: {RepositoryOwner} and repository name: {RepositoryName}", _repositoryOwner, _repositoryName);
        }

        /// <inheritdoc/>
        public async Task<ITicketData> GetTicketAsync(string ticketId)
        {
            _logger.LogInformation("Fetching ticket with ID: {TicketId}", ticketId);

            var url = $"https://api.github.com/repos/{_repositoryOwner}/{_repositoryName}/issues/{ticketId}";
            _logger.LogDebug("Constructed URL: {Url}", url);

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to fetch ticket. Status code: {StatusCode}", response.StatusCode);
                response.EnsureSuccessStatusCode();
            }

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("Response content: {Content}", content);

            var issue = JsonSerializer.Deserialize<GitHubIssue>(content);

            if (issue == null)
            {
                _logger.LogError("Failed to deserialize GitHub issue.");
                throw new InvalidOperationException("Failed to deserialize GitHub issue.");
            }

            _logger.LogInformation("Successfully fetched ticket with title: {Title}", issue.Title);

            return new TicketData(issue.Title, issue.Body);
        }
    }
}