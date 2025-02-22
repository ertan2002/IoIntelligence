using Newtonsoft.Json;

namespace IoIntelligence.Client.Models.Agents;

public class AgentMetadata
{
    [JsonProperty("image_url")] public string ImageUrl { get; set; }

    [JsonProperty("tags")] public List<string> Tags { get; set; }
}