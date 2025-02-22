using System.Net.Http.Headers;
using IoIntelligence.Client.Clients;
using IoIntelligence.Client.Interfaces;

namespace IoIntelligence.Client.Services;

public sealed class IoIntelligenceClient : IIoIntelligenceClient, IDisposable
{
    private const string DefaultApiUrl = "https://api.intelligence.io.solutions/api/v1/";
    private readonly HttpClient _httpClient;
    private bool _disposed;
    
    public ModelClient Models { get; }
    public AgentClient Agents { get; }
    public WorkflowClient Workflows { get; }

    public IoIntelligenceClient(string apiKey, string? baseUrl = null)
    {
        _httpClient = CreateConfiguredClient(apiKey, baseUrl);
        Models = new ModelClient(_httpClient);
        Agents = new AgentClient(_httpClient);
        Workflows = new WorkflowClient(_httpClient);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    private static HttpClient CreateConfiguredClient(string apiKey, string? baseUrl)
    {
        var client = new HttpClient
        {
            BaseAddress = new Uri(baseUrl ?? DefaultApiUrl)
        };
    
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", apiKey);
    
        return client;
    }
    
    private void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing) _httpClient?.Dispose();

        _disposed = true;
    }
}