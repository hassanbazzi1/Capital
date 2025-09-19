
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoenicianCapital.DTO.Auth;
using PhoenicianCapital.DTO.Common;
using PhoenicianCapital.Services.Interfaces;

namespace PhoenicianCapital.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<string>>> Register([FromBody] SignupRequest request)
    {
        var res = await _authService.RegisterAsync(request);
        return res.Success ? Ok(res) : BadRequest(res);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<string>>> Login([FromBody] LoginRequest request)
    {
        var res = await _authService.LoginAsync(request);
        return res.Success ? Ok(res) : Unauthorized(res);
    }

    [HttpGet("me")]
    [Authorize]
    public ActionResult<ApiResponse<object>> Me()
    {
        var userId = User.FindFirst("sub")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var email = User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email)?.Value ?? User.Identity?.Name;

        var data = new { userId, email };
        return Ok(new ApiResponse<object>(true, "Authenticated user", data));
    }
}
