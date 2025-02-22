using Newtonsoft.Json.Linq;

namespace IoIntelligence.Client.Models.Workflow.Responses;

public class ClassificationAgentResult : IBaseAgentResult
{
    public string Category { get; private set; }

    public void Parse(JToken resultData)
    {
        Category = resultData.ToString();
    }
}