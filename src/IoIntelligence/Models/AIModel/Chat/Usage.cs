using Newtonsoft.Json;

namespace IoIntelligence.Client.Models.AIModel.Chat;

public class Usage
{
    [JsonProperty("prompt_tokens")] public int PromptTokens { get; set; }

    [JsonProperty("total_tokens")] public int TotalTokens { get; set; }

    [JsonProperty("completion_tokens")] public int CompletionTokens { get; set; }
}