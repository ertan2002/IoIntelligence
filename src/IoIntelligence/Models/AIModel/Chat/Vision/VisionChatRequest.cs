using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace IoIntelligence.Client.Models.AIModel.Chat.Vision;

/// <summary>
/// Specialized request for vision models that can process images
/// </summary>
public class VisionChatRequest
{
    private readonly ChatCompletionRequest _request;

    public VisionChatRequest(string model, string systemMessage = "You are an AI assistant.")
    {
        _request = new ChatCompletionRequest
        {
            Model = model,
            Messages = new List<ChatCompletionMessage>
            {
                new ChatCompletionMessage
                {
                    Role = "system",
                    Content = systemMessage
                }
            }
        };
    }

    /// <summary>
    /// Adds a message with text and image content to the request
    /// </summary>
    /// <param name="text">The text prompt to include with the image</param>
    /// <param name="imageUrl">URL of the image to analyze</param>
    /// <returns>This VisionChatRequest instance for method chaining</returns>
    public VisionChatRequest AddImageMessage(string text = "What is in this image?", string imageUrl = null)
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

        // The converter will handle proper serialization of the array
        string contentJson = JsonConvert.SerializeObject(contents);

        _request.Messages.Add(new ChatCompletionMessage
        {
            Role = "user",
            Content = contentJson
        });

        return this;
    }

    /// <summary>
    /// Builds the final ChatCompletionRequest
    /// </summary>
    public ChatCompletionRequest Build()
    {
        return _request;
    }
}