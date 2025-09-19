using PhoenicianCapital.DTO.Agent;
using PhoenicianCapital.DTO.Common;
using PhoenicianCapital.Services.Interfaces;

namespace PhoenicianCapital.Services.Implementations;

public class AgentService(IHttpClientFactory factory, IConfiguration config) : IAgentService
{
    private readonly HttpClient _httpClient = factory.CreateClient();
    private readonly IConfiguration _config = config;

    public async Task<ApiResponse<AgentRunResponse>> RunResearchAsync(AgentRunRequest request)
    {
        var n8nUrl = _config["N8N:WebhookUrl"];
        if (string.IsNullOrEmpty(n8nUrl))
            return new ApiResponse<AgentRunResponse>(false, "N8N webhook URL is not configured", null);

        var response = await _httpClient.PostAsJsonAsync(n8nUrl, request);

        if (!response.IsSuccessStatusCode)
            return new ApiResponse<AgentRunResponse>(
                false,
                $"Agent service error: {response.StatusCode}",
                null
            );

        var data = await response.Content.ReadFromJsonAsync<AgentRunResponse>();
        return data is not null
            ? new ApiResponse<AgentRunResponse>(true, "Agent execution successful", data)
            : new ApiResponse<AgentRunResponse>(false, "Agent returned no data", null);
    }
}
