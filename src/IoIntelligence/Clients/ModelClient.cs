using System.Text;
using IoIntelligence.Client.Models.AIModel;
using IoIntelligence.Client.Models.AIModel.Chat;
using IoIntelligence.Client.Services;
using Newtonsoft.Json;

namespace IoIntelligence.Client.Clients;

public class ModelClient : BaseHttpService
{
    public ModelClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<IReadOnlyList<Model>> GetModelsAsync()
    {
        var response = await _httpClient.GetAsync("models");
        var result = await HandleResponse<ModelListResponse>(response);
        return result.Data;
    }

    public async Task<ChatCompletionResponse> CreateChatCompletionAsync(
        ChatCompletionRequest request)
    {
        var content = new StringContent(
            JsonConvert.SerializeObject(request, _serializerSettings),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync("chat/completions", content);
        return await HandleResponse<ChatCompletionResponse>(response);
    }
}