using Gobb.Clients.Helpers;
using Gobb.Data;
using Gobb.Providers;
using Microsoft.Extensions.Logging;

/// <summary>
/// An implementation of <see cref="ITicketProvider"/> that retrieves ticket data from Jira."/>
/// </summary>
public sealed class JiraTicketProvider: ITicketProvider
{
    private readonly ILogger<JiraTicketProvider> _logger;
    private readonly JiraClient _jiraClient;

    /// <summary>
    /// Constructor for <see cref="JiraTicketProvider"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> used for logging</param>
    /// <param name="jiraClient">The <see cref="JiraClient"/> used for REST operations with Jira</param>
    public JiraTicketProvider(ILogger<JiraTicketProvider> logger, JiraClient jiraClient)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _jiraClient = jiraClient ?? throw new ArgumentNullException(nameof(jiraClient));
    }

    /// <inheritdoc/>
    public async Task<ITicketData> GetTicketSummaryAndDescriptionAsync(string ticketKey)
    {
        _logger.LogDebug("Fetching ticket summary and description for key: {TicketKey}", ticketKey);
        var issue = await _jiraClient.GetIssueAsync(ticketKey);
        var (summary, descriptionText) = JiraParser.ParseJiraIssue(issue.Fields);
        _logger.LogInformation("Successfully fetched ticket data for key: {TicketKey}", ticketKey);
        return new TicketData(summary, descriptionText);
    }
}