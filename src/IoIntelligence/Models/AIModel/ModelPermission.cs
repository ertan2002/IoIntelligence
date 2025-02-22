using Newtonsoft.Json;

namespace IoIntelligence.Client.Models.AIModel;

public class ModelPermission
{
    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("object")] public string Object { get; set; }

    [JsonProperty("created")] public long Created { get; set; }

    [JsonProperty("allow_create_engine")] public bool AllowCreateEngine { get; set; }
}