using Newtonsoft.Json.Linq;

namespace IoIntelligence.Client.Models.Workflow.Responses;

public class SummaryAgentResponse : IBaseAgentResult
{
    public string Summary { get; private set; }
    public List<string> KeyPoints { get; private set; }

    public void Parse(JToken resultData)
    {
        Summary = resultData["summary"]?.ToString();
        KeyPoints = resultData["key_points"]?.ToObject<List<string>>();
    }
}