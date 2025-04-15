using Gobb.Clients;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Gobb.Options;
using Microsoft.Extensions.Options;

public class JiraClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<JiraClient> _logger;

    public JiraClient(ILogger<JiraClient> logger, IOptions<JiraClientOptions> options)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        var authString = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{options.Value.Email}:{options.Value.ApiToken}"));
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(options.Value.Url);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        _logger.LogInformation("JiraClient initialized with base URL: {BaseUrl}", options.Value.Url);
    }

    public async Task<JiraIssue> GetIssueAsync(string issueKey)
    {
        _logger.LogDebug("Fetching issue with key: {IssueKey}", issueKey);
        HttpResponseMessage response = await _httpClient.GetAsync($"/rest/api/3/issue/{issueKey}");
        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _logger.LogInformation("Successfully fetched issue: {IssueKey}", issueKey);
            return JsonSerializer.Deserialize<JiraIssue>(json, options);
        }
        else
        {
            string errorMessage = await response.Content.ReadAsStringAsync();
            _logger.LogError("Failed to retrieve issue: {IssueKey}, Status Code: {StatusCode}, Error: {Error}", issueKey, response.StatusCode, errorMessage);
            throw new Exception($"Failed to retrieve issue: {response.StatusCode} - {errorMessage}");
        }
    }
}