using Newtonsoft.Json;

namespace IoIntelligence.Client.Models.AIModel;

public class Model
{
    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("object")] public string Object { get; set; }

    [JsonProperty("created")] public long Created { get; set; }

    [JsonProperty("owned_by")] public string OwnedBy { get; set; }

    [JsonProperty("permission")] public List<ModelPermission> Permissions { get; set; }
}