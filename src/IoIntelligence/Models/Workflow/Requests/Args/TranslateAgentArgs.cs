using Newtonsoft.Json;

namespace IoIntelligence.Client.Models.Workflow.Requests.Args;

public class TranslateAgentArgs : AgentArgsBase
{
    public override string Type => "translate_text";

    [JsonProperty("target_language")] public string TargetLanguage { get; set; }
}