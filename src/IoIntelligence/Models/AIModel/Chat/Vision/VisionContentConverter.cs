using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace IoIntelligence.Client.Models.AIModel.Chat.Vision;

/// <summary>
/// JSON converter that helps with serializing vision content correctly
/// </summary>
public class VisionContentConverter
{
    /// <summary>
    /// Converts a list of content objects to the JSON format expected by the API
    /// </summary>
    /// <param name="contents">List of content items (text and images)</param>
    /// <returns>JSON string representation of the content</returns>
    public static string ConvertToJson(List<VisionChatCompletionMessageContent> contents)
    {
        return JsonConvert.SerializeObject(contents);
    }

    /// <summary>
    /// Parses a JSON string containing message content
    /// </summary>
    /// <param name="json">JSON string representation of message content</param>
    /// <returns>List of content objects</returns>
    public static List<VisionChatCompletionMessageContent> ParseFromJson(string json)
    {
        return JsonConvert.DeserializeObject<List<VisionChatCompletionMessageContent>>(json);
    }

    /// <summary>
    /// Determines if a content string contains complex content (vision content)
    /// </summary>
    /// <param name="content">The content string to check</param>
    /// <returns>True if the content appears to be serialized vision content</returns>
    public static bool IsVisionContent(string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            return false;
        }

        // Simple heuristic - check if it starts with a JSON array character
        // and contains both "text" and "image_url" properties
        return content.StartsWith("[") &&
               content.Contains("\"type\":") &&
               (content.Contains("\"text\":") || content.Contains("\"image_url\":"));
    }
}