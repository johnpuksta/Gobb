using System.Text.Json.Serialization;

namespace Gobb.Clients.Contracts
{
    public sealed class Mark
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
