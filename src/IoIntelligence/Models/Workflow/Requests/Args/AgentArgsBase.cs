using IoIntelligence.Client.Models.Workflow.Converters;
using Newtonsoft.Json;

namespace IoIntelligence.Client.Models.Workflow.Requests.Args;

[JsonConverter(typeof(WorkflowArgsConverter))]
public abstract class AgentArgsBase
{
    [JsonProperty("type")] public abstract string Type { get; }
}