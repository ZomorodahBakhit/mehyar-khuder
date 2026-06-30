using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using University.API.Filters;
using University.API.Helpers;
using University.Core.Forms;
using University.Core.Services;

namespace University.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[TypeFilter(typeof(ApiExceptionFilter))]
public class AuthController(IAuthService authService, IJwtTokenHelper jwtTokenHelper) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ApiResponse> Register([FromBody] RegisterForm form)
    {
        var dto = await authService.Register(form);
        return new ApiResponse("User registered successfully", dto);
    }

    [HttpPost("login")]
    public async Task<ApiResponse> Login([FromBody] LoginForm form)
    {
        var dto = await authService.Login(form);
        var token = jwtTokenHelper.GenerateToken(dto);
        return new ApiResponse("Login successful", token);
    }

    [HttpGet("me")]
    [Authorize] 
    public ApiResponse GetMyProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        var userRole = User.FindFirstValue(ClaimTypes.Role);

        var identityInfo = new
        {
            Id = userId,
            Email = userEmail,
            Role = userRole,
            Message = "You have successfully accessed a protected endpoint!"
        };

        return new ApiResponse("Profile retrieved successfully", identityInfo);
    }
}
