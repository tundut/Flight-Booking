using FlightBooking.DTOs.Auth;

namespace FlightBooking.Interfaces;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterDto registerDto);
    Task<string> LoginAsync(LoginDto dto);
}