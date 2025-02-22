using Newtonsoft.Json;

namespace IoIntelligence.Client.Models.Workflow.Requests.Args;

public class ModerationAgentArgsWithWorkaround : AgentArgsBase
{
    public override string Type => "custom";

    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("objective")] public string Objective { get; set; }

    [JsonProperty("instructions")] public string Instructions { get; set; }
    
    [JsonProperty("threshold")] public double Threshold { get; set; } = 0.5;
}