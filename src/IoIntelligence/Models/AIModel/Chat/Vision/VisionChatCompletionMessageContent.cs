using Newtonsoft.Json;

namespace IoIntelligence.Client.Models.AIModel.Chat.Vision;

public class VisionChatCompletionMessageContent
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("image_url")]
    public ImageUrl ImageUrl { get; set; }
}