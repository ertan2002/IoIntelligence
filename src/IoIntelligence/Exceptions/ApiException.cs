using IoIntelligence.Client.Models.Common;

namespace IoIntelligence.Client.Exceptions
{
    public class ApiException : Exception
    {
        public int StatusCode { get; }
        public string RawResponse { get; }
        public IReadOnlyCollection<ErrorDetail> Details { get; }

        public ApiException(
            string message,
            int statusCode,
            string rawResponse,
            IEnumerable<ErrorDetail> details = null)
            : base($"[{statusCode}] {message}")
        {
            StatusCode = statusCode;
            RawResponse = rawResponse;
            Details = details?.ToList() ?? new List<ErrorDetail>();
        }
    }
}
