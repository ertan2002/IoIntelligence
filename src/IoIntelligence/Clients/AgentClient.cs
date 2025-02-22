using IoIntelligence.Client.Models.Agents;
using IoIntelligence.Client.Services;

namespace IoIntelligence.Client.Clients;

public class AgentClient : BaseHttpService
{
    public AgentClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<GetAgentsResponse> GetAgentsAsync()
    {
        var response = await _httpClient.GetAsync("agents");
        return await HandleResponse<GetAgentsResponse>(response);
    }
}