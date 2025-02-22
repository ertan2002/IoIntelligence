using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IoIntelligence.Client.Models.Workflow.Requests.Args;

public class ModerationAgentArgs : AgentArgsBase
{
    public override string Type => "moderation";

    [JsonProperty("threshold")] public double Threshold { get; set; } = 0.7;
    
}