using Newtonsoft.Json;

namespace IoIntelligence.Client.Models.Workflow.Requests.Args;

public class ClassificationAgentArgs : AgentArgsBase
{
    public override string Type => "classify";

    [JsonProperty("classify_by")] public List<string> ClassifyBy { get; set; }
}