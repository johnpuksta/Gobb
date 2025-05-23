﻿using System.Text.Json.Serialization;

namespace Gobb.Clients.Contracts.Jira
{
    public sealed class DescriptionBlock
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("version")]
        public int Version { get; set; }

        [JsonPropertyName("content")]
        public List<ContentBlock> Content { get; set; }
    }
}
