using Newtonsoft.Json;

namespace IoIntelligence.Client.Models.AIModel;

public class ModelListResponse
{
    [JsonProperty("object")] public string Object { get; set; }

    [JsonProperty("data")] public List<Model> Data { get; set; }
}