using IoIntelligence.Client.Exceptions;
using IoIntelligence.Client.Models.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace IoIntelligence.Client.Services
{
    public abstract class BaseHttpService
    {
        protected readonly HttpClient _httpClient;
        protected readonly JsonSerializerSettings _serializerSettings;

        protected BaseHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        protected async Task<T> HandleResponse<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var (errorMessage, details) = ParseErrorContent(content);
                throw new ApiException(
                    message: errorMessage,
                    statusCode: (int)response.StatusCode,
                    rawResponse: content,
                    details: details
                );
            }

            return JsonConvert.DeserializeObject<T>(content, _serializerSettings);
        }

        private (string errorMessage, List<ErrorDetail> details) ParseErrorContent(string content)
        {
            try
            {
                // Try to parse structured error
                var error = JsonConvert.DeserializeObject<ApiError>(content, _serializerSettings);

                return (
                    error.Message ?? FormatDetailsMessage(error.Details),
                    error.Details
                );
            }
            catch
            {
                // Fallback for non-JSON errors (like 404)
                return (content.Trim('"'), null);
            }
        }

        private string FormatDetailsMessage(List<ErrorDetail> details)
        {
            if (details == null || details.Count == 0)
                return "Unknown error occurred";

            return string.Join("\n",
                details.Select((d, i) =>
                    $"[Error {i + 1}] {d.Message} (Location: {string.Join(".", d.Location)})"));
        }
    }

}
