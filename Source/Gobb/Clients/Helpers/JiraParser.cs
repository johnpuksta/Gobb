using Gobb.Clients.Contracts.Jira;
using Gobb.Data;
using System.Text;

namespace Gobb.Clients.Helpers
{
    /// <summary>
    /// A class to parse Jira tickets and convert them into a more readable format.
    /// </summary>
    public static class JiraParser
    {
        public static ITicketContext ParseJiraIssue(JiraIssueFields jiraIssueFields)
        {
            var sb = new StringBuilder();

            if (jiraIssueFields.Description?.Content != null)
            {
                foreach (var block in jiraIssueFields.Description.Content)
                {
                    ParseContentBlock(block, sb);
                }
            }

            return new TicketContext(jiraIssueFields.Summary, sb.ToString().Trim(), ParseJiraComments(jiraIssueFields));
        }

        private static List<string> ParseJiraComments(JiraIssueFields jiraIssueFields)
        {
            var comments = new List<string>();
            var commentBlock = jiraIssueFields.Comment;
            if (commentBlock?.Comments != null)
            {
                foreach (var comment in commentBlock.Comments)
                {
                    if (comment.Body?.Content != null)
                    {
                        var sb = new StringBuilder();
                        foreach (var block in comment.Body.Content)
                        {
                            ParseContentBlock(block, sb);
                        }
                        comments.Add(sb.ToString().Trim());
                    }
                }
            }
            return comments;
        }

        private static void ParseContentBlock(ContentBlock block, StringBuilder sb, int indentLevel = 0)
        {
            if (block.Type == "paragraph")
            {
                foreach (var child in block.Content ?? new List<ContentBlock>())
                {
                    if (child.Type == "text")
                    {
                        string text = child.Text ?? "";

                        if (child.Marks != null && child.Marks.Exists(m => m.Type == "code"))
                            text = $"`{text}`";

                        sb.Append(text);
                    }
                    else if (child.Type == "hardBreak")
                    {
                        sb.AppendLine();
                    }
                }
                sb.AppendLine();
            }
            else if (block.Type == "bulletList")
            {
                foreach (var listItem in block.Content ?? new List<ContentBlock>())
                {
                    sb.Append(new string(' ', indentLevel * 2));
                    sb.Append("• ");

                    foreach (var itemContent in listItem.Content ?? new List<ContentBlock>())
                    {
                        ParseContentBlock(itemContent, sb, indentLevel + 1);
                    }
                }
            }
        }
    }
}
