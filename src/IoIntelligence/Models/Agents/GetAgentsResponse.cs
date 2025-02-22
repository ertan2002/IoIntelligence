using Newtonsoft.Json;

namespace IoIntelligence.Client.Models.Agents;

public class GetAgentsResponse
{
    [JsonProperty("agents")] public Dictionary<string, Agent>? Agents { get; set; }
}