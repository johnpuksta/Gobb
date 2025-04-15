using Gobb.Clients;
using Gobb.Data;
using Gobb.Providers;
using Microsoft.Extensions.Logging;

public class JiraTicketProvider: ITicketProvider
{
    private readonly ILogger<JiraTicketProvider> _logger;
    private readonly JiraClient _jiraClient;

    public JiraTicketProvider(ILogger<JiraTicketProvider> logger, JiraClient jiraClient)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _jiraClient = jiraClient ?? throw new ArgumentNullException(nameof(jiraClient));
    }

    public async Task<ITicketData> GetTicketSummaryAndDescriptionAsync(string ticketKey)
    {
        _logger.LogDebug("Fetching ticket summary and description for key: {TicketKey}", ticketKey);
        var issue = await _jiraClient.GetIssueAsync(ticketKey);
        var (summary, descriptionText) = JiraParser.ParseJiraIssue(issue.Fields);
        _logger.LogInformation("Successfully fetched ticket data for key: {TicketKey}", ticketKey);
        return new TicketData(summary, descriptionText);
    }
}