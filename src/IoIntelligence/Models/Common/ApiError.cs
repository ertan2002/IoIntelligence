using Newtonsoft.Json;

namespace IoIntelligence.Client.Models.Common
{
    public class ApiError
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("detail")]
        public List<ErrorDetail> Details { get; set; }
    }
}
