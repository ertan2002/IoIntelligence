using Newtonsoft.Json;

namespace IoIntelligence.Client.Models.Common;

public class ApiError
{
    [JsonProperty("message")] public string Message { get; set; }

    [JsonProperty("code")] public string Code { get; set; }

    [JsonProperty("details")] public List<ErrorDetail>? Details { get; set; }
}