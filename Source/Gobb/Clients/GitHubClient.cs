using Gobb.Clients.Contracts.GitHub;
using Gobb.Data;
using Gobb.Options;
using Gobb.Providers;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;

namespace Gobb.Clients
{
    public class GitHubClient : ITicketProvider
    {
        private readonly string _repositoryOwner;
        private readonly string _repositoryName;
        private readonly HttpClient _httpClient;

        public GitHubClient(IOptions<GitHubClientOptions> options)
        {
            _repositoryOwner = options.Value.RepositoryOwner;
            _repositoryName = options.Value.RepositoryName;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("GobbApp", "1.0"));
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