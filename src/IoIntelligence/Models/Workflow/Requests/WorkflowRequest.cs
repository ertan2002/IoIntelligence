using IoIntelligence.Client.Models.Workflow.Converters;
using IoIntelligence.Client.Models.Workflow.Requests.Args;
using Newtonsoft.Json;

namespace IoIntelligence.Client.Models.Workflow.Requests;

public class WorkflowRequest
{
    [JsonProperty("text")] public string Text { get; set; }

    [JsonProperty("agent_names")] public List<string> AgentNames { get; private set; } = new();

    [JsonProperty("args")]
    [JsonConverter(typeof(WorkflowArgsConverter))]
    public AgentArgsBase Args { get; set; }
}