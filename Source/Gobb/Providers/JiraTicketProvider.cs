using Gobb.Clients;
using Gobb.Data;
using Gobb.Options;
using Gobb.Providers;
using Microsoft.Extensions.Options;

public class JiraTicketProvider: ITicketProvider
{
    private readonly JiraClient jiraClient;

    public JiraTicketProvider(IOptions<JiraTicketProviderOptions> options)
    {
        jiraClient = new JiraClient(options.Value.Url, options.Value.Username, options.Value.ApiToken);
    }

    public JiraTicketProvider(string url, string username, string apiToken)
    {
        if(string.IsNullOrEmpty(url))
        {
            throw new ArgumentNullException(nameof(url));
        }

        if (string.IsNullOrEmpty(username))
        {
            throw new ArgumentNullException(nameof(username));
        }

        if (string.IsNullOrEmpty(apiToken))
        {
            throw new ArgumentNullException(nameof(apiToken));
        }

        jiraClient = new JiraClient(url, username, apiToken);
    }

    public async Task<ITicketData> GetTicketSummaryAndDescriptionAsync(string ticketKey)
    {
        var issue = await jiraClient.GetIssueAsync(ticketKey);
        var (summary, descriptionText) = JiraParser.ParseJiraIssue(issue.Fields);
        return new TicketData(summary, descriptionText);
    }
}