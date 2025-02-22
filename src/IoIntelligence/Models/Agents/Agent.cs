using Newtonsoft.Json;

namespace IoIntelligence.Client.Models.Agents;

public class Agent
{
    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("description")] public string Description { get; set; }

    [JsonProperty("metadata")] public AgentMetadata Metadata { get; set; }
}