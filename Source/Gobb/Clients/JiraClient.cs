﻿using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Gobb.Options;
using Microsoft.Extensions.Options;
using Gobb.Clients.Contracts.Jira;
using Gobb.Clients;
using Gobb.Data;
using Gobb.Clients.Helpers;

/// <summary>
/// An http client for interacting with the Jira REST API.
/// </summary>
public sealed class JiraClient: ITicketClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<JiraClient> _logger;

    /// <summary>
    /// A constructor for <see cref="JiraClient"/> that initializes the internal <see cref="HttpClient"/> with basic authentication.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> used for logging</param>
    /// <param name="options">The <see cref="JiraClientOptions"/> containing credentials needed for basic authentication</param>
    /// <exception cref="ArgumentNullException"></exception>
    public JiraClient(ILogger<JiraClient> logger, IOptions<JiraClientOptions> options)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        var authString = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{options.Value.Email}:{options.Value.ApiToken}"));
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(options.Value.Url);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        _logger.LogDebug("JiraClient initialized with base URL: {BaseUrl}", options.Value.Url);
    }

    /// <inheritdoc/>
    public async Task<ITicketContext> GetTicketAsync(string ticketId)
    {
        _logger.LogDebug("Fetching issue with key: {IssueKey}", ticketId);
        HttpResponseMessage response = await _httpClient.GetAsync($"/rest/api/3/issue/{ticketId}");
        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _logger.LogInformation("Successfully fetched issue: {IssueKey}", ticketId);
            var jiraIssue = JsonSerializer.Deserialize<JiraIssue>(json, options);
            return JiraParser.ParseJiraIssue(jiraIssue.Fields);
        }
        else
        {
            string errorMessage = await response.Content.ReadAsStringAsync();
            _logger.LogError("Failed to retrieve issue: {IssueKey}, Status Code: {StatusCode}, Error: {Error}", ticketId, response.StatusCode, errorMessage);
            throw new HttpRequestException($"Failed to retrieve issue: {response.StatusCode} - {errorMessage}");
        }
    }
}