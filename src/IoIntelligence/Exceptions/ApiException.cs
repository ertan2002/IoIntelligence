using IoIntelligence.Client.Models.Common;

namespace IoIntelligence.Client.Exceptions;

public class ApiException(string message, int statusCode, string rawResponse, List<ErrorDetail>? details = null)
    : Exception(message)
{
    public int StatusCode { get; } = statusCode;
    public string RawResponse { get; } = rawResponse;
    public List<ErrorDetail> Details { get; } = details ?? [];
}