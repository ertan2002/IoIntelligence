using IoIntelligence.Client.Models.AIModel.Chat.Vision;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IoIntelligence.Client.Models.AIModel.Chat;

/// <summary>
/// Custom JsonConverter for handling different types of chat message content
/// </summary>
public class ChatCompletionMessageJsonConverter : JsonConverter<ChatCompletionMessage>
{
    public override ChatCompletionMessage ReadJson(JsonReader reader, Type objectType, ChatCompletionMessage existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject jObject = JObject.Load(reader);
        
        ChatCompletionMessage message = new ChatCompletionMessage
        {
            Role = jObject["role"]?.ToString()
        };

        var content = jObject["content"];
        
        // Check if content is a string or an array
        if (content.Type == JTokenType.String)
        {
            message.Content = content.ToString();
        }
        else if (content.Type == JTokenType.Array)
        {
            // For array content, convert back to string to maintain compatibility
            message.Content = content.ToString(Formatting.None);
        }
        
        return message;
    }

    public override void WriteJson(JsonWriter writer, ChatCompletionMessage value, JsonSerializer serializer)
    {
        JObject jObject = new JObject();
        jObject.Add("role", value.Role);
        
        // Check if the content is a serialized JSON array (for vision content)
        if (VisionContentConverter.IsVisionContent(value.Content))
        {
            // Parse the content string back to a JArray to properly serialize
            jObject.Add("content", JArray.Parse(value.Content));
        }
        else
        {
            // Normal string content
            jObject.Add("content", value.Content);
        }
        
        jObject.WriteTo(writer);
    }
} 