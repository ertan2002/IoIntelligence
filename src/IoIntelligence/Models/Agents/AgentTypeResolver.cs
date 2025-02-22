using IoIntelligence.Client.Models.Workflow.Requests.Args;

namespace IoIntelligence.Client.Models.Agents;

public static class AgentNameResolver
{
    public static string GetAgentName(AgentArgsBase args)
    {
        return args switch
        {
            TranslateAgentArgs => "translation_agent",
            SentimentAgentArgs => "sentiment_analysis_agent",
            SummarizeAgentArgs => "summary_agent",
            ModerationAgentArgs => "moderation_agent",
            ClassificationAgentArgs => "classification_agent",
            NameEntityRecognizerAgentArgs => "extractor",
            CustomAgentArgs => "custom_agent",
            ModerationAgentArgsWithWorkaround => "moderation_agent",
            _ => throw new NotSupportedException($"Unsupported argument type: {args.GetType().Name}")
        };
    }
}