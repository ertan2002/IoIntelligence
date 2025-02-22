using Newtonsoft.Json;

namespace IoIntelligence.Client.Models.AIModel.Chat;

public class ChatCompletionResponse
{
    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("object")] public string Object { get; set; }

    [JsonProperty("created")] public long Created { get; set; }

    [JsonProperty("model")] public string Model { get; set; }

    [JsonProperty("choices")] public List<ChatCompletionChoice> Choices { get; set; }

    [JsonProperty("usage")] public Usage Usage { get; set; }
}