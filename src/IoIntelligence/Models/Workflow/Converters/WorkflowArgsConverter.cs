using IoIntelligence.Client.Models.Workflow.Requests.Args;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IoIntelligence.Client.Models.Workflow.Converters;

public class WorkflowArgsConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(AgentArgsBase).IsAssignableFrom(objectType) ||
               typeof(IEnumerable<AgentArgsBase>).IsAssignableFrom(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        // Keep existing deserialization logic if needed
        throw new NotImplementedException();
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if(value == null)
        {
            writer.WriteNull();
            return;
        }
        
        var tempSerializer = CreateTemporarySerializer(serializer);

        switch (value)
        {
            case IEnumerable<AgentArgsBase> argsList:
                WriteArray(writer, argsList, tempSerializer);
                break;
            case AgentArgsBase singleArg:
                WriteObject(writer, singleArg, tempSerializer);
                break;
            default:
                throw new JsonSerializationException($"Unexpected type: {value.GetType()}");
        }
    }

    private JsonSerializer CreateTemporarySerializer(JsonSerializer original)
    {
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            ContractResolver = original.ContractResolver,
            Converters = original.Converters
                .Where(c => !(c is WorkflowArgsConverter))
                .ToList()
        };

        return JsonSerializer.Create(settings);
    }

    private void WriteObject(JsonWriter writer, AgentArgsBase arg, JsonSerializer serializer)
    {
        var jObject = new JObject
        {
            ["type"] = arg.Type
        };

        switch (arg)
        {
            case TranslateAgentArgs translate:
                jObject["target_language"] = translate.TargetLanguage;
                break;

            case SummarizeAgentArgs summarize:
                jObject["max_words"] = summarize.MaxWords;
                break;

            case ModerationAgentArgs moderation:
                jObject["threshold"] = moderation.Threshold;
                break;

            case ClassificationAgentArgs classify:
                jObject["classify_by"] = JToken.FromObject(classify.ClassifyBy, serializer);

                break;
            case NameEntityRecognizerAgentArgs:
            case SentimentAgentArgs:
                break;
            case CustomAgentArgs custom:
                jObject["name"] = custom.Name;
                jObject["objective"] = custom.Objective;
                jObject["instructions"] = custom.Instructions;
                break;
            case ModerationAgentArgsWithWorkaround moderationWithWorkaround:
                jObject["name"] = moderationWithWorkaround.Name;
                jObject["objective"] = moderationWithWorkaround.Objective;
                jObject["instructions"] = moderationWithWorkaround.Instructions;
                jObject["threshold"] = moderationWithWorkaround.Threshold;
                break;
            default:
                throw new NotSupportedException($"Argument type {arg.GetType().Name} not supported");
        }

        jObject.WriteTo(writer);
    }

    private void WriteArray(JsonWriter writer, IEnumerable<AgentArgsBase> args, JsonSerializer serializer)
    {
        writer.WriteStartArray();
        foreach (var arg in args) WriteObject(writer, arg, serializer);
        writer.WriteEndArray();
    }
}