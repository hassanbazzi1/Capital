using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoenicianCapital.DTO.Agent;
using PhoenicianCapital.DTO.Common;
using PhoenicianCapital.Services.Interfaces;

namespace PhoenicianCapital.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgentController(IAgentService agentService) : ControllerBase
{
    private readonly IAgentService _agentService = agentService;

    [Authorize]
    [HttpPost("run")]
    public async Task<IActionResult> RunAgent([FromBody] AgentRunRequest request)
    {
        if (request is null)
        {
            return BadRequest(new ApiResponse<string>(
                false,
                "Invalid request payload",
                null
            ));
        }

        try
        {
            var result = await _agentService.RunResearchAsync(request);

            if (!result.Success)
                return StatusCode(StatusCodes.Status502BadGateway, result);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ApiResponse<string>(false, "Agent execution failed", ex.Message));
        }
    }
}
