using Newtonsoft.Json;

namespace IoIntelligence.Client.Models.AIModel.Chat;

public class ChatCompletionRequest
{
    [JsonProperty("model")] public required string Model { get; set; }

    [JsonProperty("messages")] public required List<ChatCompletionMessage> Messages { get; set; }
}