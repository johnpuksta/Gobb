using Gobb.Clients.Contracts;
using System.Text;

namespace Gobb.Clients.Helpers
{
    /// <summary>
    /// A class to parse Jira tickets and convert them into a more readable format.
    /// </summary>
    public sealed class JiraParser
    {
        public static (string summary, string descriptionText) ParseJiraIssue(JiraIssueFields jiraIssueFields)
        {
            var sb = new StringBuilder();

            if (jiraIssueFields.Description?.Content != null)
            {
                foreach (var block in jiraIssueFields.Description.Content)
                {
                    ParseContentBlock(block, sb);
                }
            }

            return (jiraIssueFields.Summary, sb.ToString().Trim());
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
