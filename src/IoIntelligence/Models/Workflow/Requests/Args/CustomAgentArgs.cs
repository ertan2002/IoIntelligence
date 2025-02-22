using Newtonsoft.Json;

namespace IoIntelligence.Client.Models.Workflow.Requests.Args;

public class CustomAgentArgs : AgentArgsBase
{
    public override string Type => "custom";

    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("objective")] public string Objective { get; set; }

    [JsonProperty("instructions")] public string Instructions { get; set; }
}