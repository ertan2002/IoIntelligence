using Newtonsoft.Json.Linq;

namespace IoIntelligence.Client.Models.Workflow.Responses;

public class NameEntityRecognizerAgentResponse : IBaseAgentResult
{
    public List<string> Persons { get; private set; } = new();
    public List<string> Organizations { get; private set; } = new();
    public List<string> Locations { get; private set; } = new();

    public void Parse(JToken resultData)
    {
        Persons = resultData["persons"]?.ToObject<List<string>>() ?? new List<string>();
        Organizations = resultData["organizations"]?.ToObject<List<string>>() ?? new List<string>();
        Locations = resultData["locations"]?.ToObject<List<string>>() ?? new List<string>();
    }
}