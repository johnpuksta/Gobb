﻿using System.Text.Json.Serialization;

namespace Gobb.Clients.Contracts.Jira
{
    public sealed class JiraIssue
    {
        [JsonPropertyName("fields")]
        public JiraIssueFields Fields { get; set; }
    }
}
