using Microsoft.AspNetCore.Identity;
using PhoenicianCapital.DTO.Auth;
using PhoenicianCapital.DTO.Common;
using PhoenicianCapital.Entities;
using PhoenicianCapital.Helpers;
using PhoenicianCapital.Services.Interfaces;

namespace PhoenicianCapital.Services.Implementations
{
    public class AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration config) : IAuthService
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        private readonly IConfiguration _config = config;

        public async Task<ApiResponse<string>> RegisterAsync(SignupRequest request)
        {
            if (request.Password != request.ConfirmPassword)
                return new ApiResponse<string>(false, "Passwords do not match", null);

            var user = new AppUser { UserName = request.Email, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return new ApiResponse<string>(false, string.Join(", ", result.Errors.Select(e => e.Description)), null);

            var token = JwtGenerator.GenerateToken(user, _config);
            return new ApiResponse<string>(true, "Registration successful", token);
        }

        public async Task<ApiResponse<string>> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return new ApiResponse<string>(false, "User not found", null);

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                return new ApiResponse<string>(false, "Invalid credentials", null);

            var token = JwtGenerator.GenerateToken(user, _config);
            return new ApiResponse<string>(true, "Login successful", token);
        }

    }
}
