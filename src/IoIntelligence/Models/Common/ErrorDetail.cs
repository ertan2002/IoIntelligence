using Newtonsoft.Json;

namespace IoIntelligence.Client.Models.Common;

public class ErrorDetail
{
    [JsonProperty("loc")] public List<string> Location { get; set; } = new();

    [JsonProperty("msg")] public string Message { get; set; }

    [JsonProperty("type")] public string ErrorType { get; set; }
}