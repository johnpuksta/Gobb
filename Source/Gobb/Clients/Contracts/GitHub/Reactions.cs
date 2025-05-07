using System.Text.Json.Serialization;

namespace Gobb.Clients.Contracts.GitHub
{
    public sealed class Reactions
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }

        [JsonPropertyName("+1")]
        public int PlusOne { get; set; }

        [JsonPropertyName("-1")]
        public int MinusOne { get; set; }

        [JsonPropertyName("laugh")]
        public int Laugh { get; set; }

        [JsonPropertyName("hooray")]
        public int Hooray { get; set; }

        [JsonPropertyName("confused")]
        public int Confused { get; set; }

        [JsonPropertyName("heart")]
        public int Heart { get; set; }

        [JsonPropertyName("rocket")]
        public int Rocket { get; set; }

        [JsonPropertyName("eyes")]
        public int Eyes { get; set; }
    }
}
