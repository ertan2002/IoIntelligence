using Newtonsoft.Json.Linq;

namespace IoIntelligence.Client.Models.Workflow.Responses;

public class SentimentAgentResponse : IBaseAgentResult
{
    public string Sentiment { get; private set; } = "neutral";
    public double Confidence { get; private set; }

    public void Parse(JToken resultData)
    {
        if (resultData is JValue numericValue && numericValue.Type == JTokenType.Float)
        {
            Confidence = (double)numericValue;
            Sentiment = Confidence >= 0.5 ? "positive" : "negative";
        }
        else
        {
            Confidence = resultData["confidence"]?.Value<double>() ?? 0;
            Sentiment = resultData["sentiment"]?.ToString()?.ToLower() ?? "neutral";
        }
    }
}