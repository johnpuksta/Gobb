using Gobb.Clients.Contracts.GitHub;
using Gobb.Data;
using Gobb.Providers;
using System.Text.Json;

namespace Gobb.Clients
{
    public class GitHubClient : ITicketProvider
    {
        private readonly HttpClient _httpClient;
        private readonly string _repositoryOwner;
        private readonly string _repositoryName;

        public GitHubClient(HttpClient httpClient, string repositoryOwner, string repositoryName)
        {
            _httpClient = httpClient;
            _repositoryOwner = repositoryOwner;
            _repositoryName = repositoryName;
        }

        public async Task<ITicketData> GetTicketSummaryAndDescriptionAsync(string ticketId)
        {
            var url = $"https://api.github.com/repos/{_repositoryOwner}/{_repositoryName}/issues/{ticketId}";
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var issue = JsonSerializer.Deserialize<GitHubIssue>(content);

            if (issue == null)
            {
                throw new InvalidOperationException("Failed to deserialize GitHub issue.");
            }

            return new TicketData(issue.Title, issue.Body);
        }
    }
}