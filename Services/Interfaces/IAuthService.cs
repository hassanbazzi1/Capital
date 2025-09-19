using PhoenicianCapital.DTO.Auth;
using PhoenicianCapital.DTO.Common;

namespace PhoenicianCapital.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<string>> RegisterAsync(SignupRequest request);
        Task<ApiResponse<string>> LoginAsync(LoginRequest request);
    }
}
