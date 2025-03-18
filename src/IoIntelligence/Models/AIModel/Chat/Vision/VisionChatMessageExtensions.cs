using Newtonsoft.Json;
using System.Collections.Generic;

namespace IoIntelligence.Client.Models.AIModel.Chat.Vision;

/// <summary>
/// Extension methods for working with chat messages that include images
/// </summary>
public static class VisionChatMessageExtensions
{
    /// <summary>
    /// Creates a new chat message with text and image content
    /// </summary>
    /// <param name="text">The text prompt to include with the image</param>
    /// <param name="imageUrl">URL of the image to analyze</param>
    /// <returns>A chat message with the text and image content</returns>
    public static ChatCompletionMessage CreateImageMessage(string text, string imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl))
        {
            throw new ArgumentException("Image URL cannot be null or empty", nameof(imageUrl));
        }

        var contents = new List<VisionChatCompletionMessageContent>
        {
            new VisionChatCompletionMessageContent { Type = "text", Text = text },
            new VisionChatCompletionMessageContent
            {
                Type = "image_url",
                ImageUrl = new ImageUrl { Url = imageUrl }
            }
        };

        // The custom converter will handle proper serialization of the array
        string contentJson = JsonConvert.SerializeObject(contents);

        return new ChatCompletionMessage
        {
            Role = "user",
            Content = contentJson
        };
    }
}