using Newtonsoft.Json.Linq;

namespace IoIntelligence.Client.Models.Workflow.Responses;

public class TranslationAgentResponse : IBaseAgentResult
{
    public string TranslatedText { get; private set; }

    public void Parse(JToken resultData)
    {
        TranslatedText = resultData?.ToString();
    }
}