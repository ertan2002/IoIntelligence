using Newtonsoft.Json.Linq;

namespace IoIntelligence.Client.Models.Workflow.Responses;

public class ModerationAgentResult : IBaseAgentResult
{
    public double ExtremeProfanity { get; private set; }
    public double SexuallyExplicit { get; private set; }
    public double HateSpeech { get; private set; }
    public double Harassment { get; private set; }
    public double SelfHarm { get; private set; }
    public double DangerousContent { get; private set; }
    public bool IsFlagged { get; private set; }

    public void Parse(JToken resultData)
    {
        ExtremeProfanity = resultData["extreme_profanity"]?.Value<double>() ?? 0;
        SexuallyExplicit = resultData["sexually_explicit"]?.Value<double>() ?? 0;
        HateSpeech = resultData["hate_speech"]?.Value<double>() ?? 0;
        Harassment = resultData["harassment"]?.Value<double>() ?? 0;
        SelfHarm = resultData["self_harm"]?.Value<double>() ?? 0;
        DangerousContent = resultData["dangerous_content"]?.Value<double>() ?? 0;

        // Default threshold of 0.7 if not provided
        var threshold = resultData.Parent?.Parent?["args"]?["threshold"]?.Value<double>() ?? 0.7;

        IsFlagged = ExtremeProfanity > threshold ||
                    SexuallyExplicit > threshold ||
                    HateSpeech > threshold ||
                    Harassment > threshold ||
                    SelfHarm > threshold ||
                    DangerousContent > threshold;
    }
}