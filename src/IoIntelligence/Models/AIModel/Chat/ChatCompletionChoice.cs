using Newtonsoft.Json;

namespace IoIntelligence.Client.Models.AIModel.Chat;

public class ChatCompletionChoice
{
    [JsonProperty("index")] public int Index { get; set; }

    [JsonProperty("message")] public ChatCompletionMessage Message { get; set; }
}