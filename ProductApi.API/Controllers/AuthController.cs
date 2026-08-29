using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.DTOs;
using ProductApi.Application.Services;

namespace ProductApi.API.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly JwtTokenService _jwtTokenService;

    public AuthController(JwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        // Demo credentials for the technical assessment
        if (request.Username != "admin" ||
            request.Password != "Admin@123")
        {
            return Unauthorized(new
            {
                message = "Invalid username or password."
            });
        }

        var token = _jwtTokenService.GenerateToken(request.Username);

        return Ok(new
        {
            token = token
        });
    }
}
