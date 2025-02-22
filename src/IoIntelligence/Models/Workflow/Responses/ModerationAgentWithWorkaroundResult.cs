using Newtonsoft.Json.Linq;

namespace IoIntelligence.Client.Models.Workflow.Responses;

public class ModerationAgentWithWorkaroundResult : IBaseAgentResult
{
    public JToken RawData { get; private set; } = JValue.CreateNull();

    public void Parse(JToken resultData)
    {
        RawData = resultData ?? JValue.CreateNull();
    }
}