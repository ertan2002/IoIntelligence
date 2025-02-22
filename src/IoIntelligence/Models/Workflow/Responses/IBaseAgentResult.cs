using Newtonsoft.Json.Linq;

namespace IoIntelligence.Client.Models.Workflow.Responses;

public interface IBaseAgentResult
{
    void Parse(JToken resultData);
}