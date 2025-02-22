using Newtonsoft.Json;

namespace IoIntelligence.Client.Models.Workflow.Requests.Args;

public class SummarizeAgentArgs : AgentArgsBase
{
    public override string Type => "summarize_text";

    [JsonProperty("max_words")] public int? MaxWords { get; set; }
}