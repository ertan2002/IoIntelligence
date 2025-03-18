using Newtonsoft.Json;

namespace IoIntelligence.Client.Models.AIModel.Chat.Vision;

public class ImageUrl
{
    [JsonProperty("url")]
    public string Url { get; set; }
}