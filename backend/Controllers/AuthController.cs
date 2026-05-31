using Microsoft.AspNetCore.Mvc;
using FlightBooking.Interfaces;
using FlightBooking.DTOs.Auth;

namespace FlightBooking.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // Register
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto);
        if (!result.Success)
        {
            return BadRequest(new { message = result.Message });
        }
        return Ok(new { message = result.Message });
    }

    // Login
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);
        if (!result.Success)
        {
            return BadRequest(new { message = result.Message });
        }
        return Ok(new { message = result.Message, user = result.User, token = result.Token });
    }
}
