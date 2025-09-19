using PhoenicianCapital.DTO.Agent;
using PhoenicianCapital.DTO.Common;

namespace PhoenicianCapital.Services.Interfaces;

public interface IAgentService
{
    Task<ApiResponse<AgentRunResponse>> RunResearchAsync(AgentRunRequest request);
}
