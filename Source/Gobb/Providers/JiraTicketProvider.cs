using Atlassian.Jira;
using Gobb.Data;
using Gobb.Options;
using Gobb.Providers;
using Microsoft.Extensions.Options;

public class JiraTicketProvider: ITicketProvider
{
    private readonly Jira jiraRestClient;

    public JiraTicketProvider(IOptions<JiraContextProviderOptions> options)
    {
        jiraRestClient = Jira.CreateRestClient(options.Value.Url, options.Value.Username, options.Value.ApiToken);
    }

    public async Task<ITicketData> GetTicketSummaryAndDescriptionAsync(string ticketKey)
    {
        var issue = await jiraRestClient.Issues.GetIssueAsync(ticketKey);
        return new TicketData(issue.Summary, issue.Description);
    }
}