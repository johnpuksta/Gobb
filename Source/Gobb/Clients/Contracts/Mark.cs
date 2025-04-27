using System.Text.Json.Serialization;

namespace Gobb.Clients.Contracts
{
    public class Mark
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
