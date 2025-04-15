using System.Text;
using System.Text.Json.Serialization;


namespace Gobb.Clients
{
    public class JiraIssue
    {
        [JsonPropertyName("fields")]
        public JiraIssueFields Fields { get; set; }
    }

    public class JiraIssueFields
    {
        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("description")]
        public DescriptionBlock Description { get; set; }
    }

    public class DescriptionBlock
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("version")]
        public int Version { get; set; }

        [JsonPropertyName("content")]
        public List<ContentBlock> Content { get; set; }
    }

    public class ContentBlock
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("content")]
        public List<ContentBlock> Content { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("marks")]
        public List<Mark> Marks { get; set; }
    }

    public class Mark
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public static class JiraParser
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

                        // Wrap in backticks if it's marked as code
                        if (child.Marks != null && child.Marks.Exists(m => m.Type == "code"))
                            text = $"`{text}`";

                        sb.Append(text);
                    }
                    else if (child.Type == "hardBreak")
                    {
                        sb.AppendLine();
                    }
                }
                sb.AppendLine(); // End of paragraph
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
            else
            {
                // Handle other block types (e.g., heading, orderedList) as needed
            }
        }
    }


}
