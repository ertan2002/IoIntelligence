using Newtonsoft.Json;
using System.Collections.Generic;

namespace IoIntelligence.Client.Models.AIModel.Chat.Vision;

/// <summary>
/// Represents a chat message that can contain multiple content items including text and images
/// </summary>
public class VisionChatCompletionMessage
{
    [JsonProperty("role")]
    public string Role { get; set; }

    [JsonProperty("content")]
    public List<VisionChatCompletionMessageContent> Content { get; set; }
}