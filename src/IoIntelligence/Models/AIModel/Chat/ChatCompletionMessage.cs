using Newtonsoft.Json;

namespace IoIntelligence.Client.Models.AIModel.Chat;

[JsonConverter(typeof(ChatCompletionMessageJsonConverter))]
public class ChatCompletionMessage
{
    [JsonProperty("role")] public string Role { get; set; }

    [JsonProperty("content")] public string Content { get; set; }
}