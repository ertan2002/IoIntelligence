using System.Text;
using IoIntelligence.Client.Exceptions;
using IoIntelligence.Client.Models.Agents;
using IoIntelligence.Client.Models.Workflow.Requests;
using IoIntelligence.Client.Models.Workflow.Responses;
using IoIntelligence.Client.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IoIntelligence.Client.Clients;

public class WorkflowClient(HttpClient httpClient) : BaseHttpService(httpClient)
{
    public async Task<T> ExecuteAsync<T>(WorkflowRequest workflowRequest)
        where T : IBaseAgentResult, new()
    {
        ValidateRequest(workflowRequest);

        var agentName = AgentNameResolver.GetAgentName(workflowRequest.Args);

        var request = new WorkflowRequest
        {
            Text = workflowRequest.Text,
            AgentNames = { agentName },
            Args = workflowRequest.Args
        };

        var jsonResponse = await SendRequestAsync(request);
        return ParseResponse<T>(jsonResponse);
    }

    private async Task<string> SendRequestAsync(WorkflowRequest request)
    {
        var content = new StringContent(
            JsonConvert.SerializeObject(request),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync("workflows/run", content);
        var responseBody = await response.Content.ReadAsStringAsync();

        return response.IsSuccessStatusCode
            ? responseBody
            : throw new ApiException($"API Error ({response.StatusCode})", (int)response.StatusCode, responseBody);
    }

    private static T ParseResponse<T>(string jsonResponse) where T : IBaseAgentResult, new()
    {
        var result = new T();
        result.Parse(JObject.Parse(jsonResponse)["result"]);
        return result;
    }

    private static void ValidateRequest(WorkflowRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Text))
            throw new ArgumentException("Request text cannot be empty");

        if ( request.Args == null)
            throw new ArgumentException("At least one agent must be specified");
    }
}